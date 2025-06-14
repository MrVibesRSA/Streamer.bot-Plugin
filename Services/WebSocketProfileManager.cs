
using MrVibesRSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Services
{
    public class WebSocketProfileManager
    {
        // Constants
        private const int MaxRetries = 10;

        // Static Singleton Instance
        private static WebSocketProfileManager _instance;
        public static WebSocketProfileManager Instance => _instance ??= new WebSocketProfileManager();

        // Events
        public event EventHandler<WebSocketService> ProfileAdded;
        public event EventHandler<WebSocketService> ProfileRemoved;


        // Private Fields
        private readonly ConcurrentDictionary<string, WebSocketService> _connections = new();
        private readonly Dictionary<string, int> _retryCounts = new();
        private ProfileManager _profileHandler = new ProfileManager();

        // Public Properties
        public int ConnectedCount => _connections.Values.Count(ws => ws.IsConnected);
        public WebSocketService _service;

        // Add Profile
        public void AddProfile(string profileId, WebSocketService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            // Remove old connection if it exists
            if (_connections.TryGetValue(profileId, out var existing))
            {
                existing.Close();
                _connections.TryRemove(profileId, out _);
                MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocketProfileManager] Existing profile removed: {profileId}");
            }

            if (_connections.TryAdd(profileId, service))
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocketProfileManager] Profile added: {profileId}");
                ProfileAdded?.Invoke(this, service);
            }
        }

        // Remove Profile
        public void RemoveProfile(string profileId)
        {
            if (_connections.TryGetValue(profileId, out var service))
            {
                service.Close();
                if (_connections.TryRemove(profileId, out _))
                {
                    MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocketProfileManager] Profile removed: {profileId}");
                    ProfileRemoved?.Invoke(this, service);
                }
            }
        }

        // Connect Service for a profile with timeout
        public async Task<WebSocketService> ConnectServiceAsync(string profileId, int timeoutMs = 5000)
        {
            // Cleanup existing connection
            RemoveProfile(profileId);

            var profile = _profileHandler.LoadProfile(profileId);
            if (profile == null)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Profile not found: {profileId}");
                return null;
            }

            _service = new WebSocketService(profileId);
            var uri = $"ws://{profile.Address}:{profile.Port}{profile.Endpoint}";

            try
            {
                AddProfile(profileId, _service);
                MonitorConnection(profileId, profile.Name, _service);

                var connectTask = _service.ConnectAsync(uri);
                var timeoutTask = Task.Delay(timeoutMs);
                var completedTask = await Task.WhenAny(connectTask, timeoutTask).ConfigureAwait(false);

                if (completedTask == timeoutTask)
                {
                    MacroDeckLogger.Warning(PluginInstance.Main, $"Connection timeout for {profileId} after {timeoutMs}ms");
                    RemoveProfile(profileId);
                    return null;
                }

                await connectTask.ConfigureAwait(false);
                MacroDeckLogger.Info(PluginInstance.Main, $"Successfully connected to {profileId}");

                SubsribeToServiceEvents(_service);
                DefaultServices(_service);

                return _service;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error connecting to {profileId}: {ex.Message}");
                RemoveProfile(profileId);
                return null;
            }
        }

        private void SubsribeToServiceEvents(WebSocketService service)
        {
            if (service == null) return;
            service.MessageReceived_Hello += OnHelloMessageRecieved;
            service.MessageReceived_AuthenticateRequest += OnAuthenticationMessageRecieved;
            service.MessageReceived_Actions += OnActionsMessageRecieved;
            service.MessageReceived_Globals += OnGlobalsMessageRecieved;
            service.MessageReceived_Events += OnEventsList;
            service.MessageReceived_EventFired += OnEventFired;
        }

        private void UnsubscribeFromServiceEvents(WebSocketService service)
        {
            if (service == null) return;
            service.MessageReceived_Hello -= OnHelloMessageRecieved;
            service.MessageReceived_AuthenticateRequest -= OnAuthenticationMessageRecieved;
            service.MessageReceived_Actions -= OnActionsMessageRecieved;
            service.MessageReceived_Events -= OnEventsList;
            service.MessageReceived_EventFired -= OnEventFired;
        }

        private void DefaultServices(WebSocketService service)
        {
            if (service == null) return;
            service.GetActionsList();
            service.GetEventList();
            service.GetGlobalsList();
        }

        private void OnActionsMessageRecieved(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);
                if (json["id"]?.ToString() == "GetActionsForMacroDeck" && json["count"] != null)
                {
                    int count = json["count"].Value<int>();
                    MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocket] Actions count: {count}");

                    if (!string.IsNullOrEmpty(_service.ProfileId))
                    {
                        // Save entire actions JSON string thread-safely
                        DataManager.SaveActions(_service.ProfileId, e);
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse Actions message: {ex.Message}");
            }
        }

        private void OnGlobalsMessageRecieved(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);

                if (json["id"]?.ToString() != "GetGlobalsForMacroDeck")
                    return;

                var variableDict = json["variables"].ToObject<Dictionary<string, GlobalVariable>>();
                if (variableDict != null && variableDict.Count > 0)
                {
                    // Convert to Dictionary<string, string> (name → value)
                    var globals = variableDict.ToDictionary(x => x.Key, x => x.Value.Value);

                    MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocket] Globals count: {globals.Count}");
                    if (!string.IsNullOrEmpty(_service.ProfileId))
                    {
                        DataManager.SaveGlobals(_service.ProfileId, globals);
                    }
                }


                var variablesJson = json["variables"] as JObject;
                if (variablesJson == null)
                    return;

                var profileName = GetProfileNameByService(_service);
                string prefix = $"{profileName}_";

                var incomingDict = new Dictionary<string, JToken>();
                string groupName = $"GlobalVariables_{_service.ProfileId}";

                var allVariables = VariableManager.GetVariables(PluginInstance.Main) ?? new List<Variable>();
                var existingDict = allVariables
                    .Where(v => v.Name.StartsWith(prefix))
                    .ToDictionary(v => v.Name, v => v);

                foreach (var prop in variablesJson.Properties())
                {
                    string fullName = prefix + prop.Name;
                    incomingDict[fullName] = prop.Value["value"];
                }

                // Add or update
                foreach (var kvp in incomingDict)
                {
                    string key = kvp.Key;
                    string value = kvp.Value?.ToString() ?? "";

                    if (!existingDict.ContainsKey(key) || existingDict[key].Value != value)
                    {
                        VariableType type = VariableTypeHelper.GetVariableType(kvp.Value);
                        VariableManager.SetValue(key, value, type, PluginInstance.Main, new string[] { groupName });
                    }
                }

                // Remove missing
                foreach (var existing in existingDict.Keys)
                {
                    if (!incomingDict.ContainsKey(existing))
                    {
                        VariableManager.DeleteVariable(existing);
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse Globals message: {ex.Message}");
            }
        }

        private void OnAuthenticationMessageRecieved(object sender, string e)
        {
            try
            {
                var profile = _profileHandler.LoadProfile(_service.ProfileId);

                var jObject = JObject.Parse(e);
                if (jObject["request"]?.ToString() == "Hello")
                {
                    var info = jObject["info"];
                    if (info != null)
                    {
                        var connectionInfo = info.ToObject<ConnectionInfo>();
                        DataManager.SaveConnectionInfo(profile.Id, connectionInfo);
                    }

                    var authentication = jObject["authentication"];
                    if (authentication != null)
                    {
                        string salt = authentication["salt"]?.ToString();
                        string challenge = authentication["challenge"]?.ToString();
                        string password = profile.Password?.ToString();

                        _service.Authenticate(password, salt, challenge);
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket Hello message: {ex.Message}");
            }
        }

        private void OnHelloMessageRecieved(object sender, string e)
        {
            try
            {
                var profile = _profileHandler.LoadProfile(_service.ProfileId);

                var jObject = JObject.Parse(e);
                if (jObject["request"]?.ToString() == "Hello")
                {
                    var info = jObject["info"];
                    if (info != null)
                    {
                        var connectionInfo = info.ToObject<ConnectionInfo>();
                        DataManager.SaveConnectionInfo(profile.Id, connectionInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket Hello message: {ex.Message}");
            }
        }

        // Monitor connection status
        private void MonitorConnection(string profileId, string profileName, WebSocketService service)
        {
            service.Disconnected += async (sender, e) =>
            {
                if (service.IsIntentionalDisconnect)
                {
                    MacroDeckLogger.Info(PluginInstance.Main, $"Skipping reconnection for {profileName} (intentional disconnect)");
                    return;
                }

                if (!_connections.ContainsKey(profileId)) return;

                MacroDeckLogger.Info(PluginInstance.Main, $"Disconnected from {profileName}, attempting reconnection...");
                await Task.Delay(1000);

                if (_connections.ContainsKey(profileId))
                {
                    try
                    {
                        await ConnectServiceAsync(profileId).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        MacroDeckLogger.Error(PluginInstance.Main, $"Reconnection attempt failed for {profileName}: {ex.Message}");
                    }
                }
            };
        }

        // Close all active connections
        public void CloseAll()
        {
            var keys = _connections.Keys.ToList(); // Avoid collection modified exception
            foreach (var profileId in keys)
            {
                RemoveProfile(profileId);
            }
        }

        // Execute action if service is connected
        public void ExecuteIfConnected(string profileId, Action<WebSocketService> action)
        {
            if (_connections.TryGetValue(profileId, out var service) && service.IsConnected)
            {
                action(service);
            }
        }

        public IEnumerable<WebSocketService> GetAllServices() => _connections.Values;

        public bool HasConnection(string profileId) => _connections.ContainsKey(profileId);

        public bool IsProfileConnected(string profileId)
        {
            var service = GetServiceByProfileId(profileId);
            return service.IsConnected;
        }

        public WebSocketService GetServiceByProfileId(string profileId)
        {
            return _connections.TryGetValue(profileId, out var service) ? service : null;
        }

        public string GetProfileIdByService(WebSocketService service)
        {
            return _connections.FirstOrDefault(kvp => kvp.Value == service).Key;
        }

        public string GetProfileNameByService(WebSocketService service)
        {
            if (service == null) return null;
            var profileId = GetProfileIdByService(service);
            if (string.IsNullOrEmpty(profileId)) return null;
            var profile = _profileHandler.LoadProfile(profileId);
            return profile?.Name;
        }

        public int GetConnectionCount() => _connections.Values.Count(ws => ws.IsConnected);

        private void OnEventsList(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);
                if (json["id"]?.ToString() == "GetEventsForMacroDeck")
                {
                    var rawEvents = json["events"]?.ToObject<Dictionary<string, List<string>>>();
                    if (rawEvents != null)
                    {
                        if (!string.IsNullOrEmpty(_service.ProfileId))
                        {
                            DataManager.SaveEventsFromResponse(_service.ProfileId, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse Events message: {ex.Message}");
            }
        }

        private void OnEventFired(object sender, string e)
        {
            //try
            //{
            //    var json = JObject.Parse(e);
            //    if (json["id"]?.ToString() == "GetEventsForMacroDeck")
            //    {
            //        var events = json["events"]?.ToObject<Dictionary<string, List<EventWithSubscription>>>();
            //        if (events != null)
            //        {
            //            MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocket] Events count: {events.Count}");
            //            if (!string.IsNullOrEmpty(_service.ProfileId))
            //            {
            //                // Save entire events JSON string thread-safely
            //                DataManager.SaveEvents(_service.ProfileId, e);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse Event Fired message: {ex.Message}");
            //}
        }
    }
}

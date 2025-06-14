using MrVibesRSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Services
{
    public class SubscriptionEventArgs : EventArgs
    {
        public WebSocketService Service { get; set; }
        public string CategoryName { get; set; }
        public string EventName { get; set; }
    }
    public class SubscribedEventHandler
    {
        private readonly HashSet<WebSocketService> _subscribedInstances = new();
        public static SubscribedEventHandler Instance => _instance.Value;

        // Proper singleton implementation
        public static readonly Lazy<SubscribedEventHandler> _instance =
            new Lazy<SubscribedEventHandler>(() => new SubscribedEventHandler());

        private readonly Dictionary<string, Action<string, string>> _eventMessageHandlers = new();


        private WebSocketProfileManager _websocketProfileManager;
        private ProfileManager _profile;

        private event EventHandler<SubscriptionEventArgs> EventSubscribed;
        private event EventHandler<SubscriptionEventArgs> EventUnsubscribed;

        public SubscribedEventHandler()
        {
            _profile = new ProfileManager();
            _websocketProfileManager = WebSocketProfileManager.Instance;

            _eventMessageHandlers.Add("\"type\":\"ActionAdded\"", HandleActionAdded);
            _eventMessageHandlers.Add("\"type\":\"ActionUpdated\"", HandleActionUpdated);
            _eventMessageHandlers.Add("\"type\":\"ActionDeleted\"", HandleActionDeleted);

            _eventMessageHandlers.Add("\"type\":\"GlobalVariableUpdated\"", HandleGlobalVariableUpdated);

            _websocketProfileManager.ProfileAdded += OnProfileAddedEvent;
            _websocketProfileManager.ProfileRemoved += OnProfileRemovedEvent;

            EventSubscribed += OnEventSubscribed;
            EventUnsubscribed += OnEventUnsubscribed;
        }

        private async void OnProfileAddedEvent(object sender, WebSocketService service)
        {
            await Task.Delay(500);
            SubscribeToWebSocketEvents(service);
            SubscribeToSBEventOnStart(service);
            MacroDeckLogger.Info(PluginInstance.Main, $"Subscribed to WebSocket events for profile: {service.ProfileId}");
        }

        private void OnProfileRemovedEvent(object sender, WebSocketService service)
        {
            UnsubscribeFromWebSocketEvents(service);
            UnsubscribeToSBEventOnStart(service);
            MacroDeckLogger.Info(PluginInstance.Main, $"Unsubscribed from WebSocket events for profile: {service.ProfileId}");
        }

        private void SubscribeToWebSocketEvents(WebSocketService service)
        {
            if (service == null || _subscribedInstances.Contains(service)) return;

            service.MessageReceived_EventFired += OnEventFired;

            _subscribedInstances.Add(service);
        }

        private void UnsubscribeFromWebSocketEvents(WebSocketService service)
        {
            if (service == null || !_subscribedInstances.Contains(service)) return;

            service.MessageReceived_EventFired -= OnEventFired;

            _subscribedInstances.Remove(service);
        }

        private void OnEventFired(object sender, string message)
        {
            var service = (WebSocketService)sender;
            if (service == null) return;

            var profileId = service.ProfileId;
            MacroDeckLogger.Info(PluginInstance.Main, $"Event fired: {message}");

            bool handled = false;
            foreach (var kvp in _eventMessageHandlers)
            {
                if (message.Contains(kvp.Key))
                {
                    kvp.Value.Invoke(profileId, message);
                    handled = true;
                    break;
                }
            }

            if (!handled)
            {
                HandleGenericEvent(profileId, message);
            }
        }

        private void OnEventSubscribed(object sender, SubscriptionEventArgs e)
        {
            e.Service.SubscribeToStreamerbotEvents(e.CategoryName, e.EventName);
        }

        private void OnEventUnsubscribed(object sender, SubscriptionEventArgs e)
        {
            e.Service.UnsubscribeFromStreamerbotEvents(e.CategoryName, e.EventName);
        }

        public void TriggerEventSubscribed(WebSocketService service, string categoryName, string eventName)
        {
            var args = new SubscriptionEventArgs
            {
                Service = service,
                CategoryName = categoryName,
                EventName = eventName
            };

            EventSubscribed?.Invoke(this, args);
        }

        // Async version for non-blocking operations
        public async Task TriggerEventSubscribedAsync(WebSocketService service, string categoryName, string eventName)
        {
            await Task.Run(() => TriggerEventSubscribed(service, categoryName, eventName));
        }

        public void TriggerEventUnsubscribed(WebSocketService service, string categoryName, string eventName)
        {
            var args = new SubscriptionEventArgs
            {
                Service = service,
                CategoryName = categoryName,
                EventName = eventName
            };
            EventUnsubscribed?.Invoke(this, args);
        }

        // Async version for non-blocking operations
        public async Task TriggerEventUnsubscribedAsync(WebSocketService service, string categoryName, string eventName)
        {
            await Task.Run(() => TriggerEventUnsubscribed(service, categoryName, eventName));
        }

        public void SubscribeToSBEventOnStart(WebSocketService service)
        {
            var profiles = _profile.GetAllProfileSummaries();
            if (profiles == null) return;

            foreach (var profile in profiles)
            {
                if (!_websocketProfileManager.HasConnection(profile.Id)) continue;

                var eventList = DataManager.GetEvents(profile.Id);

                foreach (var category in eventList)
                {
                    string categoryName = category.Key;
                    List<string> subscribedEvents = category.Value
                        .Where(ev => ev.Subscribed)
                        .Select(ev => ev.Name)
                        .ToList();

                    if (subscribedEvents.Count > 0)
                    {
                        foreach (var subscribedEvent in subscribedEvents)
                        {
                            service.SubscribeToStreamerbotEvents(categoryName, subscribedEvent);
                        }
                    }
                }
            }
        }

        private void UnsubscribeToSBEventOnStart(WebSocketService service)
        {
            var profiles = _profile.GetAllProfileSummaries();
            if (profiles == null) return;

            foreach (var profile in profiles)
            {
                if (!_websocketProfileManager.HasConnection(profile.Id)) continue;

                var eventList = DataManager.GetEvents(profile.Id);

                foreach (var category in eventList)
                {
                    string categoryName = category.Key;
                    List<string> subscribedEvents = category.Value
                        .Where(ev => ev.Subscribed)
                        .Select(ev => ev.Name)
                        .ToList();

                    if (subscribedEvents.Count > 0)
                    {
                        foreach (var subscribedEvent in subscribedEvents)
                        {
                            service.UnsubscribeFromStreamerbotEvents(categoryName, subscribedEvent);
                        }
                    }
                }
            }
        }

        private void HandleActionAdded(string profileId, string json)
        {
            try
            {
                var jObj = JObject.Parse(json);
                var data = jObj["data"]?.ToObject<ActionItem>();
                if (data == null) return;

                var currentActions = DataManager.GetActionsList(profileId) ?? new List<ActionItem>();
                if (currentActions.Any(a => a.Id == data.Id)) return;

                currentActions.Add(data);
                DataManager.SaveActions(profileId, JsonConvert.SerializeObject(new { Actions = currentActions }));
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"HandleActionAdded failed: {ex.Message}");
            }
        }

        private void HandleActionUpdated(string profileId, string json)
        {
            try
            {
                var jObj = JObject.Parse(json);
                var updatedAction = jObj["data"]?.ToObject<ActionItem>();
                if (updatedAction == null) return;

                DataManager.UpdateAction(profileId, updatedAction.Id, updatedAction);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"HandleActionUpdated failed: {ex.Message}");
            }
        }

        private void HandleActionDeleted(string profileId, string json)
        {
            try
            {
                var jObj = JObject.Parse(json);
                var actionId = jObj["data"]?["id"]?.ToString();
                if (string.IsNullOrEmpty(actionId)) return;

                DataManager.DeleteAction(profileId, actionId);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"HandleActionDeleted failed: {ex.Message}");
            }
        }

        private void HandleGlobalVariableUpdated(string profileId, string message)
        {
            try
            {
                JObject json = JObject.Parse(message);
                var data = json["data"];
                if (data == null)
                    return;

                string key = data["name"]?.ToString();
                var newValue = data["newValue"];

                if (string.IsNullOrWhiteSpace(key) || newValue == null)
                    return;

                var service = _websocketProfileManager.GetServiceByProfileId(profileId);
                if (service == null) return;

                var profileName = _websocketProfileManager.GetProfileNameByService(service);
                string prefixedKey = $"{profileName}_{key}";

                VariableType type = VariableTypeHelper.GetVariableType(newValue);

                string groupName = $"GlobalVariables_{profileId}";
                VariableManager.SetValue(prefixedKey, newValue.ToString(), type, PluginInstance.Main, new string[] { groupName });
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to update global variable: {ex.Message}");
            }
        }

        private void HandleGenericEvent(string profileId, string message)
        {
            try
            {
                JObject json = JObject.Parse(message);
                JObject eventObj = json["event"] as JObject;
                JObject dataObj = json["data"] as JObject;
                string timestamp = json["timeStamp"]?.ToString() ?? DateTime.UtcNow.ToString("o");

                if (eventObj == null || dataObj == null) return;

                var service = _websocketProfileManager.GetServiceByProfileId(profileId);
                
                string eventType = eventObj["type"]?.ToString() ?? "UnknownType";
                string eventSource = eventObj["source"]?.ToString() ?? "UnknownSource";
                string profileName = _websocketProfileManager.GetProfileNameByService(service) ?? "UnknownProfile";

                string eventName = $"{eventSource}_{eventType}";
                string prefix = $"{profileName}_{eventName}";
                string group = $"{prefix}";

                foreach (var prop in dataObj.Properties())
                {
                    string key = $"{prefix}_{prop.Name}";
                    string value = prop.Value?.ToString() ?? "";
                    VariableType type = VariableTypeHelper.GetVariableType(prop.Value);

                    VariableManager.SetValue(key, value, type, PluginInstance.Main, new[] { group });
                }

                // Optional: Save full JSON as debug variable
                // VariableManager.SetValue($"{prefix}_RAW", dataObj.ToString(Formatting.None), VariableType.String, PluginInstance.Main, new[] { group });

                MacroDeckLogger.Info(PluginInstance.Main, $"[GenericEvent] {prefix} -> {dataObj.Properties().Count()} variables set.");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to handle dynamic event: {ex.Message}");
            }
        }
    }
}
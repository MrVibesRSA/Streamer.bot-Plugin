using MrVibesRSA.StreamerbotPlugin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MrVibesRSA.StreamerbotPlugin.Services
{
    public static class DataManager
    {
        private static readonly string DataDirectory;
        private static readonly Dictionary<string, object> FileLocks = new();

        static DataManager()
        {
            DataDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Macro Deck",
                "plugins",
                "MrVibesRSA.StreamerbotPlugin",
                "Data"
            );

            Directory.CreateDirectory(DataDirectory); // Ensure folder exists
        }

        private static object GetLockForProfile(string profileId)
        {
            lock (FileLocks)
            {
                if (!FileLocks.ContainsKey(profileId))
                    FileLocks[profileId] = new object();

                return FileLocks[profileId];
            }
        }

        #region "Connection Info"

        /// <summary>
        /// Saves the ConnectionInfo object under the "ConnectionInfo" section in {profileId}.json.
        /// Preserves all other existing sections (e.g., "Actions").
        /// </summary>
        public static void SaveConnectionInfo(string profileId, ConnectionInfo connectionInfo)
        {
            try
            {
                string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                // Load existing file or create new JObject
                JObject profileData = File.Exists(filePath)
                    ? JObject.Parse(File.ReadAllText(filePath))
                    : new JObject();

                // Set "ConnectionInfo" section
                profileData["ConnectionInfo"] = JObject.FromObject(connectionInfo);

                // Write updated content
                File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save connection info: {ex.Message}");
            }
        }

        public static ConnectionInfo GetConnectionInfo(string profileId)
        {
            try
            {
                string filePath = Path.Combine(DataDirectory, $"{profileId}.json");
                MacroDeckLogger.Info(PluginInstance.Main, $"Loading ConnectionInfo from: {filePath}");

                if (!File.Exists(filePath))
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"File does not exist: {filePath}");
                    return null;
                }

                var profileData = JObject.Parse(File.ReadAllText(filePath));

                var connToken = profileData["ConnectionInfo"];
                if (connToken == null)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"ConnectionInfo missing in file: {filePath}");
                    return null;
                }

                var deserialized = connToken.ToObject<ConnectionInfo>();
                if (deserialized == null)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to deserialize ConnectionInfo for profile ID: {profileId}");
                }

                return deserialized;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Exception in GetConnectionInfo: {ex.Message}");
                return null;
            }
        }


        #endregion

        #region "Actions"
        /// <summary>
        /// Saves the "actions" array under the "Actions" key in {profileId}.json.
        /// Preserves all other existing sections (e.g., "Globals").
        /// </summary>
        public static void SaveActions(string profileId, string webSocketMessage)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<GetActionsResponse>(webSocketMessage);
                    if (response?.Actions == null)
                        throw new ArgumentException("WebSocket message has no valid actions.");

                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");
                    JObject profileData = File.Exists(filePath)
                        ? JObject.Parse(File.ReadAllText(filePath))
                        : new JObject();

                    profileData["Actions"] = JArray.FromObject(response.Actions);

                    File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save actions: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Updates a specific action in the profile's Actions array
        /// </summary>
        public static bool UpdateAction(string profileId, string actionId, ActionItem updatedAction)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                    if (!File.Exists(filePath))
                        return false;

                    var profileData = JObject.Parse(File.ReadAllText(filePath));
                    var actions = profileData["Actions"] as JArray;
                    if (actions == null)
                        return false;

                    bool found = false;
                    for (int i = 0; i < actions.Count; i++)
                    {
                        if (actions[i]["id"]?.ToString() == actionId)
                        {
                            actions[i] = JObject.FromObject(updatedAction);
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                        return false;

                    File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to update action: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Deletes an action from the profile's Actions array by ID
        /// </summary>
        public static bool DeleteAction(string profileId, string actionId)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                    if (!File.Exists(filePath))
                        return false;

                    var profileData = JObject.Parse(File.ReadAllText(filePath));
                    var actions = profileData["Actions"] as JArray;

                    if (actions == null)
                        return false;

                    var actionToRemove = actions.FirstOrDefault(a => a["id"]?.ToString() == actionId);
                    if (actionToRemove == null)
                        return false;

                    actions.Remove(actionToRemove);
                    File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete action: {ex.Message}");
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets all actions from a profile's "Actions" section
        /// </summary>
        public static List<ActionItem> GetActionsList(string profileId)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                    if (!File.Exists(filePath))
                        return null;

                    var profileData = JObject.Parse(File.ReadAllText(filePath));
                    return profileData["Actions"]?.ToObject<List<ActionItem>>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to get action data: {ex.Message}");
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets a specific action by ID
        /// </summary>
        public static ActionItem GetAction(string profileId, string actionId)
        {
            var actions = GetActionsList(profileId);
            return actions?.FirstOrDefault(a => a.Id == actionId);
        }
        #endregion

        #region "Global Variables"

        public static void SaveGlobals(string profileId, Dictionary<string, string> globals)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");
        
                    JObject profileData = File.Exists(filePath)
                        ? JObject.Parse(File.ReadAllText(filePath))
                        : new JObject();
        
                    profileData["Globals"] = JObject.FromObject(globals);
        
                    File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save globals: {ex.Message}");
                }
            }
        }

        public static Dictionary<string, string> GetGlobalsList(string profileId)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                    if (!File.Exists(filePath))
                        return null;

                    var profileData = JObject.Parse(File.ReadAllText(filePath));
                    return profileData["Globals"]?.ToObject<Dictionary<string, string>>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to get globals: {ex.Message}");
                    return null;
                }
            }
        }

        public static bool DeleteGlobal(string profileId, string key)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");

                    if (!File.Exists(filePath))
                        return false;

                    var profileData = JObject.Parse(File.ReadAllText(filePath));
                    var globals = profileData["Globals"] as JObject;
                    if (globals == null || !globals.ContainsKey(key))
                        return false;

                    globals.Remove(key);

                    File.WriteAllText(filePath, profileData.ToString(Formatting.Indented));
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete global '{key}': {ex.Message}");
                    return false;
                }
            }
        }

        public static string GetGlobal(string profileId, string key)
        {
            var globals = GetGlobalsList(profileId); // Now returns Dictionary<string, string>
            return globals != null && globals.ContainsKey(key) ? globals[key] : null;
        }

        #endregion

        #region "Event Subscriptions"

        public static void SaveEventsFromResponse(string profileId, string webSocketMessage)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    var response = JsonConvert.DeserializeObject<GetEventsResponse>(webSocketMessage);
                    if (response?.Events == null)
                        throw new ArgumentException("WebSocket message has no valid events.");

                    var rawEvents = ConvertRawEvents(response.Events); // raw events from incoming data
                    var existingEvents = GetEvents(profileId); // events currently saved

                    var mergedEvents = MergeEvents(rawEvents, existingEvents);

                    SaveEvents(profileId, mergedEvents);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save events: {ex.Message}");
                }
            }
        }

        public static void SaveEvents(string profileId, Dictionary<string, List<EventWithSubscription>> events)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");
                    JObject eventData;

                    if (File.Exists(filePath))
                    {
                        var jsonText = File.ReadAllText(filePath);
                        eventData = JObject.Parse(jsonText);
                    }
                    else
                    {
                        eventData = new JObject();
                    }

                    eventData.Remove("Events");

                    eventData.Add("Events", JObject.FromObject(events));

                    File.WriteAllText(filePath, eventData.ToString(Formatting.Indented));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to save events: {ex.Message}");
                }
            }
        }

        private static Dictionary<string, List<EventWithSubscription>> ConvertRawEvents(Dictionary<string, List<string>> raw)
        {
            var result = new Dictionary<string, List<EventWithSubscription>>();

            foreach (var category in raw)
            {
                var list = new List<EventWithSubscription>();

                foreach (var eventName in category.Value)
                {
                    list.Add(new EventWithSubscription
                    {
                        Name = eventName,
                        Subscribed = ShouldBeDefaultSubscribed(category.Key, eventName)
                    });
                }

                result[category.Key] = list;
            }

            return result;
        }


        private static Dictionary<string, List<EventWithSubscription>> MergeEvents(
            Dictionary<string, List<EventWithSubscription>> incoming,
            Dictionary<string, List<EventWithSubscription>> existing)
        {
            var result = new Dictionary<string, List<EventWithSubscription>>();

            foreach (var category in incoming)
            {
                var mergedList = new List<EventWithSubscription>();

                foreach (var incomingEvent in category.Value)
                {
                    bool isSubscribed = existing.TryGetValue(category.Key, out var existingList)
                        && existingList.FirstOrDefault(e => e.Name == incomingEvent.Name)?.Subscribed == true;

                    mergedList.Add(new EventWithSubscription
                    {
                        Name = incomingEvent.Name,
                        Subscribed = isSubscribed || incomingEvent.Subscribed
                    });
                }

                result[category.Key] = mergedList;
            }

            return result;
        }

        public static void UpdataEventSubscription(string profileId, string category, string eventName, bool subscribed)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                var events = GetEvents(profileId); 

                if (events.TryGetValue(category, out var eventList))
                {
                    var targetEvent = eventList.FirstOrDefault(e => e.Name == eventName);
                    if (targetEvent != null)
                    {
                        targetEvent.Subscribed = subscribed;
                        SaveEvents(profileId, events); 
                    }
                }
            }
        }

        public static Dictionary<string, List<EventWithSubscription>> GetEvents(string profileId)
        {
            var profileLock = GetLockForProfile(profileId);
            lock (profileLock)
            {
                try
                {
                    string filePath = Path.Combine(DataDirectory, $"{profileId}.json");
                    if (!File.Exists(filePath))
                        return new Dictionary<string, List<EventWithSubscription>>();

                    var jsonText = File.ReadAllText(filePath);
                    var profileData = JObject.Parse(jsonText);

                    if (profileData["Events"] == null || profileData["Events"].Type != JTokenType.Object)
                        return new Dictionary<string, List<EventWithSubscription>>();

                    return profileData["Events"]
                        .ToObject<Dictionary<string, List<EventWithSubscription>>>() ??
                        new Dictionary<string, List<EventWithSubscription>>();
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to get events: {ex.Message}");
                    return new Dictionary<string, List<EventWithSubscription>>();
                }
            }
        }

        private static bool ShouldBeDefaultSubscribed(string category, string eventName)
        {
            // Example: auto-subscribe events
            return (category == "Misc" && eventName == "GlobalVariableUpdated") ||
                   (category == "Misc" && eventName == "UserGlobalVariableUpdated") ||
                   (category == "Application" && eventName == "ActionAdded") ||
                   (category == "Application" && eventName == "ActionUpdated") ||
                   (category == "Application" && eventName == "ActionDeleted");
        }

        #endregion
    }
}
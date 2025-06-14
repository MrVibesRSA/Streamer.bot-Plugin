using MrVibesRSA.StreamerbotPlugin.Models;
using SuchByte.MacroDeck.Plugins;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MrVibesRSA.StreamerbotPlugin.Services
{
    internal class ProfileManager
    {
        public void Save(string id, string name, string address, string port, string endpoint, string password, bool autoConnect)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                SaveProfileData(id, name, address, port, endpoint, password, autoConnect);
            }
            else
            {
                SaveProfileData(id, name, address, port, endpoint, password, autoConnect);
            }
        }

        private void SaveProfileData(string id, string name, string address, string port, string endpoint, string password, bool autoConnect)
        {
            var allProfiles = GetAllProfilesFlat();

            // Create or update profile object
            var profile = new ProfileConfig
            {
                Id = id,
                Name = name,
                Address = address,
                Port = port,
                Endpoint = endpoint,
                Password = password,
                AutoConnect = autoConnect
            };
        
            // Serialize the profile
            string json = JsonSerializer.Serialize(profile);
        
            // Update or add the profile in the dictionary
            allProfiles[id] = json;
        
            // Save the updated dictionary back to credentials
            PluginCredentials.SetCredentials(PluginInstance.Main, allProfiles);
        }

        public ProfileConfig LoadProfile(string id)
        {
            var credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);

            // Use LINQ to flatten and find the entry matching the given ID
            var entry = credentialsList
                .SelectMany(dict => dict)
                .FirstOrDefault(pair => pair.Key == id);

            if (!string.IsNullOrWhiteSpace(entry.Value))
            {
                return JsonSerializer.Deserialize<ProfileConfig>(entry.Value);
            }

            return null;
        }

        public void DeleteProfile(string id)
        {
            var allCredentials = GetAllProfilesFlat();

            if (allCredentials.Remove(id))
            {
                // Clear all credentials
                PluginCredentials.SetCredentials(PluginInstance.Main, new Dictionary<string, string>());

                // Re-save the remaining ones
                PluginCredentials.SetCredentials(PluginInstance.Main, allCredentials);
            }
        }

        public List<(string Id, string Name)> GetAllProfileSummaries()
        {
            var credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
        
            return credentialsList
                .SelectMany(dict => dict)
                .Select(pair =>
                {
                    try
                    {
                        var profile = JsonSerializer.Deserialize<ProfileConfig>(pair.Value);
                        return (pair.Key, profile?.Name ?? "[Unnamed]");
                    }
                    catch
                    {
                        return (pair.Key, "[Invalid Profile]");
                    }
                })
                .ToList();
        }

        private Dictionary<string, string> GetAllProfilesFlat()
        {
            var profilesList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
            var combined = new Dictionary<string, string>();

            if (profilesList == null)
                return combined;

            foreach (var dict in profilesList)
            {
                if (dict == null) continue;

                foreach (var kvp in dict)
                {
                    // Optionally, handle duplicates here if needed
                    combined[kvp.Key] = kvp.Value;
                }
            }

            return combined;
        }

        public List<ProfileConfig> GetAllProfiles()
        {
            var credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);

            return credentialsList
                .SelectMany(dict => dict)
                .Select(pair =>
                {
                    try
                    {
                        var profile = JsonSerializer.Deserialize<ProfileConfig>(pair.Value);
                        return profile;
                    }
                    catch
                    {
                        return null;
                    }
                })
                .Where(p => p != null)
                .ToList();
        }

    }
}

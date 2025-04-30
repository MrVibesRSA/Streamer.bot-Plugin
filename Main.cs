using MrVibes_RSA.StreamerbotPlugin.Actions;
using MrVibes_RSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Services;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace MrVibes_RSA.StreamerbotPlugin
{
    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        private ContentSelectorButton _statusButton = new();
        private MainWindow _mainWindow;
        private bool _autoConnect;
        private bool _autoConnectAttempted = false; // Add this field

        public static event EventHandler UpdateVariableList;

        public Main()
        {
            PluginInstance.Main ??= this;
        }

        // Optional; If your plugin can be configured, set to "true". It'll make the "Configure" button appear in the package manager.
        public override bool CanConfigure => true;

        // Gets called when the plugin is loaded
        public override void Enable()
        {
            this.Actions = new List<PluginAction>
            {
                // add the instances of your actions here
                new StreamerBotAction(),
            };


            WebSocketService.Instance.Connected += OnConnected;
            WebSocketService.Instance.Disconnected += OnDisconnect;
            WebSocketService.Instance.Error += OnError;
            WebSocketService.Instance.MessageReceived_Globals += Recieved_globals;
            WebSocketService.Instance.MessageReceived_GlobalUpdated += Updated_global;
            WebSocketService.Instance.MessageReceived_AuthenticateRequest += OnAuthenticateRequestReceived;

            MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            _mainWindow = sender as MainWindow;

            _statusButton = new ContentSelectorButton
            {
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Disconnected,
            };

            _statusButton.Click += StatusButton_Click;
            _mainWindow?.contentButtonPanel.Controls.Add(_statusButton);

            UpdateStatusIcon();

            _autoConnect = bool.TryParse(PluginConfiguration.GetValue(PluginInstance.Main, "AutoConnect") as string, out var result) && result;
            if (_autoConnect)
            {
                AutoConnect();
            }
        }

        private async void AutoConnect()
        {
            if (_autoConnectAttempted) return;
            _autoConnectAttempted = true;

            try
            {
                string address = PluginConfiguration.GetValue(PluginInstance.Main, "Address") ?? "127.0.0.1";
                string portString = PluginConfiguration.GetValue(PluginInstance.Main, "Port") ?? "8080";
                int port = int.Parse(portString);
                string endpoint = PluginConfiguration.GetValue(PluginInstance.Main, "Endpoint") ?? "/";

                var uriBuilder = new UriBuilder("ws", address, port, endpoint);

                await WebSocketService.Instance.StartAsync(uriBuilder.Uri.ToString());
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Auto-connect failed: {ex.Message}");
            }
            finally
            {
                _autoConnectAttempted = false;
            }
        }
        private void OnAuthenticateRequestReceived(object sender, string e)
        {
            if (_autoConnect)
            {
                try
                {
                    var jObject = JObject.Parse(e);
                    if (jObject["request"]?.ToString() == "Hello")
                    {
                        var authentication = jObject["authentication"];
                        if (authentication != null)
                        {
                            string salt = authentication["salt"]?.ToString();
                            string challenge = authentication["challenge"]?.ToString();

                            // Retrieve the credentials list securely
                            List<Dictionary<string, string>> credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
                            string password = string.Empty;

                            // Check if there are credentials and extract the password
                            if (credentialsList != null && credentialsList.Count > 0)
                            {
                                var credentials = credentialsList.FirstOrDefault();
                                if (credentials != null && credentials.ContainsKey("password"))
                                {
                                    password = credentials["password"];
                                }
                            }

                            WebSocketService.Instance.Authenticate(password, salt, challenge);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket Hello message: {ex.Message}");
                }
            }
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            OpenConfigurator();
        }

        // Optional; Gets called when the user clicks on the "Configure" button in the package manager; If CanConfigure is not set to true, you don't need to add this
        public override void OpenConfigurator()
        {
            using var pluginConfig = new PluginConfig();
            pluginConfig.ShowDialog();
        }

        private void UpdateStatusIcon()
        {

            if (_mainWindow != null && !_mainWindow.IsDisposed && _statusButton != null && !_statusButton.IsDisposed)
            {
                _mainWindow.Invoke(() =>
                {
                    _statusButton.BackgroundImage = WebSocketService.Instance.IsConnected ? MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Connected : MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Disconnected;
                });
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _statusButton.BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Connected;
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            _statusButton.BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Disconnected;
        }

        private void OnError(object sender, string e)
        {
           _statusButton.BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Error;
        }

        private void Recieved_globals(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);

                if (json["id"]?.ToString() != "GetGlobalsForMacroDeck")
                    return;

                // Parse the variables from JSON
                var variablesJson = json["variables"] as JObject;
                if (variablesJson == null)
                    return;

                // Fetch existing variables from Macro Deck
                var existingVariables = VariableManager.GetVariables(PluginInstance.Main) ?? new List<Variable>();

                string groupName = "Global_SB_Variables";

                // Build dictionaries for easier lookup
                var existingDict = existingVariables.ToDictionary(v => v.Name, v => v);
                var incomingDict = new Dictionary<string, JToken>();

                foreach (var prop in variablesJson.Properties())
                {
                    incomingDict[prop.Name] = prop.Value["value"];
                }

                // Handle added or updated variables
                foreach (var kvp in incomingDict)
                {
                    string key = kvp.Key;
                    var value = kvp.Value;

                    if (!existingDict.ContainsKey(key))
                    {
                        // New variable -> Add it
                        VariableType type = VariableTypeHelper.GetVariableType(value);
                        VariableManager.SetValue(key, value.ToString(), type, PluginInstance.Main, new string[] { groupName });
                    }
                    else
                    {
                        // Existing variable -> Check if value needs update
                        var existingVar = existingDict[key];
                        if (existingVar.Value != value.ToString())
                        {
                            VariableType type = VariableTypeHelper.GetVariableType(value);
                            VariableManager.SetValue(key, value.ToString(), type, PluginInstance.Main, new string[] { groupName });
                        }
                    }
                }

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
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to sync globals: {ex.Message}");
            }
        }

        private void Updated_global(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);

                var data = json["data"];
                if (data == null)
                    return;

                string key = data["name"]?.ToString();
                var newValue = data["newValue"];

                if (string.IsNullOrEmpty(key) || newValue == null)
                    return;

                string groupName = "Global_SB_Variables";

                VariableType type = VariableTypeHelper.GetVariableType(newValue);
                VariableManager.SetValue(key, newValue.ToString(), type, PluginInstance.Main, new string[] { groupName });
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to update global variable: {ex.Message}");
            }
        }
    }
}

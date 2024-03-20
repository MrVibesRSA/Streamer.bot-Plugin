using MrVibes_RSA.StreamerbotPlugin.Actions;
using MrVibes_RSA.StreamerbotPlugin.GUI;
using MrVibes_RSA.StreamerbotPlugin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.DataTypes.Updater;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic.Logging;
using SuchByte.MacroDeck;
using WebSocketSharp;

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

            MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;
            WebSocketClient.WebSocketOnMessageRecieved_globals += WebSocketClient_WebSocketOnMessageRecieved_globals;
            WebSocketClient.WebSocketConnected += OnConnected;
            WebSocketClient.WebSocketDisconnected += OnDisconnect;
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
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            OpenConfigurator();
        }

        private void UpdateStatusIcon()
        {

            if (_mainWindow != null && !_mainWindow.IsDisposed && _statusButton != null && !_statusButton.IsDisposed)
            {
                _mainWindow.Invoke(() =>
                {
                    _statusButton.BackgroundImage = WebSocketClient.IsConnected ? MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Connected : MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Disconnected;
                });
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _statusButton.BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Connected;
        }

        private void OnDisconnect(object sender, CloseEventArgs e)
        {
            _statusButton.BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_Disconnected;
        }

        private void WebSocketClient_WebSocketOnMessageRecieved_globals(object sender, string e)
        {
            string json = e.ToString();

            // Parse the JSON string
            JObject jsonObject = JObject.Parse(json);

            // Access the key and value from the data section
            JObject dataObject = (JObject)jsonObject["data"];
            string key = dataObject.Properties().First().Name;
            string value = (string)dataObject[key];  

            // Retrieve the current global variables from configuration
            string globalVariables = PluginConfiguration.GetValue(PluginInstance.Main, "sb_globals");

            if (globalVariables == null || globalVariables == string.Empty)
            {
                // If no global variables exist yet, create a new dictionary
                var globals = new Dictionary<string, string>
                {
                    { key, value }
                };

                // Save the dictionary as a JSON string to configuration
                PluginConfiguration.SetValue(PluginInstance.Main, "sb_globals", JsonConvert.SerializeObject(globals));
                UpdateVariableList?.Invoke(this, EventArgs.Empty);
                return;
            }
            else
            {
                // If global variables exist, deserialize them from JSON
                JObject globalVariablesObject = JObject.Parse(globalVariables);
                Dictionary<string, string> globalVariablesDict = globalVariablesObject.ToObject<Dictionary<string, string>>();

                // Check if the new key already exists in the global variables dictionary
                if (!globalVariablesDict.ContainsKey(key))
                {
                    // Append the new key-value pair
                    globalVariablesDict.Add(key, value);

                    JObject updatedGlobalVariablesObject = JObject.FromObject(globalVariablesDict);
                    PluginConfiguration.SetValue(PluginInstance.Main, "sb_globals", updatedGlobalVariablesObject.ToString());
                    UpdateVariableList?.Invoke(this, EventArgs.Empty);
                    return;
                }
                else
                {
                    // Update the existing value
                    globalVariablesDict[key] = value;
                    JObject updatedGlobalVariablesObject = JObject.FromObject(globalVariablesDict);

                    PluginConfiguration.SetValue(PluginInstance.Main, "sb_globals", updatedGlobalVariablesObject.ToString());

                    var whiteList = PluginConfiguration.GetValue(PluginInstance.Main, "CheckboxState");
                    List<Variable> variables = JsonConvert.DeserializeObject<List<Variable>>(whiteList);

                    foreach (var variable in variables)
                    {
                        if (variable.VariableName.ToLower() == key.ToLower() && variable.IsChecked)
                        {
                            // Your code logic here
                            VariableType type = VariableTypeHelper.GetVariableType(value);
                            VariableManager.SetValue(key, value, type, PluginInstance.Main, new string[] { "Gobal Variables" });
                            MacroDeckLogger.Info(PluginInstance.Main, $"Updating global variables: Key:{key} Value: {value}");
                        }
                    }
                    UpdateVariableList?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // Optional; Gets called when the user clicks on the "Configure" button in the package manager; If CanConfigure is not set to true, you don't need to add this
        public override void OpenConfigurator()
        {
            using var pluginConfig = new PluginConfig();
            pluginConfig.ShowDialog();
        }
    }

    public class Variable
    {
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public bool IsChecked { get; set; }
    }
}

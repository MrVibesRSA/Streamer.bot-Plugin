using MrVibesRSA.StreamerbotPlugin.Services;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    public partial class StreamerBotActionConfigurator : ActionConfigControl
    {

        // Add a variable for the instance of your action to get access to the Configuration etc.
        private PluginAction _macroDeckAction;
        private WebSocketProfileManager _WebsocketProfileManager;
        private ProfileManager _profile;

        private List<MrVibesRSA.StreamerbotPlugin.Models.ActionItem> _currentActionsList = new List<MrVibesRSA.StreamerbotPlugin.Models.ActionItem>();

        public StreamerBotActionConfigurator(PluginAction macroDeckAction, ActionConfigurator actionConfigurator)
        {
            this._macroDeckAction = macroDeckAction;
            InitializeComponent();

            _WebsocketProfileManager = WebSocketProfileManager.Instance;
            _profile = new ProfileManager();

            ErrorPanelVisible(false,"");
            LoadActionConfiguration();
        }

        private void LoadActionConfiguration()
        {
            if (!PopulateProfileComboBox())
            {
                ErrorPanelVisible(true, "\"No connected profiles found...\"");
                return;
            }

            var config = _macroDeckAction.Configuration;
            if (string.IsNullOrEmpty(config))
            {
                comboBox_SelectedProfile.SelectedIndex = 0;
                return;
            }

            try
            {
                var jConfig = JObject.Parse(config);
                var profileId = jConfig["profileId"]?.ToString();
                var actionId = jConfig["actionId"]?.ToString();

                if (string.IsNullOrEmpty(profileId))
                {
                    comboBox_SelectedProfile.SelectedIndex = 0;
                    return;
                }

                var profileExists = comboBox_SelectedProfile.Items.Cast<ComboBoxItemHelper>()
                    .Any(item => item.Id?.ToString() == profileId);

                if (!profileExists)
                {
                    ShowErrorMessage($"Profile ID '{profileId}' not found. Loading default");
                    MacroDeckLogger.Warning(PluginInstance.Main, $"Profile ID '{profileId}' not found in list");
                    comboBox_SelectedProfile.SelectedIndex = 0;
                    return;
                }

                // Set the selected profile first
                foreach (ComboBoxItemHelper item in comboBox_SelectedProfile.Items)
                {
                    if (item.Id.ToString() == profileId)
                    {
                        comboBox_SelectedProfile.SelectedItem = item;
                        break;
                    }
                }

                if (comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedProfile)
                {
                    if (TryGetActionsList(selectedProfile.Id.ToString(), out var actionsList))
                    {
                        _currentActionsList = actionsList;
                        PopulateActionGroupComboBox(_currentActionsList);
                        PopulateActionList(_currentActionsList);

                        // Try selecting the saved action
                        var match = _currentActionsList.FirstOrDefault(a => a.Id == actionId);
                        if (match != null)
                        {
                            foreach (ComboBoxItemHelper item in comboBox_ActionList.Items)
                            {
                                if (item.Id.ToString() == actionId)
                                {
                                    comboBox_ActionList.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Failed to load action list for selected profile");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error loading profile configuration");
                MacroDeckLogger.Error(PluginInstance.Main, $"Configuration parse error: {ex.Message}. Loading default profile");
                comboBox_SelectedProfile.SelectedIndex = 0;
            }
        }

        private bool PopulateProfileComboBox()
        {
            try
            {
                var profileList = _profile.GetAllProfiles();
                if (profileList == null || !profileList.Any())
                {
                    MacroDeckLogger.Warning(PluginInstance.Main, "No connected profiles found when populating combo box");
                    return false;
                }

                var comboBoxItems = profileList
                    .Where(p => WebSocketProfileManager.Instance.HasConnection(p.Id))
                    .Select(p => new ComboBoxItemHelper(p.Name, p.Id, true))
                    .ToList();

                comboBox_SelectedProfile.DataSource = comboBoxItems;
                comboBox_SelectedProfile.DisplayMember = "Name";
                comboBox_SelectedProfile.ValueMember = "Id";

                return true;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to populate profile combo box: {ex.Message}");
                return false;
            }
        }

        private void PopulateActionGroupComboBox(List<MrVibesRSA.StreamerbotPlugin.Models.ActionItem> actionList)
        {
            try
            {
                var grouped = actionList
                    .Where(a => !string.IsNullOrWhiteSpace(a.Group))
                    .Select(a => a.Group)
                    .Distinct()
                    .OrderBy(g => g)
                    .ToList();

                bool hasUngrouped = actionList.Any(a => string.IsNullOrWhiteSpace(a.Group));

                comboBox_ActionGroup.Items.Clear();
                comboBox_ActionGroup.Items.Add("All");

                if (hasUngrouped)
                {
                    comboBox_ActionGroup.Items.Add("Ungrouped");
                }

                comboBox_ActionGroup.Items.AddRange(grouped.ToArray());
                comboBox_ActionGroup.SelectedItem = "All";
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Unexpected error populating action groups: {ex.Message}");
            }
        }

        private void PopulateActionList(List<MrVibesRSA.StreamerbotPlugin.Models.ActionItem> actions)
        {
            string selectedGroup = comboBox_ActionGroup.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGroup)) return;

            var filteredActions = selectedGroup switch
            {
                "All" => actions,
                "Ungrouped" => actions
                    .Where(a => string.IsNullOrWhiteSpace(a.Group))
                    .OrderBy(a => a.Name)
                    .ToList(),
                _ => actions
                    .Where(a => a.Group == selectedGroup)
                    .OrderBy(a => a.Name)
                    .ToList()
            };

            comboBox_ActionList.Items.Clear();

            foreach (var action in filteredActions)
            {
                comboBox_ActionList.Items.Add(new ComboBoxItemHelper(action.Name, action.Id));
            }

            if (comboBox_ActionList.Items.Count > 0)
            {
                comboBox_ActionList.SelectedIndex = 0;
            }
        }

        private void comboBox_SelectedProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedProfile)
            {
                if (!TryGetActionsList(selectedProfile.Id.ToString(), out var actionList))
                {
                    /// Show error message and log
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to retrieve actions list for profile {selectedProfile.Name} ({selectedProfile.Id})");
                    return;
                }

                _currentActionsList = actionList;
                PopulateActionGroupComboBox(actionList);
                PopulateActionList(actionList);
            }
        }

        private void comboBox_ActionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedProfile &&
            TryGetActionsList(selectedProfile.Id.ToString(), out var actionList))
            {
                PopulateActionList(actionList);
            }
        }

        private void comboBox_ActionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_ActionList.SelectedItem is not ComboBoxItemHelper selectedItem) return;

            var selectedAction = _currentActionsList.FirstOrDefault(a => a.Id == selectedItem.Id.ToString());
            if (selectedAction == null) return;

            label_actionId.Text = selectedAction.Id;
            label_actionEnabled.Text = selectedAction.Enabled.ToString();
            label_subactionCount.Text = selectedAction.SubactionCount.ToString();
            label_triggerCount.Text = selectedAction.TriggerCount.ToString();
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            var selectedProfileItem = comboBox_SelectedProfile.SelectedItem as ComboBoxItemHelper;
            var service = _WebsocketProfileManager.GetServiceByProfileId(selectedProfileItem?.Id.ToString());
            if (service == null) return;

            service.GetActionsList();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox.SelectedText);
        }

        private void pastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox.SelectAll();
        }

        private void formatJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var formattedJson = FormatJson(textBox.Text);
                textBox.Text = formattedJson;
            }
            catch (Exception)
            {
                ShowErrorMessage("Invalid JSON for formatting.");
            }
        }

        private void validateJsonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var isValid = ValidateJson(textBox.Text);
                ShowErrorMessage(isValid ? "JSON is valid!" : "Invalid JSON!");
            }
            catch (Exception)
            {
                ShowErrorMessage("Invalid JSON format.");
            }
        }

        private string FormatJson(string json)
        {
            var parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private bool ValidateJson(string json)
        {
            try
            {
                JsonConvert.DeserializeObject(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void ShowErrorMessage(string message)
        {
            using var error = new CustomDialog(message);
            error.ShowDialog();
        }

        public override bool OnActionSave()
        {
            if (comboBox_SelectedProfile.SelectedItem is not ComboBoxItemHelper selectedProfileItem)
                return false;

            if (comboBox_ActionList.SelectedItem is not ComboBoxItemHelper selectedItem)
                return false;

            try
            {
                // Find the selected action from the current list
                var selectedAction = _currentActionsList.FirstOrDefault(a => a.Id == selectedItem.Id.ToString());
                if (selectedAction == null)
                    return false;

                JObject configuration = new JObject
                {
                    ["profileId"] = selectedProfileItem.Id.ToString(),
                    ["profile"] = selectedProfileItem.Name,
                    ["actionId"] = selectedAction.Id,
                    ["actionName"] = selectedAction.Name,
                    ["actionArgument"] = textBox.Text,
                    ["actionGroup"] = selectedAction.Group,
                    ["actionEnabled"] = selectedAction.Enabled,
                    ["actionSubactionCount"] = selectedAction.SubactionCount,
                    ["actionTriggerCount"] = selectedAction.TriggerCount
                };

                string summary = string.IsNullOrEmpty(configuration["actionArgument"]?.ToString())
                    ? $"Profile - '{configuration["profile"]}', Action Name - '{configuration["actionName"]}'"
                    : $"Profile - '{configuration["profile"]}', Action Name - '{configuration["actionName"]}', Value - '{configuration["actionArgument"]}'";

                this._macroDeckAction.ConfigurationSummary = summary;
                this._macroDeckAction.Configuration = configuration.ToString();

                return true;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Error while saving action: {ex.Message}");
                return false;
            }
        }

        private bool TryGetActionsList(string profileId, out List<MrVibesRSA.StreamerbotPlugin.Models.ActionItem> actionsList)
        {
            actionsList = null;

            try
            {
                actionsList = DataManager.GetActionsList(profileId);
                return true;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Warning(PluginInstance.Main, $"Failed to get actions list for profile {profileId}: {ex.Message}");
                return false;
            }
        }

        public void ErrorPanelVisible(bool visible,string error)
        {
            label_Error.Text = error;
            errorPanel.Visible = visible;
            if (visible)
            {
                errorPanel.BringToFront();
            }
            else
            {
                errorPanel.SendToBack();
            }
        }
    }
}
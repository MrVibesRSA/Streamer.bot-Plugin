using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.Services;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MrVibes_RSA.StreamerbotPlugin.GUI
{
    public partial class StreamerBotActionConfigurator : ActionConfigControl
    {
        // Add a variable for the instance of your action to get access to the Configuration etc.
        private PluginAction _macroDeckAction;
        private WebSocketService webSocketService = WebSocketService.Instance;
        private GetActionsResponse _actionsData;

        private bool isComboBoxPopulated = false;
        private Timer connectionCheckTimer;

        public StreamerBotActionConfigurator(PluginAction macroDeckAction, ActionConfigurator actionConfigurator)
        {
            this._macroDeckAction = macroDeckAction;
            InitializeComponent();
            InitializeConnectionCheck();
            webSocketService.MessageReceived_Actions += WebSocketService_MessageReceived_Actions;
            webSocketService.GetActionsList();
        }

        private void InitializeConnectionCheck()
        {
            connectionCheckTimer = new Timer();
            connectionCheckTimer.Interval = 5000; // 5 seconds delay
            connectionCheckTimer.Tick += ConnectionCheckTimer_Tick;
        }

        private void WebSocketService_MessageReceived_Actions(object sender, string e)
        {
            isComboBoxPopulated = false; // Reset the flag before populating
            PopulateActionGroupComboBox(e);
            connectionCheckTimer.Start(); // Start the timer to monitor the population status
        }

        private void OnActionLoad()
        {
            // If _actionsData is null or empty, log an error and return early
            if (_actionsData == null || _actionsData.actions == null || !_actionsData.actions.Any())
            {
                MacroDeckLogger.Warning(PluginInstance.Main, "No actions found in _actionsData.");
                return;
            }

            // Check if the action list still contains the selected action from the configuration
            var selectedActionId = _macroDeckAction.Configuration != null
                ? JObject.Parse(_macroDeckAction.Configuration)["actionId"]?.ToString()
                : null;

            // Set the selected item in the comboBox based on the configuration's action ID
            var selectedActionItem = comboBox_ActionList.Items
                .OfType<ComboBoxItemHelper>()
                .FirstOrDefault(item => item.Value.ToString() == selectedActionId);

            if (selectedActionItem != null)
            {
                comboBox_ActionList.SelectedItem = selectedActionItem;
            }
            else
            {
                // If the selected action is not in the combo box anymore, log a warning
                // MacroDeckLogger.Warning(PluginInstance.Main, $"Selected action with ID '{selectedActionId}' is no longer in the list.");
            }
        }

        private void ConnectionCheckTimer_Tick(object sender, EventArgs e)
        {
            // Stop the timer since we've checked
            connectionCheckTimer.Stop();

            // If the ComboBox is not populated, it could indicate a connection issue
            if (!isComboBoxPopulated)
            {
                // Show the error panel
                errorPanel.Visible = true;
            }
        }

        private void PopulateActionGroupComboBox(string e)
        {
            _actionsData = JsonConvert.DeserializeObject<GetActionsResponse>(e);
            comboBox_ActionGroup.Items.Clear();

            var actionGroups = _actionsData.actions
                .Select(a => a.group)
                .Distinct()
                .OrderBy(g => g)
                .ToList();

            comboBox_ActionGroup.Items.Add("All"); // Add "All" at the top
            comboBox_ActionGroup.Items.AddRange(actionGroups.ToArray());
            comboBox_ActionGroup.SelectedItem = "All";

            isComboBoxPopulated = true;
            errorPanel.Visible = false;
            OnActionLoad();
        }

        private void comboBox_ActionGroup_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            PopulateActionList();
        }

        private void comboBox_ActionList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var selectedItem = comboBox_ActionList.SelectedItem as ComboBoxItemHelper;
            if (selectedItem == null) return;

            // Find the full action object using the ID
            var selectedAction = _actionsData.actions.FirstOrDefault(a => a.id == selectedItem.Value.ToString());
            if (selectedAction == null) return;

            // Now you have full details
            string actionName = selectedAction.name;
            bool isEnabled = selectedAction.enabled;
            int subActionCount = selectedAction.subaction_count;
            int triggerCount = selectedAction.trigger_count;

            label_actionId.Text = selectedAction.id;
            label_actionEnabled.Text = isEnabled.ToString();
            label_subactionCount.Text = subActionCount.ToString();
            label_triggerCount.Text = triggerCount.ToString();
        }

        private void PopulateActionList()
        {
            var selectedGroup = comboBox_ActionGroup.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedGroup)) return;

            var filteredActions = _actionsData.actions
                .Where(a => selectedGroup == "All" || a.group == selectedGroup)
                .OrderBy(a => a.name)
                .ToList();

            comboBox_ActionList.Items.Clear();

            foreach (var action in filteredActions)
            {
                comboBox_ActionList.Items.Add(new ComboBoxItemHelper(action.name, action.id));
            }

            if (comboBox_ActionList.Items.Count > 0)
            {
                comboBox_ActionList.SelectedIndex = 0;
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            webSocketService.GetActionsList();
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
            using var error = new ErrorMessage(message);
            error.ShowDialog();
        }

        public override bool OnActionSave()
        {
            // Step 1: Check if a valid action is selected
            var selectedItem = comboBox_ActionList.SelectedItem as ComboBoxItemHelper;
            if (selectedItem == null)
            {
                // No action selected, return false
                return false;
            }

            try
            {
                // Step 2: Find the full action object using the ID from selected item
                var selectedAction = _actionsData.actions.FirstOrDefault(a => a.id == selectedItem.Value.ToString());
                if (selectedAction == null)
                {
                    // Action was not found, return false
                    return false;
                }

                // Step 3: Create configuration JObject to save action details
                JObject configuration = new JObject
                {
                    ["actionId"] = selectedAction.id,
                    ["actionName"] = selectedAction.name,
                    ["actionArgument"] = textBox.Text,
                    ["actionGroup"] = selectedAction.group,
                    ["actionEnabled"] = selectedAction.enabled,
                    ["actionSubactionCount"] = selectedAction.subaction_count,
                    ["actionTriggerCount"] = selectedAction.trigger_count
                };

                // Step 4: Create a summary based on the action's name and argument
                string summary = string.IsNullOrEmpty(configuration["actionArgument"]?.ToString())
                    ? $"Name - '{configuration["actionName"]}'"
                    : $"Name - '{configuration["actionName"]}', Value - '{configuration["actionArgument"]}'";

                // Step 5: Save the summary and configuration
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
    }
}
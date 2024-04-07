using MrVibes_RSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Models;
using Newtonsoft.Json;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using SuchByte.MacroDeck.Variables;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebSocketSharp;

namespace MrVibes_RSA.StreamerbotPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        private List<Tuple<string, string>> selectedVariables = new List<Tuple<string, string>>();
        private List<CheckboxState> checkboxStates = new List<CheckboxState>();

        public PluginConfig()
        {
            InitializeComponent();
            checkboxColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            var address = PluginConfiguration.GetValue(PluginInstance.Main, "Address") ?? "127.0.0..1";
            var port = PluginConfiguration.GetValue(PluginInstance.Main, "Port") ?? "8080";
            var endpoint = PluginConfiguration.GetValue(PluginInstance.Main, "Endpoint") ?? "/";

            textBox_Address.Text = address;
            textBox_Port.Text = port;
            textBox_Endpoint.Text = endpoint;

            // Set up tooltip for PictureBox
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(btn_Connect, "Connect to Streamer.bot's Websocked Server.");
            toolTip.SetToolTip(buttonPrimary1, "Copy Streamer.bot action import code");


            //Import Code for streamer.bot
            roundedTextBox3.Text = "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";
            // Assuming WebSocketClient has a property named State indicating the connection state
            btn_Connect.Text = WebSocketClient.IsConnected ? "Disconnect" : "Connect";

            WebSocketClient.WebSocketConnected += OnConnected;
            WebSocketClient.WebSocketDisconnected += OnDisconnect;

            // Subscribe to the UpdateVariableList event
            Main.UpdateVariableList += HandleUpdateVariableList;

            PopulateDataGridView();
            LoadCheckboxState();
        }

        private void HandleUpdateVariableList(object sender, EventArgs e)
        {
            PopulateDataGridView();

           // dataGridView1.Invalidate();
           // dataGridView1.Update();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (textBox_Address.Text == "")
            {
                using var error = new ErrorMessage("Please enter ip/address.");
                error.ShowDialog();
                return;
            }

            if (textBox_Port.Text == "")
            {
                using var error = new ErrorMessage("please enter port.");
                error.ShowDialog();
                return;
            }

            var webSocketClient = WebSocketClient.Instance;

            string address = textBox_Address.Text;
            int port;
            string endpoint = textBox_Endpoint.Text;
            Uri serverUri = null;

            if (int.TryParse(textBox_Port.Text, out port))
            {
                // Port parsing successful, use the 'port' variable here
                UriBuilder uriBuilder = new UriBuilder("ws", address, port, endpoint);
                serverUri = uriBuilder.Uri;

                PluginConfiguration.SetValue(PluginInstance.Main, "Address", textBox_Address.Text);
                PluginConfiguration.SetValue(PluginInstance.Main, "Port", textBox_Port.Text);
                PluginConfiguration.SetValue(PluginInstance.Main, "Endpoint", textBox_Endpoint.Text);
            }
            else
            {
                using var error = new ErrorMessage("Invalid port number entered.");
                error.ShowDialog();
                return;
            }

            try
            {
                if (btn_Connect.Text == "Connect")
                {
                    btn_Connect.Text = "Disconnect";
                    webSocketClient.Connect(serverUri.ToString());
                }
                else if (btn_Connect.Text == "Disconnect")
                {
                    webSocketClient.Close();
                    btn_Connect.Text = "Connect";
                    PluginConfiguration.SetValue(PluginInstance.Main, "Configured", "False");
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"Error: {ex.Message}");
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            PluginConfiguration.SetValue(PluginInstance.Main, "Configured", "True");
            btn_Connect.Text = "Disconnect";
            this.Invalidate();
            this.Update();
        }

        private void OnDisconnect(object sender, CloseEventArgs e)
        {
            btn_Connect.Text = "Connect";
            this.Invalidate();
            this.Update();

            if(e.Code == 1006)
            {
                using var error = new ErrorMessage("Not Connecting to Streamer.bot" +
                    "\nMake sure Streamer.bot and Websocket Server is running" );
                error.ShowDialog();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["checkBoxColumn"].Index && e.RowIndex >= 0)
            {
                DataGridViewCheckBoxCell checkBoxCell = dataGridView1.Rows[e.RowIndex].Cells["checkBoxColumn"] as DataGridViewCheckBoxCell;

                // Get the state before the click
                bool prevState = Convert.ToBoolean(checkBoxCell.Value);

                // Toggle the checkbox state
                checkBoxCell.Value = !(prevState);

                // Get the current state after the click
                bool currentState = Convert.ToBoolean(checkBoxCell.Value);

                // Get the variable name and value
                string variableName = dataGridView1.Rows[e.RowIndex].Cells["VariableName"].Value.ToString();
                string variableValue = dataGridView1.Rows[e.RowIndex].Cells["VariableValue"].Value.ToString();

                if (currentState)
                {
                    // Checkbox is checked
                    selectedVariables.Add(new Tuple<string, string>(variableName, variableValue));
                    VariableType type = VariableTypeHelper.GetVariableType(variableValue);

                    VariableManager.SetValue(variableName, variableValue, type, PluginInstance.Main, new string[] { "Gobal Variables" });
                }
                else
                {
                    // Checkbox is unchecked
                    selectedVariables.RemoveAll(v => v.Item1 == variableName && v.Item2 == variableValue);
                    VariableManager.DeleteVariable(variableName.ToLower());
                }

                // Save checkbox state
                SaveCheckboxState();
            }
        }


        private void SaveCheckboxState()
        {
            // If checkboxStates is null, initialize it
            if (checkboxStates == null)
            {
                checkboxStates = new List<CheckboxState>();
            }

            // Clear existing checkbox states
            checkboxStates.Clear();

            // Add current checkbox states
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["checkBoxColumn"].Value);
                string variableName = row.Cells["VariableName"].Value.ToString();
                string variableValue = row.Cells["VariableValue"].Value.ToString();

                checkboxStates.Add(new CheckboxState
                {
                    IsChecked = isChecked,
                    VariableName = variableName,
                    VariableValue = variableValue
                });
            }

            // Serialize and save checkbox states
            string json = JsonConvert.SerializeObject(checkboxStates, Formatting.Indented);
            PluginConfiguration.SetValue(PluginInstance.Main, "CheckboxState", json);
        }

        private void LoadCheckboxState()
        {
            string json = PluginConfiguration.GetValue(PluginInstance.Main, "CheckboxState");

            if (!string.IsNullOrEmpty(json))
            {
                checkboxStates = JsonConvert.DeserializeObject<List<CheckboxState>>(json);

                if (checkboxStates.Count > 0)
                {
                    // Set checkbox states based on loaded data
                    foreach (var state in checkboxStates)
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells["VariableName"].Value.ToString() == state.VariableName)
                            {
                                row.Cells["checkBoxColumn"].Value = state.IsChecked;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void PopulateDataGridView()
        {
            string globalVariables = PluginConfiguration.GetValue(PluginInstance.Main, "sb_globals");

            if (!string.IsNullOrEmpty(globalVariables))
            {
                // If global variables exist, deserialize them from JSON
                var existingGlobals = JsonConvert.DeserializeObject<Dictionary<string, string>>(globalVariables);

                if (existingGlobals != null)
                {
                    // Clear existing rows in the DataGridView
                    dataGridView1.Rows.Clear();

                    foreach (var kvp in existingGlobals)
                    {
                        // Add a new row to the DataGridView
                        dataGridView1.Rows.Add();

                        // Set the key and value in the appropriate columns
                        int rowIndex = dataGridView1.Rows.Count - 1; // Index of the newly added row
                        var formattedKey = kvp.Key;

                        if (!kvp.Key.StartsWith("sb_global_"))
                        {
                            if (kvp.Key.StartsWith("global_"))
                            {
                                formattedKey = ("sb_" + kvp.Key);
                            }
                            else
                            {
                                formattedKey = ("sb_global_" + kvp.Key);
                            }
                        }

                        // Set the key in the second column (index 1) and value in the third column (index 2)
                        dataGridView1.Rows[rowIndex].Cells[1].Value = formattedKey.ToLower();
                        dataGridView1.Rows[rowIndex].Cells[2].Value = kvp.Value.ToLower();
                    }
                    dataGridView1.Invalidate();
                    dataGridView1.Update();
                    LoadCheckboxState();
                }
            }
        }

        private void buttonPrimary1_Click(object sender, EventArgs e)
        {
            // Copy text from the textbox to the clipboard
            Clipboard.SetText(roundedTextBox3.Text);
            buttonPrimary1.Text = "Copied";

            // Start a timer to change the button text back to "Copy" after a delay
            Timer timer = new Timer();
            timer.Interval = 3000; // 3000 milliseconds = 3 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer
            Timer timer = (Timer)sender;
            timer.Stop();

            // Change the button text back to "Copy"
            buttonPrimary1.Text = "Copy Code";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open the link in the default web browser
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/MrVibesRSA/Streamer.bot-Plugin");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error opening the link: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

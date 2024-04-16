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
using System.Net.Http;
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

            string username = "MrVibesRSA";
            string repository = "Streamer.bot-Actions";
            string filePath = "Update-Macro-Deck-veriables/Text/Import-Code.txt";
            string branch = "main"; // or whichever branch your file is in

            string apiUrl = $"https://raw.githubusercontent.com/{username}/{repository}/{branch}/{filePath}";

            string fileContent = GetFileContent(apiUrl);

            //Import Code for streamer.bot
            roundedTextBox3.Text = fileContent; 

            // Assuming WebSocketClient has a property named State indicating the connection state
            btn_Connect.Text = WebSocketClient.IsConnected ? "Disconnect" : "Connect";

            WebSocketClient.WebSocketConnected += OnConnected;
            WebSocketClient.WebSocketDisconnected += OnDisconnect;

            // Subscribe to the UpdateVariableList event
            Main.UpdateVariableList += HandleUpdateVariableList;

            PopulateDataGridView();
            LoadCheckboxState();
        }

        static string GetFileContent(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result; // Using .Result to synchronously wait for the response
                response.EnsureSuccessStatusCode();

                string fileContent = response.Content.ReadAsStringAsync().Result; // Using .Result to synchronously wait for the content

                return fileContent;
            }
        }

        private void HandleUpdateVariableList(object sender, EventArgs e)
        {
            PopulateDataGridView();
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

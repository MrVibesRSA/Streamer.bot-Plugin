using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Services;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrVibes_RSA.StreamerbotPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        /* 
         * RECOMMENDED FUTURE IMPROVEMENTS:
         * 
         * 1. Add Connection Status Indicator
         *    - Visual LED indicator showing connection state (connected/disconnected/connecting)
         *    - Tooltip with detailed connection status
         *    
         * 2. Auto-Reconnect Option --- Added busy sorting out bugs.
         *    - Checkbox "Auto-reconnect" in UI
         *    - Configuration for retry interval
         *    - Maximum retry attempts setting
         *    
         * 3. Connection Testing Feature
         *    - Separate "Test Connection" button
         *    - Validates connection without saving configuration
         *    - Returns detailed connection diagnostics
         *    
         * 4. Configuration Profiles
         *    - Dropdown to select between multiple saved profiles
         *    - Add/Delete profile buttons
         *    - Profile-specific credentials
         *    
         * 5. Advanced Settings Section
         *    - Expandable "Advanced Settings" panel
         *    - Connection timeout configuration
         *    - Message timeout settings
         *    - Debug logging toggle
         *    
         * 6. Connection Statistics
         *    - Uptime/downtime monitoring
         *    - Message throughput counters
         *    - Error rate tracking
         */

        private bool _isFormLoaded = false;
        private bool _isConfigured;

        public bool IsConfigured
        {
            get => _isConfigured;
            set => _isConfigured = value;
        }

        public PluginConfig()
        {
            InitializeComponent();

            this.Load += PluginConfig_Load;
        }

        private async void PluginConfig_Load(object sender, EventArgs e)
        {
            try
            {
                // Load configuration first (non-UI work)
                await Task.Run(() => LoadConfiguration());

                _isFormLoaded = true;
                SafeInvoke(() =>
                {
                    SetupToolTips();
                    SetupWebSocketEvents();
                    UpdateConnectionButton();
                    linkLabel1.Focus();
                });

            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Form load failed: {ex.Message}");
            }
        }

        private void LoadConfiguration()
        {
            string address = "127.0.0.1";
            string port = "8080";
            string endpoint = "/";
            string password = "";

            string configValue = PluginConfiguration.GetValue(PluginInstance.Main, "Configured") ?? "false";
            _isConfigured = bool.TryParse(configValue, out bool result) && result;

            if (!_isConfigured)
            {
                MacroDeckLogger.Info(PluginInstance.Main, "Streamer.bot plugin isn't configured.");
            }
            else
            {
                MacroDeckLogger.Info(PluginInstance.Main, "Streamer.bot plugin is configured.");
                address = PluginConfiguration.GetValue(PluginInstance.Main, "Address") ?? address;
                port = PluginConfiguration.GetValue(PluginInstance.Main, "Port") ?? port;
                endpoint = PluginConfiguration.GetValue(PluginInstance.Main, "Endpoint") ?? endpoint;

                var credentialsList = PluginCredentials.GetPluginCredentials(PluginInstance.Main);
                if (credentialsList?.Count > 0 && credentialsList[0].ContainsKey("password"))
                {
                    password = credentialsList[0]["password"];
                }

                checkBox_AutoConnect.Checked = bool.TryParse(PluginConfiguration.GetValue(PluginInstance.Main, "AutoConnect") as string, out var autoConnect) && autoConnect;
            }

            address_roundedTextBox.Text = address;
            port_roundedTextBox.Text = port;
            endpoint_roundedTextBox.Text = endpoint;
            password_roundedTextBox.Text = password;
            password_roundedTextBox.PasswordChar = true;
        }

        private void SetupToolTips()
        {
            var toolTip = new ToolTip()
            {
                AutoPopDelay = 5000,
                InitialDelay = 500,
                ReshowDelay = 250,
                ShowAlways = true
            };

            toolTip.SetToolTip(btn_Connect, "Connect/disconnect from Streamer.bot websocket server");
            toolTip.SetToolTip(address_roundedTextBox, "Server IP address (e.g., 127.0.0.1 or your remote IP)");
            toolTip.SetToolTip(port_roundedTextBox, "Websocket server port (default: 8080)");
            toolTip.SetToolTip(endpoint_roundedTextBox, "Websocket endpoint path (default: /)");
            toolTip.SetToolTip(password_roundedTextBox, "Optional server password if authentication is required");
            toolTip.SetToolTip(showPassword_button, "Toggle password visibility");
        }

        private void SetupWebSocketEvents()
        {
            WebSocketService.Instance.Connected += OnConnected;
            WebSocketService.Instance.Disconnected += OnDisconnect;
            WebSocketService.Instance.Error += OnErrorMessageReceived;
            WebSocketService.Instance.MessageReceived_Hello += OnHelloMessageReceived;
            WebSocketService.Instance.MessageReceived_Info += OnInfoMessageReceived;
            WebSocketService.Instance.MessageReceived_Actions += OnActionMessageReceived;
            WebSocketService.Instance.MessageReceived_Globals += OnGlobalsMessageReceived;
            WebSocketService.Instance.MessageReceived_AuthenticateRequest += OnAuthenticateRequestReceived;
        }

        private void OnErrorMessageReceived(object sender, string e)
        {
            if (e.Contains("Unable to connect to the remote server"))
            {
                ShowErrorMessage("Please make sure Streamer.bot and the web socket server is Running");
            }
        }

        private void OnHelloMessageReceived(object sender, string e)
        {
            try
            {
                var jObject = JObject.Parse(e);
                if (jObject["request"]?.ToString() == "Hello")
                {
                    var info = jObject["info"];
                    if (info != null)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            label_ConnectedTo.Text = info["name"]?.ToString();
                            label_Version.Text = info["version"]?.ToString();
                            label_OS.Text = info["os"]?.ToString().ToUpper();
                            label_OSVersion.Text = info["osVersion"]?.ToString();
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket message: {ex.Message}");
            }
        }

        private void OnAuthenticateRequestReceived(object sender, string e)
        {
            try
            {
                var jObject = JObject.Parse(e);
                if (jObject["request"]?.ToString() == "Hello")
                {
                    var info = jObject["info"];
                    if (info != null)
                    {
                        this.Invoke((MethodInvoker)delegate {
                            label_ConnectedTo.Text = info["name"]?.ToString();
                            label_Version.Text = info["version"]?.ToString();
                            label_OS.Text = info["os"]?.ToString().ToUpper();
                            label_OSVersion.Text = info["osVersion"]?.ToString();
                        });
                    }

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

        private void OnInfoMessageReceived(object sender, string e)
        {
            try
            {
                var jObject = JObject.Parse(e);
                var info = jObject["info"];
                if (info != null)
                {
                    SafeInvoke(() => {
                        label_ConnectedTo.Text = info["name"]?.ToString();
                        label_Version.Text = info["version"]?.ToString();
                        label_OS.Text = info["os"]?.ToString().ToUpper();
                        label_OSVersion.Text = info["osVersion"]?.ToString();
                    });
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket message: {ex.Message}");
            }
        }

        // Update all message handlers to use SafeInvoke
        private void OnActionMessageReceived(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);
                if (json["id"]?.ToString() == "GetActionsForMacroDeck" && json["count"] != null)
                {
                    int count = json["count"].Value<int>();
                    MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocket] Actions count: {count}");

                    SafeInvoke(() => {
                        label_ActionCount.Text = count.ToString();
                    });
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse Actions message: {ex.Message}");
            }
        }

        private void OnGlobalsMessageReceived(object sender, string e)
        {
            try
            {
                var json = JObject.Parse(e);
                string id = json["id"]?.ToString();
                if ((id == "GetGlobalsForMacroDeck") && json["count"] != null)
                {
                    int count = json["count"].Value<int>();
                    MacroDeckLogger.Info(PluginInstance.Main, $"[WebSocket] {id} count: {count}");

                    SafeInvoke(() => {
                        label_GlobalCount.Text = count.ToString();
                    });
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse message: {ex.Message}");
            }
        }

        // Update the connection button method
        private void UpdateConnectionButton()
        {
            SafeInvoke(() => {
                btn_Connect.Text = WebSocketService.Instance.IsConnected ? "Disconnect" : "Connect";

                if (WebSocketService.Instance.IsConnected)
                {
                    WebSocketService.Instance.GetActionsList();
                    WebSocketService.Instance.GetGlobalsList();
                    WebSocketService.Instance.GetConnectionInfo();
                }
            });
        }

        private bool IsInputValid()
        {
            if (string.IsNullOrWhiteSpace(address_roundedTextBox.Text))
            {
                ShowErrorMessage("Please enter IP/address.");
                return false;
            }

            if (!int.TryParse(port_roundedTextBox.Text, out int port) || port < 1 || port > 65535)
            {
                ShowErrorMessage("Invalid port number (1-65535).");
                return false;
            }

            if (string.IsNullOrWhiteSpace(endpoint_roundedTextBox.Text))
            {
                ShowErrorMessage("Please enter endpoint.");
                return false;
            }

            return true;
        }

        private void ShowErrorMessage(string message)
        {
            SafeInvoke(() => {
                using var error = new ErrorMessage(message);
                error.ShowDialog();
            });
        }

        private int GetPort()
        {
            return int.TryParse(port_roundedTextBox.Text, out int port) ? port : 8080;
        }

        private void SaveConfiguration(string address, int port, string endpoint, string password)
        {
            try
            {
                MacroDeckLogger.Info(PluginInstance.Main,
                    $"Saving configuration - Address: {address}, Port: {port}, Endpoint: {endpoint}");

                PluginConfiguration.SetValue(PluginInstance.Main, "Address", address);
                PluginConfiguration.SetValue(PluginInstance.Main, "Port", port.ToString());
                PluginConfiguration.SetValue(PluginInstance.Main, "Endpoint", endpoint);

                var credentials = new Dictionary<string, string>
                {
                    { "password", password }
                };

                PluginCredentials.SetCredentials(PluginInstance.Main, credentials);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main,
                    $"Failed to save configuration: {ex.Message}");
                throw;
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            SafeInvoke(() => {
                PluginConfiguration.SetValue(PluginInstance.Main, "Configured", "true");
                btn_Connect.Text = "Disconnect";
            });
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            SafeInvoke(() => {
                ClearConnectionLabels();
                btn_Connect.Text = "Connect";
            });
        }

        // Add this helper method for safe UI updates
        private void SafeInvoke(Action action)
        {
            try
            {
                if (this.IsDisposed || !_isFormLoaded)
                    return;

                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate {
                        if (!this.IsDisposed) action();
                    });
                }
                else
                {
                    if (!this.IsDisposed) action();
                }
            }
            catch (InvalidOperationException ex)
            {
                MacroDeckLogger.Warning(PluginInstance.Main, $"SafeInvoke skipped: {ex.Message}");
            }
        }

        private void ClearConnectionLabels()
        {
            label_ConnectedTo.Text = "";
            label_Version.Text = "";
            label_OS.Text = "";
            label_OSVersion.Text = "";
            label_ActionCount.Text = "";
            label_GlobalCount.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/MrVibesRSA/Streamer.bot-Plugin");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error opening the link: " + ex);
            }
        }

        private async void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                btn_Connect.Enabled = false;

                if (!IsInputValid())
                {
                    btn_Connect.Enabled = true;
                    return;
                }

                SaveConfiguration(
                    address_roundedTextBox.Text,
                    GetPort(),
                    endpoint_roundedTextBox.Text,
                    password_roundedTextBox.Text
                );

                if (WebSocketService.Instance.IsConnected)
                {
                    await DisconnectFromServer();
                }
                else
                {
                    await ConnectToServer();
                }
            }
            finally
            {
                btn_Connect.Enabled = true;
            }
        }

        private async Task ConnectToServer()
        {
            try
            {
                var uri = new UriBuilder("ws", address_roundedTextBox.Text, GetPort(), endpoint_roundedTextBox.Text).Uri;

                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));
                var connectTask = WebSocketService.Instance.StartAsync(uri.ToString());

                var completedTask = await Task.WhenAny(connectTask, timeoutTask);
                if (completedTask == timeoutTask)
                {
                    ShowErrorMessage("Connection timed out after 10 seconds");
                    return;
                }

                await connectTask;
            }
            catch (UriFormatException ex)
            {
                ShowErrorMessage($"Invalid connection URL: {ex.Message}");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Connection failed: {ex.Message}");
                ShowErrorMessage($"Connection failed: {ex.Message}");
            }
        }

        private async Task DisconnectFromServer()
        {
            try
            {
                WebSocketService.Instance.Close();
                MacroDeckLogger.Info(PluginInstance.Main, "Disconnected from WebSocket server");
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main,
                    $"Error during disconnection: {ex.Message}");
                ShowErrorMessage("Error while disconnecting");
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPassword_button_Click_1(object sender, EventArgs e)
        {
            password_roundedTextBox.PasswordChar = !password_roundedTextBox.PasswordChar;
            showPassword_button.Text = password_roundedTextBox.PasswordChar ? "Show" : "Hide";

            if (!password_roundedTextBox.PasswordChar)
            {
                Task.Delay(30000).ContinueWith(t => {
                    this.Invoke((MethodInvoker)delegate {
                        password_roundedTextBox.PasswordChar = true;
                        showPassword_button.Text = "Show";
                    });
                });
            }
        }

        private void checkBox_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {
            PluginConfiguration.SetValue(PluginInstance.Main, "AutoConnect", checkBox_AutoConnect.Checked.ToString());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            WebSocketService.Instance.Connected -= OnConnected;
            WebSocketService.Instance.Disconnected -= OnDisconnect;
            WebSocketService.Instance.Error -= OnErrorMessageReceived;
            WebSocketService.Instance.MessageReceived_Hello -= OnHelloMessageReceived;
            WebSocketService.Instance.MessageReceived_Info -= OnInfoMessageReceived;
            WebSocketService.Instance.MessageReceived_Actions -= OnActionMessageReceived;
            WebSocketService.Instance.MessageReceived_Globals -= OnGlobalsMessageReceived;
            WebSocketService.Instance.MessageReceived_AuthenticateRequest -= OnAuthenticateRequestReceived;

            base.OnFormClosing(e);
        }
    }
}
using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.Services;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck.DataTypes.Core;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

namespace MrVibesRSA.StreamerbotPlugin.GUI
{
    public partial class PluginConfig : DialogForm
    {
        // Fields
        private bool _isFormLoaded = false;
        private bool _isConfigured = false;
        private bool _isEditingProfile = false;
        private bool _isAddingProfile = false;

        private WebSocketProfileManager _WebsocketProfileManager;
        private SubscribedEventHandler _subscribedEventHandler;
        private ProfileManager _profile = new ProfileManager();

        public event EventHandler<string> ProfileConnected;
        public event EventHandler<string> ProfileDisconnected;

        private readonly HashSet<TreeNode> _partiallyCheckedNodes = new HashSet<TreeNode>();

        private Dictionary<string, List<EventWithSubscription>> _currentEvents;
        private string _currentEventProfileId;


        public PluginConfig()
        {
            InitializeComponent();

            _WebsocketProfileManager = WebSocketProfileManager.Instance;
            _subscribedEventHandler = SubscribedEventHandler.Instance;
            ProfileManager _profile = new ProfileManager();

            this.Load += PluginConfig_Load;

            this.ProfileConnected += (s, id) =>
            {
                if (id == null)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, "ProfileConnected event fired with null ID.");
                }

                var connectionInfo = DataManager.GetConnectionInfo(id);
                int actionsCount = (DataManager.GetActionsList(id)?.Count) ?? 0;
                int globalsCount = (DataManager.GetGlobalsList(id)?.Count) ?? 0;


                if (connectionInfo != null)
                {
                    UpdateConnectionInfo(
                        connectionInfo.Name,
                        connectionInfo.Version,
                        connectionInfo.OS,
                        connectionInfo.OSVersion,
                        actionsCount.ToString(),
                        globalsCount.ToString()
                    );
                }
                else
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to update connection info for profile ID: {id} due to null data.");
                }

                UpdateConnectionStatus();
            };

            this.ProfileDisconnected += (s, id) =>
            {
                ClearConnectionInfo();
                UpdateConnectionStatus();
            };

            SetupToolTips();
        }

        #region "Handle Loading"
        private void PluginConfig_Load(object sender, EventArgs e)
        {

            string configuredValue = PluginConfiguration.GetValue(PluginInstance.Main, "Configured");

            _isConfigured = bool.TryParse(configuredValue, out var result) && result;

            try
            {
                if (!_isConfigured)
                {
                    MacroDeckLogger.Info(PluginInstance.Main, "Streamer.bot plugin isn't configured, loading default profile.");
                    LoadDefaultProfile();
                }
                else
                {
                    MacroDeckLogger.Info(PluginInstance.Main, "Streamer.bot plugin is configured, loading saved profiles");
                    LoadSavedProfiles();
                }

                linkLabel_ToGithub.Focus();
                _isFormLoaded = true;
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Form load failed: {ex.Message}");
            }
        }

        private void LoadDefaultProfile()
        {
            string id = Guid.NewGuid().ToString();

            var defaultProfile = new ProfileConfig
            {
                Id = id,
                Name = "Default",
                Address = "127.0.0.1",
                Port = "8080",
                Endpoint = "/",
                Password = "",
                AutoConnect = false
            };

            _profile.Save(defaultProfile.Id, defaultProfile.Name, defaultProfile.Address,
                          defaultProfile.Port, defaultProfile.Endpoint, defaultProfile.Password, defaultProfile.AutoConnect);

            List<ProfileConfig> profileList = _profile.GetAllProfiles();
            BindProfilesToComboBox(profileList, id);

            PluginConfiguration.SetValue(PluginInstance.Main, "Configured", "true");
        }

        private void LoadSavedProfiles()
        {
            var profileList = _profile.GetAllProfiles();

            if (_profile == null)
            {
                MacroDeckLogger.Error(PluginInstance.Main, "_profile is null during form load");
                return;
            }

            if (profileList == null || profileList.Count == 0)
            {
                MacroDeckLogger.Info(PluginInstance.Main, "No saved profiles found, loading default profile.");
                LoadDefaultProfile();
                return;
            }

            BindProfilesToComboBox(profileList);
        }

        #endregion

        private void BindProfilesToComboBox(List<ProfileConfig> profileList, string selectedId = null)
        {
            var comboBoxItems = profileList
                .Select(profile => new ComboBoxItemHelper(profile.Name, profile.Id))
                .ToList();

            comboBox_SelectedProfile.DataSource = comboBoxItems;
            comboBox_SelectedProfile.DisplayMember = "Name";
            comboBox_SelectedProfile.ValueMember = "Id";

            if (!string.IsNullOrEmpty(selectedId))
            {
                comboBox_SelectedProfile.SelectedValue = selectedId;
            }
            else if (comboBox_SelectedProfile.Items.Count > 0)
            {
                comboBox_SelectedProfile.SelectedIndex = 0;
            }
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

        private bool IsInputValid()
        {
            if (string.IsNullOrWhiteSpace(address_roundedTextBox.Text))
            {
                ShowDialogMessage("Please enter IP/address.", CustomDialogType.Error);
                return false;
            }

            if (!int.TryParse(port_roundedTextBox.Text, out int port) || port < 1 || port > 65535)
            {
                ShowDialogMessage("Invalid port number (1-65535).", CustomDialogType.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(endpoint_roundedTextBox.Text))
            {
                ShowDialogMessage("Please enter endpoint.", CustomDialogType.Error);
                return false;
            }

            return true;
        }

        private void FillProfileFields(ProfileConfig profile)
        {
            // if (this.InvokeRequired)
            // {
            //     this.Invoke((MethodInvoker)(() => FillProfileFields(profile)));
            //     return;
            // }

            address_roundedTextBox.Text = profile.Address;
            port_roundedTextBox.Text = profile.Port;
            endpoint_roundedTextBox.Text = profile.Endpoint;
            password_roundedTextBox.Text = profile.Password;
            password_roundedTextBox.PasswordChar = true;
            checkBox_AutoConnect.Checked = profile.AutoConnect;
        }

        private void UpdateConnectionButton(string profileIdOverride = null)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateConnectionButton(profileIdOverride)));
                return;
            }

            LockUnlockControls(false);

            string profileId = profileIdOverride;

            if (string.IsNullOrEmpty(profileId) && comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedItem)
            {
                profileId = selectedItem.Id?.ToString();
            }

            if (string.IsNullOrEmpty(profileId))
            {
                MacroDeckLogger.Warning(PluginInstance.Main, "UpdateConnectionButton: No profile ID found.");
                btn_Connect.Text = "Connect";
                btn_Connect.Enabled = true;
                return;
            }

            var service = _WebsocketProfileManager.GetServiceByProfileId(profileId);

            if (service != null)
            {
                bool isConnected = service.IsConnected;
                btn_Connect.Text = isConnected ? "Disconnect" : "Connect";

                if (isConnected)
                {
                    ProfileConnected?.Invoke(this, profileId);
                }
                else
                {
                    ProfileDisconnected?.Invoke(this, profileId);
                }

                LockUnlockControls(isConnected);
                btn_Connect.Enabled = true;

                MacroDeckLogger.Info(PluginInstance.Main, $"Updating connection button state. ID {profileId}, isConnected {isConnected}");
            }
            else
            {
                btn_Connect.Text = "Connect";
                btn_Connect.Enabled = true;
                MacroDeckLogger.Warning(PluginInstance.Main, $"UpdateConnectionButton: No service found for ID {profileId}");
            }
        }

        private void UpdateConnectionInfo(string name = null, string version = null, string os = null, string osVersion = null, string actionCount = null, string globleCount = null)
        {
            if (!string.IsNullOrEmpty(name))
                label_ConnectedTo.Text = name;

            if (!string.IsNullOrEmpty(version))
                label_Version.Text = version;

            if (!string.IsNullOrEmpty(os))
                label_OS.Text = os.ToUpper();

            if (!string.IsNullOrEmpty(osVersion))
                label_OSVersion.Text = osVersion;

            if (!string.IsNullOrEmpty(actionCount))
                label_ActionCount.Text = actionCount;

            if (!string.IsNullOrEmpty(globleCount))
                label_GlobalCount.Text = globleCount;
        }
      
        private void ClearConnectionInfo()
        {
            label_ConnectedTo.Text = "";
            label_Version.Text = "";
            label_OS.Text = "";
            label_OSVersion.Text = "";
            label_ActionCount.Text = "";
            label_GlobalCount.Text = "";
        }

        private void OnConnected(object sender, EventArgs e)
        {

            MacroDeckLogger.Info(PluginInstance.Main, "OnConnected fired.");
            var service = sender as WebSocketService;
            if (service == null) return;

            UpdateConnectionButton(service.ProfileId);
        }

        private void OnDisconnected(object sender, EventArgs e)
        {
            var service = sender as WebSocketService;
            MacroDeckLogger.Info(PluginInstance.Main, "OnDisconnected fired. " + service.ProfileId);
            if (service == null) return;
            var profileId = service.ProfileId;
            btn_Connect.Enabled = true;
            UpdateConnectionButton(profileId);
        }

        private void UpdateConnectionStatus()
        {
            int connectionCount = _WebsocketProfileManager.GetConnectionCount();
            int profileCount = _profile.GetAllProfiles().Count;

            label_Status.Text = $"Connected: {connectionCount} / {profileCount}";
        }

        private void LockUnlockControls(bool lockControls)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => LockUnlockControls(lockControls)));
                return;
            }

            address_roundedTextBox.Enabled = !lockControls;
            port_roundedTextBox.Enabled = !lockControls;
            endpoint_roundedTextBox.Enabled = !lockControls;
            password_roundedTextBox.Enabled = !lockControls;
            checkBox_AutoConnect.Enabled = !lockControls;
            buttonPrimary_AddNewProfile.Enabled = !lockControls;
            buttonPrimary_EditProfile.Enabled = !lockControls;
            button_RemoveProfile.Enabled = !lockControls;
            showPassword_button.Enabled = !lockControls;
        }

        private void LoadEventsIntoTreeView(Dictionary<string, List<EventWithSubscription>> events)
        {
            try 
            {
                treeView_Events.BeginUpdate();
                treeView_Events.Nodes.Clear();

                // Store current profile's events for saving later
                _currentEvents = events ?? new Dictionary<string, List<EventWithSubscription>>();

                // Sort parent keys alphabetically
                foreach (var source in _currentEvents.OrderBy(e => e.Key))
                {
                    var parentNode = new TreeNode(source.Key);

                    foreach (var ev in source.Value)
                    {
                        var childNode = new TreeNode(ev.Name)
                        {
                            Checked = ev.Subscribed
                        };
                        parentNode.Nodes.Add(childNode);
                    }

                    treeView_Events.Nodes.Add(parentNode);
                }

                // After all nodes are added, update parent labels for partial selection
                foreach (TreeNode parent in treeView_Events.Nodes)
                {
                    UpdateParentLabel(parent);
                }

                treeView_Events.EndUpdate();
            }
            catch 
            {
                MacroDeckLogger.Info(PluginInstance.Main, "No events loaded or error loading events. Streamer.bot not logged in or running.");

            }

        }


        private DialogResult ShowDialogMessage(string message, CustomDialogType type = CustomDialogType.Info, string okButtonText = "OK", string cancelButtonText = "Cancel")
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new Func<DialogResult>(() => ShowDialogMessage(message, type, okButtonText, cancelButtonText)));
            }

            using var dialog = new CustomDialog(message, type, okButtonText, cancelButtonText);
            dialog.ShowDialog();
            return dialog.Result;
        }

        #region "HandleControls"

        private void buttonPrimary_AddNewProfile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new AddEditProfile("Add Profile", ""))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        dynamic result = dialog.Tag;
                        string profileName = result.Profile;

                        string id = Guid.NewGuid().ToString();
                        var newProfile = new ProfileConfig
                        {
                            Id = id,
                            Name = profileName,
                            Address = "127.0.0.1",
                            Port = "8080",
                            Endpoint = "/",
                            Password = "",
                            AutoConnect = false
                        };

                        _profile.Save(
                            newProfile.Id,
                            newProfile.Name,
                            newProfile.Address,
                            newProfile.Port,
                            newProfile.Endpoint,
                            newProfile.Password,
                            newProfile.AutoConnect
                        );

                        var profileList = _profile.GetAllProfiles()
                            .Select(p => new ComboBoxItemHelper(p.Name, p.Id))
                            .ToList();
                        comboBox_SelectedProfile.DataSource = null;
                        comboBox_SelectedProfile.DataSource = profileList;
                        comboBox_SelectedProfile.DisplayMember = "Name";
                        comboBox_SelectedProfile.ValueMember = "Id";
                        comboBox_SelectedProfile.SelectedValue = newProfile.Id;

                        FillProfileFields(newProfile);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowDialogMessage($"Error saving new profile: {ex.Message}", CustomDialogType.Error);
            }
        }

        private void buttonPrimary_EditProfile_Click(object sender, EventArgs e)
        {
            if (comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedItem)
            {
                string address = address_roundedTextBox.Text.Trim();
                string port = port_roundedTextBox.Text.Trim();
                string endpoint = endpoint_roundedTextBox.Text.Trim();
                string password = password_roundedTextBox.Text.Trim();
                bool autoConnect = checkBox_AutoConnect.Checked;

                try
                {
                    using (var dialog = new AddEditProfile("Edit Profile", selectedItem.Name))
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            dynamic result = dialog.Tag;
                            string newName = result.Profile;

                            var selectedProfile = _profile.LoadProfile(selectedItem.Id.ToString());

                            selectedProfile.Name = newName;
                            selectedProfile.Address = address;
                            selectedProfile.Port = port;
                            selectedProfile.Endpoint = endpoint;
                            selectedProfile.Password = password;
                            selectedProfile.AutoConnect = autoConnect;

                            _profile.Save(
                                selectedProfile.Id,
                                selectedProfile.Name,
                                selectedProfile.Address,
                                selectedProfile.Port,
                                selectedProfile.Endpoint,
                                selectedProfile.Password,
                                selectedProfile.AutoConnect
                            );

                            var profileList = _profile.GetAllProfiles()
                                .Select(p => new ComboBoxItemHelper(p.Name, p.Id))
                                .ToList();
                            comboBox_SelectedProfile.DataSource = null;
                            comboBox_SelectedProfile.DataSource = profileList;
                            comboBox_SelectedProfile.DisplayMember = "Name";
                            comboBox_SelectedProfile.ValueMember = "Id";
                            comboBox_SelectedProfile.SelectedValue = selectedProfile.Id;

                            FillProfileFields(selectedProfile);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ShowDialogMessage($"Error editing profile: {ex.Message}", CustomDialogType.Error);
                }
            }
        }

        private void button_RemoveProfile_Click(object sender, EventArgs e)
        {
            if (comboBox_SelectedProfile.SelectedItem is not ComboBoxItemHelper selectedItem)
            {
                ShowDialogMessage("Please select a profile to remove.", CustomDialogType.Error);
                return;
            }

            var confirmResult = ShowDialogMessage(
                $"Are you sure you want to delete the profile \"{selectedItem.Name}\"?",
                CustomDialogType.Confirm,
                "Yes", "No");

            if (confirmResult != DialogResult.OK)
                return;

            _profile.DeleteProfile(selectedItem.Id.ToString());

            var profileList = _profile.GetAllProfiles()
                .Select(p => new ComboBoxItemHelper(p.Name, p.Id))
                .ToList();

            comboBox_SelectedProfile.DataSource = null;
            comboBox_SelectedProfile.DataSource = profileList;
            comboBox_SelectedProfile.SelectedIndex = 0;

            if (profileList.Count > 0)
            {
                comboBox_SelectedProfile.SelectedIndex = 0;
            }
            else
            {
                LoadDefaultProfile();
            }

            ShowDialogMessage($"Profile \"{selectedItem.Name}\" removed.", CustomDialogType.Info);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", "https://github.com/MrVibesRSA/Streamer.bot-Plugin");
            }
            catch (Exception ex)
            {
                ShowDialogMessage("Error opening the link: " + ex, CustomDialogType.Error);
            }
        }

        private void comboBox_SelectedProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_SelectedProfile.SelectedItem is ComboBoxItemHelper selectedItem &&
                selectedItem.Id is string profileId)
            {
                var selectedProfile = _profile.LoadProfile(profileId);
                if (selectedProfile == null)
                {
                    ShowDialogMessage("Failed to load selected profile.", CustomDialogType.Error);
                    return;
                }

                
                FillProfileFields(selectedProfile);
                UpdateConnectionButton(profileId);

                MacroDeckLogger.Info(PluginInstance.Main, $"Selected profile changed to: {selectedItem.Name} ({selectedItem.Id})");
            }
        }

        private void showPassword_button_Click(object sender, EventArgs e)
        {
            password_roundedTextBox.PasswordChar = !password_roundedTextBox.PasswordChar;
            showPassword_button.Text = password_roundedTextBox.PasswordChar ? "Show" : "Hide";

            if (!password_roundedTextBox.PasswordChar)
            {
                Task.Delay(30000).ContinueWith(t =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        password_roundedTextBox.PasswordChar = true;
                        showPassword_button.Text = "Show";
                    });
                });
            }
        }

        private void checkBox_AutoConnect_CheckedChanged(object sender, EventArgs e)
        {

            if (comboBox_SelectedProfile.SelectedItem is not ComboBoxItemHelper selectedItem)
            {
                btn_Connect.Enabled = true;
                return;
            }

            var selectedProfile = _profile.LoadProfile(selectedItem.Id.ToString());
            if (selectedProfile == null)
            {
                ShowDialogMessage("Failed to load the selected profile.", CustomDialogType.Error);
                return;
            }

            if (!IsInputValid())
            {
                btn_Connect.Enabled = true;
                return;
            }

            _profile.Save(
                selectedProfile.Id,
                selectedProfile.Name,
                address_roundedTextBox.Text.Trim(),
                port_roundedTextBox.Text.Trim(),
                endpoint_roundedTextBox.Text.Trim(),
                password_roundedTextBox.Text.Trim(),
                checkBox_AutoConnect.Checked
            );
        }

        private async void btn_Connect_Click(object sender, EventArgs e)
        {
            btn_Connect.Enabled = false;

            try
            {
                if (comboBox_SelectedProfile.SelectedItem is not ComboBoxItemHelper selectedItem)
                    return;

                var profileId = selectedItem.Id.ToString();
                var selectedProfile = _profile.LoadProfile(profileId);

                if (selectedProfile == null)
                {
                    ShowDialogMessage("Failed to load the selected profile.", CustomDialogType.Error);
                    return;
                }

                if (!IsInputValid())
                    return;

                // Save profile data
                _profile.Save(
                    selectedProfile.Id,
                    selectedProfile.Name,
                    address_roundedTextBox.Text.Trim(),
                    port_roundedTextBox.Text.Trim(),
                    endpoint_roundedTextBox.Text.Trim(),
                    password_roundedTextBox.Text.Trim(),
                    checkBox_AutoConnect.Checked
                );

                var existingService = _WebsocketProfileManager.GetServiceByProfileId(profileId);

                if (existingService != null && existingService.IsConnected)
                {
                    existingService.Close();
                    await Task.Delay(100);

                    existingService.Connected -= OnConnected;
                    existingService.Disconnected -= OnDisconnected;

                    UpdateConnectionButton(profileId);
                    return;
                }

                // Attempt to connect
                var newService = await _WebsocketProfileManager.ConnectServiceAsync(profileId);

                await Task.Delay(150);

                if (newService == null || !newService.IsConnected)
                {
                    ShowDialogMessage("Connection failed. Please check the address, port, password and Streamer.bot Server.", CustomDialogType.Error);
                    return;
                }

                // Hook up events safely (only once)
                newService.Connected -= OnConnected;
                newService.Disconnected -= OnDisconnected;

                newService.Connected += OnConnected;
                newService.Disconnected += OnDisconnected;

                UpdateConnectionButton(profileId);
            }
            catch (Exception ex)
            {
                ShowDialogMessage($"Unexpected error during connection:\n{ex.Message}", CustomDialogType.Error);
            }
            finally
            {
                btn_Connect.Enabled = true;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_WSSettings_Click(object sender, EventArgs e)
        {
            button_WSSettings.BackColor = Color.FromArgb(65, 65, 65);
            button_EventList.BackColor = Color.FromArgb(45, 45, 45);
            roundedPanel_Websocket.Visible = true;
            roundedPanel_Eventlist.Visible = false;
            LoadSavedProfiles();
        }

        private void button_EventList_Click(object sender, EventArgs e)
        {
            button_EventList.BackColor = Color.FromArgb(65, 65, 65);
            button_WSSettings.BackColor = Color.FromArgb(45, 45, 45);
            roundedPanel_Websocket.Visible = false;
            roundedPanel_Eventlist.Visible = true;

            var profileList = _profile.GetAllProfiles();
            if (profileList != null && profileList.Any())
            {
                var comboBoxItems = profileList
                    .Where(p => _WebsocketProfileManager.HasConnection(p.Id))
                    .Select(p => new ComboBoxItemHelper(p.Name, p.Id, true))
                    .ToList();

                comboBox_ProfileListForEvents.DataSource = comboBoxItems;
                comboBox_ProfileListForEvents.DisplayMember = "Name";
                comboBox_ProfileListForEvents.ValueMember = "Id";

                if (comboBoxItems.Count > 0)
                {
                    comboBox_ProfileListForEvents.SelectedIndex = 0;
                }
                else
                {
                    comboBox_ProfileListForEvents.SelectedIndex = -1; // no valid items
                }

                comboBox_ProfileListForEvents.Refresh();
            }
            else
            {
                // Clear if no profiles at all
                comboBox_ProfileListForEvents.DataSource = null;
                comboBox_ProfileListForEvents.Items.Clear();
                comboBox_ProfileListForEvents.SelectedIndex = -1;
            }
        }


        private void comboBox_ProfileListForEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            treeView_Events.Nodes.Clear(); // Always clear first

            if (comboBox_ProfileListForEvents.SelectedItem is ComboBoxItemHelper selectedItem &&
                !string.IsNullOrEmpty(selectedItem.Id?.ToString()))
            {
                string profileId = selectedItem.Id.ToString();
                _currentEventProfileId = profileId;
                if (_WebsocketProfileManager.HasConnection(profileId))
                {
                    var events = DataManager.GetEvents(profileId);

                    if (events == null || events.Count == 0)
                    {
                        LoadEventsIntoTreeView(null);
                        return;
                    }

                    LoadEventsIntoTreeView(events);
                }
                else
                {
                    LoadEventsIntoTreeView(null);
                }
            }
        }

        private async void treeView_Events_AfterCheck(object sender, TreeViewEventArgs e)
        {
            treeView_Events.AfterCheck -= treeView_Events_AfterCheck;
            var service = _WebsocketProfileManager.GetServiceByProfileId(_currentEventProfileId);

            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode child in e.Node.Nodes)
                {
                    child.Checked = e.Node.Checked;
                }
                UpdateParentLabel(e.Node);
            }
            else if (e.Node.Parent != null)
            {
                var parent = e.Node.Parent;
                bool allChecked = parent.Nodes.Cast<TreeNode>().All(node => node.Checked);
                parent.Checked = allChecked;
                UpdateParentLabel(parent);
            }

            var events = new Dictionary<string, List<EventWithSubscription>>();

            foreach (TreeNode categoryNode in treeView_Events.Nodes)
            {
                var categoryName = categoryNode.Text;
                var eventList = new List<EventWithSubscription>();

                foreach (TreeNode eventNode in categoryNode.Nodes)
                {
                    eventList.Add(new EventWithSubscription
                    {
                        Name = eventNode.Text,
                        Subscribed = eventNode.Checked
                    });
                }

                events[categoryName] = eventList;
            }

            if (e.Node.Checked)
            {
                //Sub
                await _subscribedEventHandler.TriggerEventSubscribedAsync(service, e.Node.Parent.Text, e.Node.Text);
            }
            else
            { 
                //unsub
                await _subscribedEventHandler.TriggerEventUnsubscribedAsync(service, e.Node.Parent.Text, e.Node.Text);
            }

            
            DataManager.SaveEvents(_currentEventProfileId, events);
            treeView_Events.AfterCheck += treeView_Events_AfterCheck;
        }

        private void UpdateParentLabel(TreeNode parent)
        {
            int checkedCount = parent.Nodes.Cast<TreeNode>().Count(n => n.Checked);

            string baseText = parent.Text.TrimEnd(' ', '*');

            if (checkedCount == 0 || checkedCount == parent.Nodes.Count)
            {
                parent.Text = baseText; // All or none → no asterisk
            }
            else
            {
                parent.Text = baseText + " *"; // Partial → add asterisk
            }

            // Recurse up if needed
            if (parent.Parent != null)
            {
                UpdateParentLabel(parent.Parent);
            }
        }

        #endregion
    }
}
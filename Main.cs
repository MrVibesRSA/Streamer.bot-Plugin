
using MrVibesRSA.StreamerbotPlugin.Actions;
using MrVibesRSA.StreamerbotPlugin.GUI;
using MrVibesRSA.StreamerbotPlugin.Services;
using Newtonsoft.Json.Linq;
using SuchByte.MacroDeck;
using SuchByte.MacroDeck.GUI;
using SuchByte.MacroDeck.GUI.CustomControls;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrVibesRSA.StreamerbotPlugin
{
    public static class PluginInstance
    {
        public static Main Main { get; set; }
    }

    public class Main : MacroDeckPlugin
    {
        private readonly HashSet<WebSocketService> _subscribedInstances = new();
        private WebSocketProfileManager _websocketProfileManager;
        private ProfileManager _profile;
        private SubscribedEventHandler _subscribedEventHandler = new SubscribedEventHandler();

        private ContentSelectorButton _statusButton = new();
        private MainWindow _mainWindow;
        private int _connectionCount;

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

            _profile = new ProfileManager(); // Make sure this is initialized
            _websocketProfileManager = WebSocketProfileManager.Instance;

            _websocketProfileManager.ProfileAdded += OnProfileAddedEvent;
            _websocketProfileManager.ProfileRemoved += OnProfileRemovedEvent;

            MacroDeck.OnMainWindowLoad += MacroDeck_OnMainWindowLoad;

            AutoConnect();
        }

        private void OnProfileAddedEvent(object sender, WebSocketService service)
        {
            SubscribeToWebSocketEvents(service);
        }

        private void OnProfileRemovedEvent(object sender, WebSocketService service)
        {
            UnsubscribeFromWebSocketEvents(service);
        }

        private void MacroDeck_OnMainWindowLoad(object sender, EventArgs e)
        {
            _mainWindow = sender as MainWindow;

            _statusButton = new ContentSelectorButton
            {
                BackgroundImageLayout = ImageLayout.Stretch,
                BackgroundImage = MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_transparent
            };

            _statusButton.Click += StatusButton_Click;
            _mainWindow?.contentButtonPanel.Controls.Add(_statusButton);

            UpdateStatusIcon();
            // AutoConnect();
        }

        private async void AutoConnect()
        {
            var profiles = _profile.GetAllProfiles();
            if (profiles == null || profiles.Count == 0)
            {
                MacroDeckLogger.Info(PluginInstance.Main, "No Streamer.bot profiles found for auto-connect.");
                return;
            }

            foreach (var profile in profiles)
            {
                await Task.Delay(500);
                var profileData = _profile.LoadProfile(profile.Id);
                if (profileData?.AutoConnect != true) continue;

                try
                {
                    await _websocketProfileManager.ConnectServiceAsync(profileData.Id);
                    await Task.Delay(100); // slight delay before checking

                    var service = _websocketProfileManager.GetServiceByProfileId(profileData.Id);
                    if (service != null)
                    {
                        SubscribeToWebSocketEvents(service);
                    }

                    if (_websocketProfileManager.HasConnection(profileData.Id))
                    {
                        MacroDeckLogger.Info(PluginInstance.Main, $"Profile {profileData.Name} auto connected.");
                    }
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Auto-connect failed for profile {profileData?.Name}: {ex.Message}");
                }
            }
        }

        private void SubscribeToWebSocketEvents(WebSocketService service)
        {
            if (service == null || _subscribedInstances.Contains(service)) return;
            service.Connected += OnConnected;
            service.Disconnected += OnDisconnect;
            service.Error += OnErrorMessageReceived;
            // service.MessageReceived_Globals += OnGlobalsMessageReceived;
            // service.MessageReceived_GlobalUpdated += OnGlobalsUpdatedReceived;
            // service.MessageReceived_AuthenticateRequest += OnAuthenticateRequestReceived;
            UpdateStatusIcon();
            _subscribedInstances.Add(service);
        }

        private void UnsubscribeFromWebSocketEvents(WebSocketService service)
        {
            if (service == null || !_subscribedInstances.Contains(service)) return;
            service.Connected -= OnConnected;
            service.Disconnected -= OnDisconnect;
            service.Error -= OnErrorMessageReceived;
            // service.MessageReceived_Globals -= OnGlobalsMessageReceived;
            // service.MessageReceived_GlobalUpdated -= OnGlobalsUpdatedReceived;
            service.MessageReceived_AuthenticateRequest -= OnAuthenticateRequestReceived;
            UpdateStatusIcon();
            _subscribedInstances.Remove(service);
        }

        private void OnAuthenticateRequestReceived(object sender, string e)
        {
            try
            {
                var source = sender as WebSocketService;
                if (source == null) return;

                string profileId = _websocketProfileManager.GetProfileIdByService(source);
                var profile = _profile.LoadProfile(profileId);

                var jObject = JObject.Parse(e);
                if (jObject["request"]?.ToString() == "Hello")
                {
                    var authentication = jObject["authentication"];
                    if (authentication != null)
                    {
                        string salt = authentication["salt"]?.ToString();
                        string challenge = authentication["challenge"]?.ToString();

                        source.Authenticate(profile.Password, salt, challenge);
                    }
                }
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Error(PluginInstance.Main, $"Failed to parse WebSocket Hello message: {ex.Message}");
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
                    _statusButton.BackgroundImage = GetStatusIconWithOverlay();
                });
            }
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _connectionCount++;
            MacroDeckLogger.Info(PluginInstance.Main, "WebSocket connected. In Main.cs");
            UpdateStatusIcon();
        }

        private void OnDisconnect(object sender, EventArgs e)
        {
            _connectionCount--;
            MacroDeckLogger.Info(PluginInstance.Main, "WebSocket disconnected. In Main.cs");
            UpdateStatusIcon();
        }

        private void OnErrorMessageReceived(object sender, string e)
        {
            var source = sender as WebSocketService;
            if (source == null) return;

            MacroDeckLogger.Warning(PluginInstance.Main, $"WebSocket error in Main.cs: {e} for profile {source.ProfileId}");
            if (e.Contains("canceled"))
            {
                return;
            }

            UpdateStatusIcon();
        }

        private Image GetStatusIconWithOverlay()
        {
            var totalProfile = _profile.GetAllProfiles().Count();
            var services = _websocketProfileManager.GetAllServices().ToList();

            bool hasError = services.Any(ws => ws.HasError);

            MacroDeckLogger.Info(PluginInstance.Main, $"[StatusIcon] Total connectionCount: {_connectionCount}, totalProfile: {totalProfile}, HasError: {hasError}");

            Color statusColor;

            if (_connectionCount == 0 || totalProfile == 0)
            {
                statusColor = Color.Gray; // Disconnected
                // MacroDeckLogger.Info(PluginInstance.Main, "[StatusIcon] Status: Gray (Disconnected)");
            }
            else if (totalProfile == _connectionCount)
            {
                statusColor = Color.LimeGreen; // Fully connected
                // MacroDeckLogger.Info(PluginInstance.Main, "[StatusIcon] Status: GREEN (Fully connected)");
            }
            else if (hasError)
            {
                statusColor = Color.Red; // Error detected
                // MacroDeckLogger.Info(PluginInstance.Main, "[StatusIcon] Status: RED (Error detected)");
            }
            else
            {
                statusColor = Color.Orange; // Partial connection
                // MacroDeckLogger.Info(PluginInstance.Main, "[StatusIcon] Status: ORANGE (Partial connection)");
            }

            Bitmap result = new Bitmap(MrVibesRSA.StreamerbotPlugin.Properties.Resources.streamerbot_logo_transparent);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int size = 196;
                int padding = 25;
                int x = result.Width - size - padding;
                int y = result.Height - (size + 25) - padding;

                using (Brush brush = new SolidBrush(statusColor))
                {
                    g.FillEllipse(brush, x, y, size, size);
                }
            }

            return result;
        }
    }
}

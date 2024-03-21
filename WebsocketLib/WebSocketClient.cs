using Newtonsoft.Json;
using SuchByte.MacroDeck.Logging;
using SuchByte.MacroDeck.Plugins;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace MrVibes_RSA.StreamerbotPlugin
{
    internal class WebSocketClient
    {
        private static WebSocketClient _instance;
        private static readonly object _lock = new object();

        // Global variable to store the URI
        private static string _serverUri;
        private static bool _isConnected;

        public static event EventHandler WebSocketConnected;
        public static event EventHandler<CloseEventArgs> WebSocketDisconnected;
        public static event EventHandler<string> WebSocketOnMessageRecieved_actions;
        public static event EventHandler<string> WebSocketOnMessageRecieved_globals;


        private WebSocket ws;

        public static WebSocketClient Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new WebSocketClient();
                    }
                    return _instance;
                }
            }
        }

        // New property to expose connection status
        public static bool IsConnected
        {
            get { return _isConnected; }
        }

        private WebSocketClient()
        {
            if (PluginConfiguration.GetValue(PluginInstance.Main, "Configured") == "True")
            {
                string address = PluginConfiguration.GetValue(PluginInstance.Main, "Address");
                int port;
                string endpoint = PluginConfiguration.GetValue(PluginInstance.Main, "Endpoint");
                Uri serverUri = null;

                if (int.TryParse(PluginConfiguration.GetValue(PluginInstance.Main, "Port"), out port))
                {
                    // Port parsing successful, use the 'port' variable here
                    UriBuilder uriBuilder = new UriBuilder("ws", address, port, endpoint);
                    serverUri = uriBuilder.Uri;
                    RetryConnect(serverUri.ToString());
                }
                else
                {

                }
            }  
        }

        public void Connect(string serverUri)
        {
            // Store the URI in the global variable
            _serverUri = serverUri;

            if (ws != null && ws.ReadyState == WebSocketState.Open)
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket already connected.");
                //ws.Close();
            }

            ws = new WebSocket(serverUri);

            ws.EmitOnPing = true;

            ws.OnOpen += Ws_OnOpen;
            ws.OnClose += Ws_OnClose;
            ws.OnMessage += Ws_OnMessageReceived;
            ws.OnError += Ws_OnError;

            ws.Connect();
        }

        public void RetryConnect(string serverUri)
        {
            try
            {
                if(_serverUri == null)
                {
                    _serverUri = serverUri;
                }

                Connect(_serverUri);
            }
            catch (Exception ex)
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket connection attempt failed. Retrying... ({ex.Message})");
                // Retry after a delay
                Thread.Sleep(5000);
                Connect(_serverUri);
            }
        }

        private void Ws_OnOpen(object sender, EventArgs e)
        {
           
            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Connected");

            WebSocketConnected?.Invoke(null, EventArgs.Empty);

            _isConnected = true;

            ConfirmConnection(@"
            {
                ""request"": ""GetInfo"",
                ""id"": ""MacroDeck Connecting""
            }");
        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Disconnected: Code:{e.Code}, Reason {e.Reason}");

            //Code:1006 Streamerbot isnt open

            WebSocketDisconnected?.Invoke(sender, e);
            _isConnected = false;
        }

        public void ConfirmConnection(string message)
        {

            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Send Message: {JsonConvert.SerializeObject(message)}");
            ws.Send(message);
        }

        public void SendMessage(string message)
        {

            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Send Message: {JsonConvert.SerializeObject(message)}");
            ws.Send(message);

        }

        private void Ws_OnMessageReceived(object sender, MessageEventArgs e)
        {
            HandleRecievedMessages(e);
        }

        private void HandleRecievedMessages(MessageEventArgs e)
        {
            Task.Run(() =>
            {
                if (e.IsPing)
                {
                    //MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Message Recieved ping");
                    return;
                }

                if (e.IsText)
                {
                    if (e.Data == string.Empty)
                    {
                        return;
                    }

                    MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Message Recieved: {e.Data}");

                    if (e.Data.Contains("\"source\":\"websocketServer\""))
                    {
                        SubscribeToCustom();
                        return;
                    }

                    if (e.Data.Contains("\"actions\":[{\"id\""))
                    {
                        WebSocketOnMessageRecieved_actions?.Invoke(this, e.Data);
                        return;
                    }

                    if (e.Data.Contains("\"type\":\"Custom\""))
                    {
                        WebSocketOnMessageRecieved_globals?.Invoke(this, e.Data);
                        return;
                    }

                    return;
                }

                if (e.IsBinary)
                {
                    // Do something with e.RawData.
                    MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Message Recieved Raw: {e.RawData}");
                    return;
                }
            });
        }

        private void SubscribeToCustom()
        {
            string requestId = Guid.NewGuid().ToString();
            string jsonString = @"
            {
                ""request"": ""Subscribe"",
                ""id"": """ + requestId + @""",
                ""events"": {
                    ""General"": [
                        ""Custom""
                    ]
                }
            }";

            // Retry logic
            int retryCount = 5; // Number of retries
            bool messageSent = false;
            while (retryCount > 0 && !messageSent)
            {
                try
                {
                    SendMessage(jsonString);
                    messageSent = true;
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"Failed to send message: {ex.Message}");
                    retryCount--;
                    if (retryCount == 0)
                    {
                        MacroDeckLogger.Error(PluginInstance.Main, $"Failed to send message after multiple attempts.");
                        // Handle failure after multiple attempts, e.g., log an error, notify the user, etc.
                    }
                    else
                    {
                        MacroDeckLogger.Info(PluginInstance.Main, $"Retrying to send message... Attempts left: {retryCount}");
                        // Wait before retrying
                        Thread.Sleep(1000); // You can adjust the delay as needed
                    }
                }
            }
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket Error: {e.Message} :: {e.Exception}");
        }

        public void Close()
        {
            if (ws != null && ws.ReadyState == WebSocketState.Open)
            {
                ws.Close();
            }
        }
    }
}

using MrVibesRSA.StreamerbotPlugin.Models;
using MrVibesRSA.StreamerbotPlugin.Utilities;
using Newtonsoft.Json;
using SuchByte.MacroDeck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Services
{
    public class WebSocketService : IDisposable
    {
        private readonly Dictionary<string, Action<string>> _messageHandlers = new();
        private readonly Queue<MessageData> _messageQueue = new();

        private Uri _serverUri;
        public Uri Uri => _serverUri;

        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;

        private bool _isConnected;
        public bool IsConnected => _isConnected;
        public bool HasError { get; private set; }

        public bool IsIntentionalDisconnect { get; set; }
        public bool EventsSubscribed { get; private set; }

        public string ProfileId { get; }

        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler<bool> Authenticated;
        public event EventHandler<string> Error;

        public event EventHandler<string> MessageReceived;
        public event EventHandler<string> MessageReceived_Hello;
        public event EventHandler<string> MessageReceived_AuthenticateRequest;
        public event EventHandler<string> MessageReceived_Info;
        public event EventHandler<string> MessageReceived_Actions;
        public event EventHandler<string> MessageReceived_Globals;
        public event EventHandler<string> MessageReceived_Events;
        public event EventHandler<string> MessageReceived_EventFired;
        // public event EventHandler<string> MessageReceived_GlobalUpdated;

        public WebSocketService(string profileId)
        {
            ProfileId = profileId;
            InitializeMessageHandlers();
        }

        private void InitializeMessageHandlers()
        {
            _messageHandlers.Add("\"request\":\"Hello\"", HandleHelloRequest);
            _messageHandlers.Add("\"id\":\"GetConnectionInfoForMacroDeck\"", HandleGetInfo);
            _messageHandlers.Add("\"id\":\"GetAuthenticationForMacroDeck\",\"status\":\"ok\"", HandleAuthenticated);
            _messageHandlers.Add("\"id\":\"GetAuthenticationForMacroDeck\",\"status\":\"error\"", HandleAuthenticationError);
            _messageHandlers.Add("\"id\":\"GetActionsForMacroDeck\"", HandleGetActions);
            _messageHandlers.Add("\"id\":\"GetGlobalsForMacroDeck\"", HandleGetGlobalVariables);
            _messageHandlers.Add("\"id\":\"GetEventsForMacroDeck\"", HandleGetEvents);
            _messageHandlers.Add("\"id\":\"SubscribeToEventsForMacroDeck\"", HandleSubscriptionNotification);
            _messageHandlers.Add("\"id\":\"UnsubscribeFromEventsForMacroDeck\"", HandleUnsubscriptionNotification);
            _messageHandlers.Add("\"event\":", HandleSubcribedEventFired);
            _messageHandlers.Add("\"id\":\"DoActionForMacroDeck\"", HandleDoActionNotification);

        }

        public async Task ConnectAsync(string uri)
        {
            if (_webSocket != null && (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.Connecting))
                return;

            IsIntentionalDisconnect = false;

            _serverUri = new Uri(uri);
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();

            try
            {
                await _webSocket.ConnectAsync(_serverUri, _cts.Token);
                _isConnected = true;
            }
            catch (Exception ex)
            {
                SetError($"[{ProfileId}] Connect Error: {ex.Message}");
                return;
            }

            _ = Task.Run(async () =>
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    await ReceiveMessagesAsync();
                }

                Disconnected?.Invoke(this, EventArgs.Empty);
                _isConnected = false;
            });

            FlushMessageQueue();
        }

        private void FlushMessageQueue()
        {
            while (_messageQueue.TryDequeue(out var msg))
            {
                SendWebSocketMessage(msg.Request, msg.Id, msg.Events);
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[4096];
            var messageBuffer = new List<byte>();

            try
            {
                WebSocketReceiveResult result;

                do
                {
                    result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    messageBuffer.AddRange(buffer.Take(result.Count));

                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _cts.Token);
                    return;
                }

                var message = Encoding.UTF8.GetString(messageBuffer.ToArray());

                bool handled = false;
                foreach (var kvp in _messageHandlers)
                {
                    if (message.Contains(kvp.Key))
                    {
                        kvp.Value.Invoke(message);
                        handled = true;
                        break;
                    }
                }
                if (!handled)
                {
                    MessageReceived?.Invoke(this, message);
                }

                MessageReceived?.Invoke(this, message);
                // MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] WebSocket raw message received: {message}");
            }
            catch (OperationCanceledException) when (IsIntentionalDisconnect)
            {
                // Graceful shutdown — don't log as error
                MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] Receive loop canceled due to intentional disconnect.");
            }
            catch (Exception ex)
            {
                SetError($"[{ProfileId}] Receive Error: {ex.Message}");
            }

        }

        private async void SendWebSocketMessage(string request, string id, Dictionary<string, List<string>> events = null)
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var messageObject = new
                    {
                        request,
                        id,
                        events
                    };

                    string message = JsonConvert.SerializeObject(messageObject, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    // MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket raw send: {message}");

                    var data = Encoding.UTF8.GetBytes(message);
                    await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
                }
                catch (ObjectDisposedException ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"[{ProfileId}] WebSocket disposed: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"[{ProfileId}] Failed to send message: {ex}");
                }
            }
            else
            {
                _messageQueue.Enqueue(new MessageData(request, id, events));
            }
        }

        private async void SendWebSocketMessage(string request, string id, string actionId, string value = null)
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                try
                {
                    var messageObject = new
                    {
                        request = "DoAction",
                        action = new { id = actionId },
                        args = new { key = value },
                        id
                    };

                    string message = JsonConvert.SerializeObject(messageObject, Formatting.None,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var data = Encoding.UTF8.GetBytes(message);
                    MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] WebSocket action sent: {message}");
                    await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
                }
                catch (ObjectDisposedException ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"[{ProfileId}] WebSocket disposed: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MacroDeckLogger.Error(PluginInstance.Main, $"[{ProfileId}] Failed to send action message: {ex}");
                }
            }
            else
            {
                _messageQueue.Enqueue(new MessageData("DoAction", id, null) { ActionId = actionId });
            }
        }

        private async void SendAuthenticationWebSocketMessage(string request, string id, string authenticationString)
        {
            var messageObject = new
            {
                id = id,
                request = request,
                authentication = authenticationString
            };

            string message = JsonConvert.SerializeObject(messageObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = Encoding.UTF8.GetBytes(message);
            MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] WebSocket auth sent: {message}");

            await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
        }

        public async Task SendMessageAsync(string message)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] WebSocket raw message sent: {message}");

            if (_webSocket.State != WebSocketState.Open) return;

            var data = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
        }

        public void Close()
        {
            IsIntentionalDisconnect = true;
            _cts.Cancel();
            _webSocket?.Dispose();
        }

        private void HandleConnected()
        {
            Connected?.Invoke(this, EventArgs.Empty);
            _isConnected = true;
            // InitializeServices();
        }

        private void HandleHelloRequest(string obj)
        {
            if (obj.Contains("salt"))
            {
                HandleAuthenticateRequest(obj);
            }
            else
            {
                MessageReceived_Hello?.Invoke(this, obj);
                HandleConnected();
            }
        }

        private void HandleAuthenticateRequest(string obj)
        {
            MessageReceived_AuthenticateRequest?.Invoke(this, obj);
        }

        private void HandleAuthenticated(string obj)
        {
            Authenticated?.Invoke(this, true);
            HandleConnected();
        }

        private void HandleAuthenticationError(string obj)
        {
            Authenticated?.Invoke(this, false);
            if (obj.Contains("\"error\":\"Already authenticated\""))
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] Already authenticated: {obj}");
                return;
            }

            SetError($"[{ProfileId}] Authentication error: {obj}");
        }

        private void HandleGetInfo(string obj) => MessageReceived_Info?.Invoke(this, obj);

        private void HandleGetActions(string obj) => MessageReceived_Actions?.Invoke(this, obj);

        private void HandleGetGlobalVariables(string obj) => MessageReceived_Globals?.Invoke(this, obj);

        private void HandleGetEvents(string obj) => MessageReceived_Events?.Invoke(this, obj);

        private void HandleSubscriptionNotification(string obj) => MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] Subscribed to: {obj}");

        private void HandleUnsubscriptionNotification(string obj) => MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] Unsubscribed from: {obj}");

        private void HandleSubcribedEventFired(string obj) => MessageReceived_EventFired?.Invoke(this, obj);

        private void HandleDoActionNotification(string obj) => MacroDeckLogger.Info(PluginInstance.Main, $"[{ProfileId}] Do Action: {obj}");

        private void InitializeServices()
        {
            GetActionsList();
            GetGlobalsList();
            SubscribeToGlobalVariableUpdated();
        }

        public void Authenticate(string password, string salt, string challenge)
        {
            string authenticationString = GenerateAuthenticationString(password, salt, challenge);
            SendAuthenticationWebSocketMessage("Authenticate", "GetAuthenticationForMacroDeck", authenticationString);
        }

        public void GetConnectionInfo() => SendWebSocketMessage("GetInfo", "GetConnectionInfoForMacroDeck");

        public void GetActionsList() => SendWebSocketMessage("GetActions", "GetActionsForMacroDeck");

        public void GetGlobalsList() => SendWebSocketMessage("GetGlobals", "GetGlobalsForMacroDeck");

        public void GetEventList() => SendWebSocketMessage("GetEvents", "GetEventsForMacroDeck");

        public void DoAction(string actionId, string value)
        {
            SendWebSocketMessage("DoAction", "DoActionForMacroDeck", actionId, value);
        }

        private void SubscribeToGlobalVariableUpdated()
        {
            var events = new Dictionary<string, List<string>>
            {
                { "Misc", new List<string> { "GlobalVariableUpdated" } }
            };

            SendWebSocketMessage("Subscribe", "SubscribeToGlobalVariableUpdated", events);
        }

        public void SubscribeToStreamerbotEvents(string categoryName, string eventName) 
        {
            var events = new Dictionary<string, List<string>>
            {
                {
                    CleanCategoryName(categoryName), new List<string> { eventName }
                }
            };

            SendWebSocketMessage("Subscribe", "SubscribeToEventsForMacroDeck", events);
        }

        public void UnsubscribeFromStreamerbotEvents(string categoryName, string eventName)
        {
            var events = new Dictionary<string, List<string>>
            {
                {
                    CleanCategoryName(categoryName), new List<string> { eventName }
                }
            };

            SendWebSocketMessage("UnSubscribe", "UnsubscribeFromEventsForMacroDeck", events);
        }

        private string CleanCategoryName(string name)
        {
            return name.EndsWith(" *") ? name.Substring(0, name.Length - 2) : name;
        }

        public void SetError(string message)
        {
            HasError = true;
            Error?.Invoke(this, message);
            MacroDeckLogger.Warning(PluginInstance.Main, message);
        }

        public void Dispose()
        {
            _webSocket?.Dispose();
        }

        private string GenerateAuthenticationString(string password, string salt, string challenge)
        {
            return EncryptionHelper.GenerateAuthenticationString(password, salt, challenge);
        }
    }
}


/*
    public class WebSocketService : IDisposable
    {
        private readonly Dictionary<string, Action<string>> _messageHandlers = new Dictionary<string, Action<string>>();
        private Queue<MessageData> _messageQueue = new Queue<MessageData>();

        private Uri _serverUri;
        public Uri Uri => _serverUri;
  
        private ClientWebSocket _webSocket;
        private CancellationTokenSource _cts;

        private bool _isConnected;
        public bool IsConnected => _isConnected;
        public bool HasError { get; private set; }

        public bool IsIntentionalDisconnect { get; set; }
        public bool EventsSubscribed { get; private set; }

        // Connection Events
        public event EventHandler Connected;
        public event EventHandler Disconnected;
        public event EventHandler<bool> Authenticated;
        public event EventHandler<string> Error;

        // General Message Event
        public event EventHandler<string> MessageReceived;

        // Typed Message Events
        public event EventHandler<string> MessageReceived_Hello;
        public event EventHandler<string> MessageReceived_AuthenticateRequest;
        public event EventHandler<string> MessageReceived_Info;
        public event EventHandler<string> MessageReceived_Actions;
        public event EventHandler<string> MessageReceived_Globals;
        public event EventHandler<string> MessageReceived_GlobalUpdated;


        private static WebSocketService _instance;
        public static WebSocketService Instance => _instance ??= new WebSocketService();
        // private readonly Dictionary<string, WebSocketService> _webSocketConnections = new();

        public WebSocketService()
        {
            InitializeMessageHandlers();
        }

        private void InitializeMessageHandlers()
        {
            _messageHandlers.Add("\"request\":\"Hello\"", HandleHelloRequest);
            _messageHandlers.Add("\"id\":\"GetConnectionInfoForMacroDeck\"", HandleGetInfo);
            _messageHandlers.Add("\"id\":\"GetAuthenticationForMacroDeck\",\"status\":\"ok\"", HandleAuthenticated);
            _messageHandlers.Add("\"id\":\"GetAuthenticationForMacroDeck\",\"status\":\"error\"", HandleAuthenticationError);
            _messageHandlers.Add("\"id\":\"GetActionsForMacroDeck\"", HandleGetActions);
            _messageHandlers.Add("\"id\":\"GetGlobalsForMacroDeck\"", HandleGetGlobalVariables);
            _messageHandlers.Add("\"id\":\"SubscribeToGlobalVariableUpdated\"", HandleSubscription);
            _messageHandlers.Add("\"id\":\"DoActionForMacroDeck\"", HandleDoAction);
            _messageHandlers.Add("\"type\":\"GlobalVariableUpdated\"", HandleGlobalVariableUpdate);
        }

        public async Task ConnectAsync(string uri)
        {
            if (_webSocket != null && (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.Connecting))
                return;

            IsIntentionalDisconnect = false;

            _serverUri = new Uri(uri);
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();

            try
            {
                await _webSocket.ConnectAsync(_serverUri, _cts.Token);
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, ex.Message);
                return; // Don't retry here; let manager handle it
            }

            _ = Task.Run(async () =>
            {
                while (_webSocket.State == WebSocketState.Open)
                {
                    await ReceiveMessagesAsync();
                }

                Disconnected?.Invoke(this, EventArgs.Empty);
                _isConnected = false;
            });

            FlushMessageQueue();
        }

        private void FlushMessageQueue()
        {
            while (_messageQueue.TryDequeue(out var msg))
            {
                SendWebSocketMessage(msg.Request, msg.Id, msg.Events);
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[4096];
            var messageBuffer = new List<byte>();

            try
            {
                WebSocketReceiveResult result;

                do
                {
                    result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), _cts.Token);
                    messageBuffer.AddRange(buffer.Take(result.Count));

                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", _cts.Token);
                    return;
                }

                var message = Encoding.UTF8.GetString(messageBuffer.ToArray());

                foreach (var key in _messageHandlers.Keys)
                {
                    if (message.Contains(key))
                    {
                        _messageHandlers[key](message);
                        return;
                    }
                }

                MessageReceived?.Invoke(this, message);
                MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket message Recieved: {message}");
            }
            catch (Exception ex)
            {
                Error?.Invoke(this, ex.Message);
                MacroDeckLogger.Warning(PluginInstance.Main, $"Error while receiving WebSocket message: {ex.Message}");
            }
        }

        private async void SendWebSocketMessage(string request, string id, Dictionary<string, List<string>> events = null)
        {
            if (_isConnected)
            {
                var messageObject = new
                {
                    request,
                    id,
                    events
                };

                string message = JsonConvert.SerializeObject(messageObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = Encoding.UTF8.GetBytes(message);
                await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
            }
            else
            {
                _messageQueue.Enqueue(new MessageData(request, id, events));
            }
        }

        private async void SendWebSocketMessage(string request, string id, string actionId, string value = null)
        {
            if (_isConnected)
            {
                var messageObject = new
                {
                    request = "DoAction",
                    action = new
                    {
                        id = actionId
                    },
                    args = new
                    {
                        key = value
                    },
                    id = id
                };

                string message = JsonConvert.SerializeObject(messageObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var data = Encoding.UTF8.GetBytes(message);
                MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket request message sent: {message}");
                await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
            }
            else
            {
                _messageQueue.Enqueue(new MessageData("DoAction", id, null)
                {
                    ActionId = actionId
                });
            }
        }

        private async void SendAuthenticationWebSocketMessage(string request, string id, string authenticationString)
        {
            var messageObject = new
            {
                id = id,
                request = request,
                authentication = authenticationString
            };

            string message = JsonConvert.SerializeObject(messageObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = Encoding.UTF8.GetBytes(message);
            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket authentication reqeust message sent: {message}");

            await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
        }

        public async Task SendMessageAsync(string message)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"WebSocket message sent: {message}");

            if (_webSocket.State != WebSocketState.Open) return;

            var data = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(data), WebSocketMessageType.Text, true, _cts.Token);
        }

        public void Close()
        {
            IsIntentionalDisconnect = true;
            _cts.Cancel();
            _webSocket?.Dispose();
        }

        private void HandleConnected()
        {
            Connected?.Invoke(this, EventArgs.Empty);
            _isConnected = true;
            InitializeServices();
        }

        private void HandleHelloRequest(string obj)
        {
            if (obj.Contains("salt"))
            {
                HandleAuthenticateRequest(obj);
            }
            else
            {
                MessageReceived_Hello?.Invoke(this, obj);
                HandleConnected();
            }
        }

        private void HandleAuthenticateRequest(string obj)
        {
            MessageReceived_AuthenticateRequest?.Invoke(this, obj);
        }

        private void HandleAuthenticated(string obj)
        {
            bool isAuthenticated = true;
            Authenticated?.Invoke(this, isAuthenticated);
            HandleConnected();
        }

        private void HandleAuthenticationError(string obj)
        {
            bool isAuthenticated = false;
            Authenticated?.Invoke(this, isAuthenticated);
            if(obj.Contains("\"error\":\"Already authenticated\""))
            {
                MacroDeckLogger.Info(PluginInstance.Main, $"Already authenticated: {obj}");
                return;
            }

            SetError(obj);
            MacroDeckLogger.Error(PluginInstance.Main, $"Error Authenticating: {obj}");
        }

        private void HandleGetInfo(string obj)
        {
            MessageReceived_Info?.Invoke(this, obj);
        }

        private void HandleGetActions(string obj)
        {
            MessageReceived_Actions?.Invoke(this, obj);
        }

        private void HandleGetGlobalVariables(string obj)
        {
            MessageReceived_Globals?.Invoke(this, obj);
        }

        private void HandleSubscription(string obj)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"Subscribed to GlobalVariableUpdated: {obj}");
        }

        private void HandleGlobalVariableUpdate(string obj)
        {
            MessageReceived_GlobalUpdated?.Invoke(this, obj);
        }

        private void HandleDoAction(string obj)
        {
            MacroDeckLogger.Info(PluginInstance.Main, $"Do Action: {obj}");
        }

        private void InitializeServices()
        {
            GetActionsList();                       // Fetches available actions
            GetGlobalsList();                       // Fetches global variables
            SubscribeToGlobalVariableUpdated();     // Enables real-time updates
        }

        public void Authenticate(string password, string salt, string challenge)
        {
            string authenticationString = GenerateAuthenticationString(password, salt, challenge);
            SendAuthenticationWebSocketMessage("Authenticate", "GetAuthenticationForMacroDeck", authenticationString);
        }

        public void GetConnectionInfo()
        {
            SendWebSocketMessage("GetInfo", "GetConnectionInfoForMacroDeck");
        }

        public void GetActionsList()
        {
            SendWebSocketMessage("GetActions", "GetActionsForMacroDeck");
        }

        public void GetGlobalsList()
        {
            SendWebSocketMessage("GetGlobals", "GetGlobalsForMacroDeck");
        }

        public void DoAction(string actionId, string value)
        {
            SendWebSocketMessage("DoAction", "DoActionForMacroDeck", actionId, value);
        }

        private void SubscribeToGlobalVariableUpdated()
        {
            var events = new Dictionary<string, List<string>>
            {
                { "Misc", new List<string> { "GlobalVariableUpdated" } }
            };

            SendWebSocketMessage("Subscribe", "SubscribeToGlobalVariableUpdated", events);
        }

        public void SetError(string message)
        {
            HasError = true;
            Error?.Invoke(this, message);
        }

        private string GenerateAuthenticationString(string password, string salt, string challenge)
        {
            // Check for empty inputs
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(challenge))
            {
                // Debug.LogError("Password, Salt, or Challenge is empty.");
                return string.Empty;
            }

            // Step 1: Concatenate password and salt
            string passwordSaltConcat = password + salt;

            // Step 2: Generate SHA-256 binary hash of the result
            byte[] passwordSaltHashByte = SHA256Hash(passwordSaltConcat);

            // Step 3: Base64 encode the SHA-256 hash
            string base64Secret = Convert.ToBase64String(passwordSaltHashByte);

            // Step 4: Concatenate the base64_secret with the challenge
            string secretChallengeConcat = base64Secret + challenge;

            // Step 5: Generate SHA-256 hash of this result
            byte[] finalHashByte = SHA256Hash(secretChallengeConcat);
            return Convert.ToBase64String(finalHashByte);
        }

        // SHA-256 method for hashing
        private static byte[] SHA256Hash(string input)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        public void Dispose()
        {
            _webSocket?.Dispose();
        }
*/

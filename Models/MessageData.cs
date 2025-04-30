using System;
using System.Collections.Generic;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    internal class MessageData
    {
        /// <summary>
        /// The raw message string (NEW: Added support for raw message storage)
        /// </summary>
        public string RawMessage { get; }

        /// <summary>
        /// The request type (e.g., "GetInfo", "Subscribe")
        /// </summary>
        public string Request { get; }

        /// <summary>
        /// The message identifier
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Events dictionary for subscription requests
        /// </summary>
        public Dictionary<string, List<string>> Events { get; }

        /// <summary>
        /// Action ID for action-specific messages (NEW: Made nullable)
        /// </summary>
        public string ActionId { get; set; }

        /// <summary>
        /// Timestamp when the message was created (NEW: Added for message expiration)
        /// </summary>
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        /// <summary>
        /// Constructor for structured messages
        /// </summary>
        public MessageData(string request, string id, Dictionary<string, List<string>> events = null)
        {
            Request = request ?? throw new ArgumentNullException(nameof(request));
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Events = events;
        }

        /// <summary>
        /// NEW: Constructor for raw messages
        /// </summary>
        public MessageData(string rawMessage)
        {
            RawMessage = rawMessage ?? throw new ArgumentNullException(nameof(rawMessage));
        }

        /// <summary>
        /// NEW: Determines if the message is expired (default 5 minute TTL)
        /// </summary>
        public bool IsExpired(TimeSpan? timeToLive = null)
        {
            var ttl = timeToLive ?? TimeSpan.FromMinutes(5);
            return DateTime.UtcNow - Timestamp > ttl;
        }

        /// <summary>
        /// NEW: Creates a copy of the message with current timestamp
        /// </summary>
        public MessageData Clone()
        {
            if (RawMessage != null)
            {
                return new MessageData(RawMessage);
            }

            var clone = new MessageData(Request, Id, Events != null
                ? new Dictionary<string, List<string>>(Events)
                : null)
            {
                ActionId = ActionId
            };
            return clone;
        }
    }
}

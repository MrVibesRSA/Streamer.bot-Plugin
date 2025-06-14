using Newtonsoft.Json;
using System.Collections.Generic;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class GetEventsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("events")]
        public Dictionary<string, List<string>> Events { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class EventWithSubscription
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; } = false;
    }
}

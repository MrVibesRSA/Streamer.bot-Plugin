using Newtonsoft.Json;
using System.Collections.Generic;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class GetActionsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("actions")]
        public List<ActionItem> Actions { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ActionItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("subaction_count")]
        public int SubactionCount { get; set; }

        [JsonProperty("trigger_count")]
        public int TriggerCount { get; set; }
    }

}

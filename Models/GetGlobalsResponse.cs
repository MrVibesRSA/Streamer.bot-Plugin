using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class GetGlobalsResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("variables")]
        public Dictionary<string, GlobalVariable> Variables { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class GlobalVariable
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("lastWrite")]
        public DateTime LastWrite { get; set; }
    }
}

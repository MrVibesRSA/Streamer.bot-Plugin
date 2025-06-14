using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class GetConnectionInfoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("info")]
        public ConnectionInfo Info { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class ConnectionInfo
    {
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("os")]
        public string OS { get; set; }

        [JsonProperty("osVersion")]
        public string OSVersion { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

}

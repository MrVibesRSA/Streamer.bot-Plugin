using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrVibesRSA.StreamerbotPlugin.Models
{
    public class ActionItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string group { get; set; }
        public bool enabled { get; set; }
        public int subaction_count { get; set; }
        public int trigger_count { get; set; }
    }

    public class GetActionsResponse
    {
        public string id { get; set; }
        public int count { get; set; }
        public List<ActionItem> actions { get; set; }
        public string status { get; set; }
    }
}

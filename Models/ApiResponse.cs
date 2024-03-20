using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrVibes_RSA.StreamerbotPlugin.Models
{
    public class ApiResponse
    {
        public int count { get; set; }
        public Action[] actions { get; set; }
    }

    // Define classes to match the JSON structure
    public class Action
    {
        public string id { get; set; }
        public string name { get; set; }
        public string group { get; set; }
        public bool enabled { get; set; }
        public int subactions_count { get; set; }

        public Action(string id, string name)
        {
            this.id = id;
            this.name = name;
        }

        // Override the ToString() method to return the action's name
        public override string ToString()
        {
            return name;
        }
    }
}

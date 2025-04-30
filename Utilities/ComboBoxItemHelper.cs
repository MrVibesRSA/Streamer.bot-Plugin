using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MrVibesRSA.StreamerbotPlugin.Utilities
{
    public class ComboBoxItemHelper
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public ComboBoxItemHelper(string text, object value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text; // Important for display
        }
    }
}

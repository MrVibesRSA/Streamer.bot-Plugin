using SuchByte.MacroDeck.Variables;

namespace MrVibes_RSA.StreamerbotPlugin.Models
{
    public static class VariableTypeHelper
    {
        public static VariableType GetVariableType(object value)
        {
            if (value is int)
            {
                return VariableType.Integer;
            }
            else if (value is bool)
            {
                return VariableType.Bool;
            }
            else if (value is float || value is double)
            {
                return VariableType.Float;
            }
            else if (value is string)
            {
                return VariableType.String;
            }
            else
            {
                // Handle other types as needed
                return VariableType.String; // Default to String if type is unknown
            }
        }
    }
}

using Microsoft.VisualBasic;
using SuchByte.MacroDeck.Variables;
using System;
using System.Reflection.Metadata;

namespace MrVibes_RSA.StreamerbotPlugin.Models
{
    public static class VariableTypeHelper
    {
        public static VariableType GetVariableType(object value)
        {

            if (int.TryParse(value.ToString(), out _))
            {
                return VariableType.Integer;
            }
            else if (float.TryParse(value.ToString(), out _))
            {
                return VariableType.Float;
            }
            else if (bool.TryParse(value.ToString(), out _))
            {
                return VariableType.Bool;
            }
            else
            {
                return VariableType.String; // Default to String if type is unknown
            }
        }
    }
}

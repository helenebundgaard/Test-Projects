using System;
using System.Collections.Generic;

namespace server
{
    public class ColorTheory
    {
        static Dictionary<string, string[]> analogousColors = new Dictionary<string, string[]>
        {
            { "red", new string[] { "orange", "purple" } },
            { "yellow", new string[] { "orange", "green" } },
            { "blue", new string[] { "green", "purple" } },
            { "orange", new string[] { "red", "yellow" } },
            { "purple", new string[] { "blue", "red" } },
            { "green", new string[] { "blue", "yellow" } }
        };

        static Dictionary<string, string> complementaryColors = new Dictionary<string, string>
        {
            { "red", "green" },
            { "yellow", "purple" },
            { "blue", "orange" },
            { "orange", "blue" },
            { "purple", "yellow" },
            { "green", "red" }
        };

        static Dictionary<string, string> colorAdditions = new Dictionary<string, string>
        {
            { "red+yellow", "orange" },
            { "blue+red", "purple" },
            { "blue+yellow", "green" },
            { "blue+red+yellow", "brown" },
            { "blue+orange", "brown" },
            { "purple+yellow", "brown" },
            { "green+red", "brown" }
        };


        public static string[] GetAnalogousColors(string color)
        {
            return analogousColors.ContainsKey(color) ? analogousColors[color] : new string[] { };
        }

        public static string GetComplementaryColor(string color)
        {
            return complementaryColors.ContainsKey(color) ? complementaryColors[color] : null;
        }

        public static string AddColors(params string[] colors)
        {
            Array.Sort(colors);
            string key = string.Join("+", colors);
            return colorAdditions.ContainsKey(key) ? colorAdditions[key] : "undefined combination";
        }
    }
}


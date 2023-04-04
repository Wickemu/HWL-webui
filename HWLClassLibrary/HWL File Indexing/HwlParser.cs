namespace HwlFileAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class HwlParser
    {
        // "*/"
        public static List<string> ParseDescriptions(string line)
        {
            List<string> items = line.Split("*/", StringSplitOptions.None).ToList();
            return items;
        }
        public static string UnparseDescriptions(List<string> items)
        {
            string line = string.Join("*/", items);
            return line;
        }

        // "+|"
        public static List<string> ParseHeaderLine(string line)
        {
            List<string> items = Regex.Split(line, @"\+\|").ToList();

            return items;
        }
        public static string UnparseHeaderLine(List<string> items)
        {
            string line = string.Join("+|", items);
            return line;
        }

        // "^"
        public static List<string> ParseHeaderSub(string line)
        {
            List<string> items = line.Split("^", StringSplitOptions.None).ToList();
            return items;
        }
        public static string UnparseHeaderSub(List<string> items)
        {
            string line = string.Join("^", items);
            return line;
        }


        // "\"
        public static List<string> ParseMultiLine(string line)
        {
            List<string> items = line.Split("\\", StringSplitOptions.None).ToList();
            return items;
        }
        public static string UnparseMultiLine(List<string> items)
        {
            string line = string.Join("\\", items);
            return line;
        }

        // "|"
        public static List<string> ParsePlotLine(string line)
        {
            List<string> items = line.Split("|", StringSplitOptions.None).ToList();
            return items;
        }
        public static string UnparsePlotLine(List<string> items)
        {
            string line = string.Join("|", items);
            return line;
        }
    }
}

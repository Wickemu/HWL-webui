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

        // "+|"
        public static List<string> ParseHeaderLine(string line)
        {
            //original method
            //List<string> items = line.Split("+|", StringSplitOptions.None).ToList();
            //GPT-4's suggestion
            List<string> items = Regex.Split(line, @"\+\|").ToList();

            return items;
        }

        // "^"
        public static List<string> ParseHeaderSub(string line)
        {
            List<string> items = line.Split("^", StringSplitOptions.None).ToList();
            return items;
        }

        // "\"
        public static List<string> ParseMultiLine(string line)
        {
            List<string> items = line.Split("\\", StringSplitOptions.None).ToList();
            return items;
        }
        // "|"
        public static List<string> ParsePlotLine(string line)
        {
            List<string> items = line.Split("|", StringSplitOptions.None).ToList();
            return items;
        }
    }
}

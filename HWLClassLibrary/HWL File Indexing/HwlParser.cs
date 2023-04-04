using System.Text.RegularExpressions;

namespace HwlFileAnalyzer;

public static class HwlParser
{
    // "*/"
    public static List<string> ParseDescriptions(string line)
    {
        var items = line.Split("*/").ToList();
        return items;
    }

    public static string UnparseDescriptions(List<string> items)
    {
        var line = string.Join("*/", items);
        return line;
    }

    // "+|"
    public static List<string> ParseHeaderLine(string line)
    {
        var items = Regex.Split(line, @"\+\|").ToList();

        return items;
    }

    public static string UnparseHeaderLine(List<string> items)
    {
        var line = string.Join("+|", items);
        return line;
    }

    // "^"
    public static List<string> ParseHeaderSub(string line)
    {
        var items = line.Split("^").ToList();
        return items;
    }

    public static string UnparseHeaderSub(List<string> items)
    {
        var line = string.Join("^", items);
        return line;
    }


    // "\"
    public static List<string> ParseMultiLine(string line)
    {
        var items = line.Split("\\").ToList();
        return items;
    }

    public static string UnparseMultiLine(List<string> items)
    {
        var line = string.Join("\\", items);
        return line;
    }

    // "|"
    public static List<string> ParsePlotLine(string line)
    {
        var items = line.Split("|").ToList();
        return items;
    }

    public static string UnparsePlotLine(List<string> items)
    {
        var line = string.Join("|", items);
        return line;
    }
}
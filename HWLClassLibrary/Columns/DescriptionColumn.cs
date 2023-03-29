using System.Drawing;


namespace HwlFileAnalyzer.Columns
{
    public class DescriptionColumn : IColumn
    {
        public DescriptionColumn(HwlData hwl)
        {
            Data = hwl.Descriptions;
        }

        public List<DescriptionLine> Data { get; }
        public string Name { get; set; }
        public string HeaderName { get; set; } = "Descriptions";
        public int Width { get; set; } = 60;
        public int Position { get; set; }
        public Color BackgroundColor { get; set; } = Color.White;
        public int NumOfFields => 1;


        public List<string> HeaderItems => new List<string>() { "Descriptions" };

        public string GetPlotLine(double depth)
        {
            if (!Data.Any(x => x.Depth == depth)) return "".PadRight(Width);
            return Data.Find(x => x.Depth == depth).Description.PadRight(Width).Substring(0, Width);
        }

        public void PrintPlotLine(double depth)
        {
            if (!Data.Any(x => x.Depth == depth)) Console.Write("".PadRight(Width));
            else
            {
                var desc = Data.Find(x => x.Depth == depth).Description;
                if (desc.Length <= Width) Console.Write(desc.PadRight(Width).Substring(0, Width));
                else
                {
                    Console.Write(desc.PadRight(Width - 3).Substring(0, Width - 3));
                    Console.Write("...");
                };
            }
        }

        private Dictionary<double, List<string>> SplitDescription(DescriptionLine desc)
        {
            var depth = desc.Depth;
            var text = desc.Description;
            List<string> splitText = new List<string>();
            for (int i = 1; i < text.Length; i++)
            {
                if (i % 50 == 0)
                {
                    splitText.Add(text.Substring(i - 50, 50));
                }
            }
            if (text.Length > splitText.Count * 50) splitText.Add(text.Substring(splitText.Count * 50));
            var dict = new Dictionary<double, List<string>>();
            dict.Add(depth, splitText);
            return dict;
        }

        public void PrintHeaderItem(int i)
        {
            if (i < NumOfFields)
            {
                Color textColor = Color.Black;
                Console.Write(HeaderItems[i].PadRight(Width).Substring(0, Width));
            }
            else Console.Write("".PadRight(Width));
        }
    }
}

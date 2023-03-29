using System.Drawing;


namespace HwlFileAnalyzer.Columns
{
    public class DepthColumn : IColumn
    {
        public DepthColumn(HwlData hwl)
        {
            Data = hwl.Depths;
        }

        public List<double> Data { get; set; }
        public string HeaderName { get; set; } = "Depth";
        public int Width { get; set; } = 5;
        public int Position { get; set; } = 1;
        public Color BackgroundColor { get; set; } = Color.LightGray;

        public int NumOfFields => 1;

        public List<string> HeaderItems => new List<string>() { "Depth" };

        public string GetPlotLine(double depth)
        {
            return depth.ToString().PadRight(Width).Substring(0, Width);
        }

        public void PrintPlotLine(double depth)
        {
            Console.Write(depth.ToString().PadRight(Width).Substring(0, Width));
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

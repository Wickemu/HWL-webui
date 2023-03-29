using System.Drawing;


namespace HwlFileAnalyzer.Columns
{
    public class LithologyColumn : IColumn
    {
        public LithologyColumn(HwlData hwl)
        {
            Data = hwl.Lithology;

        }

        public string HeaderName { get; set; } = "Lithology";
        public bool InterpretedEnabled { get; set; } = false;
        public List<LithologyLine> Data { get; }
        public string Plotline { get; private set; }
        public int Width { get; set; } = 30;
        public int Position { get; set; }
        public Color BackgroundColor { get; set; } = Color.White;
        public int NumOfFields => 1;
        public List<string> HeaderItems => new List<string>() { "Lithology" };


        public string GetPlotLine(double depth)
        {
            var lithLine = Data.Find(x => x.Depth == depth);
            if (lithLine.Lithology1 == 0) return "".PadRight(Width);
            var lithInts = new int[]
            {
                lithLine.Lithology1,
                lithLine.Lithology2,
                lithLine.Lithology3,
                lithLine.Lithology4,
                lithLine.Lithology5,
                lithLine.Lithology6,
                lithLine.Lithology7,
                lithLine.Lithology8,
                lithLine.Lithology9,
                lithLine.Lithology10,
            };
            var lithString = string.Join(" ", lithInts);
            return lithString.PadRight(Width).Substring(0, Width);
        }

        public void PrintPlotLine(double depth)
        {
            var lithLine = Data.Find(x => x.Depth == depth);
            if (lithLine.Lithology1 == 0) Console.Write("".PadRight(Width));
            else
            {
                var lithInts = new int[]
                {
                    lithLine.Lithology1,
                    lithLine.Lithology2,
                    lithLine.Lithology3,
                    lithLine.Lithology4,
                    lithLine.Lithology5,
                    lithLine.Lithology6,
                    lithLine.Lithology7,
                    lithLine.Lithology8,
                    lithLine.Lithology9,
                    lithLine.Lithology10,
                };
                var lithString = string.Join(" ", lithInts);
                Console.Write(lithString.PadRight(Width).Substring(0, Width));
            }
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

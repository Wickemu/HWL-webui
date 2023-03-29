using System.Drawing;


namespace HwlFileAnalyzer.Columns
{
    interface IColumn
    {
        public string HeaderName { get; set; }
        public int Width { get; }
        public string GetPlotLine(double depth);
        public int Position { get; set; }
        public Color BackgroundColor { get; set; }
        public int NumOfFields { get; }
        public List<string> HeaderItems { get; }
        public void PrintPlotLine(double depth);
        public void PrintHeaderItem(int a);
    }
}

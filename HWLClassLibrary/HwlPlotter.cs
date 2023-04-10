using HwlFileAnalyzer.Columns;

namespace HwlFileAnalyzer
{
    public class HwlPlotter
    {
        private HwlData hwl;
        private List<double> rangeIndex;
        private List<double> rangeList;

        private int NumofColumns { get; set; } = 7;
        private Dictionary<int, IColumn> Columns { get; set; }
        private string PlotType { get; set; } //"mud" or "dp"

        public HwlPlotter(HwlData hwlData)
        {
            hwl = hwlData;
            SelectedTop = hwl.TopDepth;
            SelectedBottom = hwl.BottomDepth;
        }

        public double SelectedBottom { get; set; }
        public List<double> SelectedRange => rangeList;
        public List<double> SelectedRangeIndex => rangeIndex;
        public double SelectedTop { get; set; }
        public double Avg(List<int> plotData)
        {
            return Math.Round(plotData.Average(), 0);
        }

        public List<double> GetDpPlot(DrillingParameter dp)
        {
            var dpRange = new List<double>();
            foreach (var i in SelectedRange)
            {
                if (!dp.Data.ContainsKey(i)) dpRange.Add(i);
                else dpRange.Add(dp.Data[i]);
            }

            return dpRange;
        }

        public List<string[]> GetPlot(List<DrillingParameter> dpList)
        {
            List<string[]> plot = new List<string[]>();
            List<string> plotLine = new List<string>();
            plotLine.Add("Depth");
            foreach (var item in dpList)
            {
                plotLine.Add(item.DisplayName);
            }

            plot.Add(plotLine.ToArray());
            plotLine.Clear();
            plotLine.Add("Ft");
            foreach (var item in dpList)
            {
                plotLine.Add(item.UnitOfMeasurement);
            }

            plot.Add(plotLine.ToArray());
            plotLine.Clear();
            foreach (var i in SelectedRange)
            {
                string printedDepth = i.ToString();
                plotLine.Add(printedDepth);
                foreach (var item in dpList)
                {
                    string printedString;
                    if (!item.Data.ContainsKey(i)) printedString = "";
                    else printedString = item.Data[i].ToString();
                    plotLine.Add(printedString);
                }

                plot.Add(plotLine.ToArray());
                plotLine.Clear();
            }

            return plot;
        }

        public double Max(List<double> plotData)
        {
            plotData.RemoveAll(i => i == -999);
            return plotData.Max();
        }

        public double Min(List<double> plotData)
        {
            plotData.RemoveAll(i => i == -999);
            return plotData.Min();
        }

        public double No0Avg(List<double> plotData)
        {
            plotData.RemoveAll(i => i == 0);
            if (plotData.Count == 0)
            {
                return 0;
            }
            else
            {
                return Math.Round(plotData.Average(), 0);

            }
        }

        public void SetRange()
        {
            if (rangeList == null) rangeList = new List<double>();
            else rangeList.Clear();
            if (rangeIndex == null) rangeIndex = new List<double>();
            else rangeIndex.Clear();

            for (int i = 0; i < hwl.Depths.Count; i++)
            {
                if (hwl.Depths[i] >= SelectedTop && hwl.Depths[i] <= SelectedBottom)
                {
                    if (rangeList == null) rangeList = new List<double>();
                    rangeList.Add(hwl.Depths[i]);
                    rangeIndex.Add(i);
                }
            }
        }

        public string PlotLine(double depth)
        {
            var strings = new List<string>();
            for (int i = 0; i < NumofColumns; i++)
            {
                strings.Add(Columns[i].GetPlotLine(depth));
            }

            return string.Join("|", strings);
        }

        public void PrintLine(double depth)
        {
            for (int i = 0; i < NumofColumns; i++)
            {
                Columns[i].PrintPlotLine(depth);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("|");
            }
            Console.Write('\b');
        }

        public string PlotLineHeader()
        {
            int maxFields = 0;
            for (int i = 0; i < NumofColumns; i++)
            {
                if (Columns[i].NumOfFields > maxFields) maxFields = Columns[i].NumOfFields;
            }

            var allLines = new List<string>();
            for (int i = 0; i < maxFields; i++)
            {
                var thisLine = new List<string>();
                for (int a = 0; a < NumofColumns; a++)
                {
                    if (i < Columns[a].NumOfFields) thisLine.Add(Columns[a].HeaderItems[i].PadRight(Columns[a].Width).Substring(0, Columns[a].Width));
                    else thisLine.Add("".PadRight(Columns[a].Width));
                }
                allLines.Add(string.Join("|", thisLine));
            }
            allLines.Add(new String('=', allLines[0].Length));
            return string.Join("\n", allLines);
        }

        public void PrintLineHeader()
        {
            int maxFields = 0;
            for (int i = 0; i < NumofColumns; i++)
            {
                if (Columns[i].NumOfFields > maxFields) maxFields = Columns[i].NumOfFields;
            }

            for (int i = 0; i < maxFields; i++)
            {
                Console.Write("|");
                for (int a = 0; a < NumofColumns; a++)
                {
                    Columns[a].PrintHeaderItem(i);
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            int fullWidth = 0;
            string separator = "|";
            for (int i = 0; i < Columns.Count; i++)
            {
                fullWidth += Columns[i].Width;
                separator += new String('=', Columns[i].Width);
                separator += "|";
            }
            fullWidth += NumofColumns;
            Console.WriteLine(separator);
            Console.WindowWidth = fullWidth + 1;
            //Console.WriteLine(new string('=', fullWidth));
        }

        public void SetMDDefaults()
        {
            //NumofColumns = 6;
            if (Columns is null) Columns = new Dictionary<int, IColumn>();
            else Columns.Clear();

            var col0 = new ParameterColumn() { HeaderName = "Drill Rate" };
            Columns.Add(0, col0);
            var col1 = new DepthColumn(hwl);
            Columns.Add(1, col1);
            var col2 = new LithologyColumn(hwl);
            Columns.Add(2, col2);
            var col3 = new ParameterColumn() { HeaderName = "Shows" };
            Columns.Add(3, col3);
            var col4 = new ParameterColumn() { HeaderName = "Gases" };
            Columns.Add(4, col4);
            var col5 = new ParameterColumn() { HeaderName = "Chromatograph" };
            Columns.Add(5, col5);
            var col6 = new DescriptionColumn(hwl) { HeaderName = "Descriptions" };
            Columns.Add(6, col6);
            foreach (var item in hwl.DrillingParameters)
            {
                if (item.GetType() == typeof(DrillRate)) col0.AddParameter(item);
                if (item.GetType() == typeof(WOB)) col0.AddParameter(item);
                if (item.GetType() == typeof(Shows)) col3.AddParameter(item);
                if (item.GetType() == typeof(TotalGas)) col4.AddParameter(item);
                if (item.GetType() == typeof(CuttingsGas)) col4.AddParameter(item);
                if (item.GetType() == typeof(C1)) col5.AddParameter(item);
                if (item.GetType() == typeof(C2)) col5.AddParameter(item);
                if (item.GetType() == typeof(C3)) col5.AddParameter(item);
                if (item.GetType() == typeof(C4)) col5.AddParameter(item);
                if (item.GetType() == typeof(C5)) col5.AddParameter(item);
            }
        }

        public void GetMudLogLayout()
        {
            if (Columns is null) Columns = new Dictionary<int, IColumn>();
            else Columns.Clear();
            Columns.Add(1, new DepthColumn(hwl));
            Columns.Add(2, new LithologyColumn(hwl));
            Columns.Add(6, new DescriptionColumn(hwl));
            var plotty = hwl.PlottedDrillingParameters.ToList();

            for (int i = 0; i < NumofColumns; i++)
            {
                if (i == 1 || i == 2 || i == 6) continue;
                var col = new ParameterColumn();
                foreach (var item in plotty)
                {
                    if (item.GetType() == typeof(SPM)
                        || item.GetType() == typeof(PumpPressure)
                        || item.GetType() == typeof(RPM)
                        || item.GetType() == typeof(Torque)) continue;
                    if (item.Column == i)
                    {
                        //if (Columns.ContainsKey(i)) col.Fields.Add(hwl.PlottedDrillingParameters[i]);
                        col.AddParameter(item);
                    }

                }

                Columns.Add(i, col);
            }

            Columns.OrderBy(i => i.Key);
        }
    }
}

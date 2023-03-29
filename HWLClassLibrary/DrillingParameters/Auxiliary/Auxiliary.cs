namespace HwlFileAnalyzer
{
    public abstract class Auxiliary : DrillingParameter
    {
        public Auxiliary(HwlData hwlData)
            : base(hwlData)
        {
        }

        private Dictionary<double, double> auxdata;

        //public override Dictionary<double, int> Data { get
        //    {
        //        var newdata = new Dictionary<double, int>();
        //        foreach (var item in auxdata)
        //        {
        //            int newvalue = Convert.ToInt32(item.Value);
        //            newdata.Add(item.Key, newvalue);
        //        }
        //        return newdata;
        //    } }

        protected override void SetPlotData()
        {
            if (auxdata is null) auxdata = new Dictionary<double, double>();
            else auxdata.Clear();
            if (data is null) data = new Dictionary<double, int>();
            else data.Clear();
            foreach (var item in hwl.DrillingParameterData)
            {
                if (string.IsNullOrEmpty(item[PlotLinePosition.Value])) continue;
                else
                {
                    int x;
                    if (int.TryParse(item[PlotLinePosition.Value], out x))
                    {
                        auxdata.Add(double.Parse(item[0]), double.Parse(item[PlotLinePosition.Value]));
                        data.Add(double.Parse(item[0]), x);
                    }
                    else
                    {
                        auxdata.Add(double.Parse(item[0]), double.Parse(item[PlotLinePosition.Value]));
                        data.Add(double.Parse(item[0]), Convert.ToInt32(double.Parse(item[PlotLinePosition.Value])));
                    }

                }

            }
        }
    }
}

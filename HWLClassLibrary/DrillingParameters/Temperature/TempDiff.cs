namespace HwlFileAnalyzer.Drilling_Parameters.Temperature
{
    public class TempDiff : DrillingParameter
    {
        public TempDiff(HwlData hwlData) : base(hwlData)
        {

        }

        //Fully filled out and accurate as of 07/21/2020
        public override int Column => 7;
        public override Dictionary<double, int> Data
        {
            get
            {
                var dict = new Dictionary<double, int>();
                DrillingParameter tempin = hwl.DrillingParameters.Find(i => i.GetType() == typeof(TemperatureIn));
                DrillingParameter tempout = hwl.DrillingParameters.Find(i => i.GetType() == typeof(TemperatureOut));
                foreach (var depth in hwl.Depths)
                {
                    dict.Add(depth, System.Math.Abs(tempin.Data[depth] - tempout.Data[depth]));
                }

                return dict;
            }
        }
        public override bool PlotEnabled => true;
        public override string UnitOfMeasurement => "Degrees F";
        protected override int? GeoColorPosition => 7;
        protected override int? GeoDisplayNamePosition => 25;
        protected override int? GeoLineStylesPosition => 9;
    }
}

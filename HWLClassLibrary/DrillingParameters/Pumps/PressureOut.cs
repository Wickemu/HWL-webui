namespace HwlFileAnalyzer.DrillingParameters.Pumps
{
    class PressureOut : DrillingParameter
    {
        public PressureOut(HwlData hwlData) : base(hwlData) { }

        //Filled out and accurate as of 07/21/2020
        public override string ShortName => "Press Out";
        protected override int? GeoPlotLinePosition => 25;
    }
}

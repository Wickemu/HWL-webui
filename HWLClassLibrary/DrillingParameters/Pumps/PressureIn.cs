namespace HwlFileAnalyzer.DrillingParameters.Pumps
{
    class PressureIn : DrillingParameter
    {
        public PressureIn(HwlData hwlData) : base(hwlData) { }

        //Filled out and accurate as of 07/21/2020
        public override string ShortName => "Press In";
        protected override int? GeoPlotLinePosition => 24;
    }
}

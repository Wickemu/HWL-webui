namespace HwlFileAnalyzer.Drilling_Parameters.Gases
{
    class H2S : DrillingParameter
    {
        public H2S(HwlData hwlData) : base(hwlData) { }

        //Filled out and accurate as of 7/21/2020
        public override string ShortName => "H2S";
        protected override int? GeoDisplayNamePosition => 9;
        protected override int? GeoLineStylesPosition => 6;
        protected override int? GeoPlotLinePosition => 15;
    }
}

namespace HwlFileAnalyzer.DrillingParameters.Gases
{
    class CO2 : DrillingParameter
    {
        public CO2(HwlData hwlData) : base(hwlData) { }

        //Filled out and accurate as of 07/21/2020
        public override string ShortName => "CO2";
        protected override int? GeoColorPosition => 2;
        protected override int? GeoDisplayNamePosition => 6;
        protected override int? GeoHeaderScalePosition => 2;
        protected override int? GeoLineStylesPosition => 3;
        protected override int? GeoPlotLinePosition => 14;
    }
}

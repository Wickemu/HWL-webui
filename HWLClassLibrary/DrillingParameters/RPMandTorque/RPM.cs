namespace HwlFileAnalyzer
{
    public class RPM : DrillingParameter
    {
        public RPM(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override int Column => 7;
        public override string UnitOfMeasurement => "RPM";
        protected override int? GeoColorPosition => 24;
        protected override int? GeoDisplayNamePosition => 37;
        protected override int? GeoHeaderScalePosition => 11;
        protected override int? GeoLineStylesPosition => 17;
        protected override int? GeoPlotLinePosition => 43;
        protected override int? OGColorPosition => 19;
        protected override int? OGDisplayNamePosition => 18;
        protected override int? OGHeaderScalePosition => 10;
        protected override int? OGLineStylesPosition => 18;
        protected override int? OGPlotLinePosition => 30;
        public override int? OGPlotOverscalePosition => 24;
        public override int? OGPlotScalePosition => 10;
    }
}
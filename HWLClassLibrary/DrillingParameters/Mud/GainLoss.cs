namespace HwlFileAnalyzer
{
    public class GainLoss : DrillingParameter
    {
        public GainLoss(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/21/2020
        public override int Column => 6;
        public override string ShortName => "G/L";
        public override string UnitOfMeasurement => "BBLs";
        protected override int? GeoColorPosition => 23;
        protected override int? GeoDisplayNamePosition => 34;
        protected override int? GeoHeaderScalePosition => 10;
        protected override int? GeoLineStylesPosition => 15;
        protected override int? GeoPlotLinePosition => 42;
        protected override int? OGColorPosition => 17;
        protected override int? OGDisplayNamePosition => 16;
        protected override int? OGHeaderScalePosition => 9;
        protected override int? OGLineStylesPosition => 16;
        protected override int? OGPlotLinePosition => 29;
        public override int? OGPlotOverscalePosition => 21;
        public override int? OGPlotScalePosition => 7;
    }
}
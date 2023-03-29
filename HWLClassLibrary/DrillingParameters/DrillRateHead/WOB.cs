namespace HwlFileAnalyzer
{
    public class WOB : DrillingParameter
    {
        public WOB(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 0;
        public override string ShortName => "WOB";
        public override string UnitOfMeasurement => "Klbs";
        protected override int? GeoColorPosition => 1;
        protected override int? GeoDisplayNamePosition => 4;
        protected override int? GeoHeaderScalePosition => 1;
        protected override int? GeoLineStylesPosition => 2;
        protected override int? GeoPlotLinePosition => 2;
        protected override int? OGColorPosition => 11;
        protected override int? OGDisplayNamePosition => 10;
        protected override int? OGHeaderScalePosition => 1;
        protected override int? OGLineStylesPosition => 2;
        protected override int? OGPlotLinePosition => 2;
        public override int? OGPlotOverscalePosition => 28;
        public override int? OGPlotScalePosition => 14;
    }
}

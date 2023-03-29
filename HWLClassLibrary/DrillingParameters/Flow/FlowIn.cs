namespace HwlFileAnalyzer
{
    public class FlowIn : DrillingParameter
    {
        public FlowIn(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 5;
        public override string ShortName => "Flow In";
        public override string UnitOfMeasurement => "ft^3/min";
        protected override int? GeoColorPosition => 21;
        protected override int? GeoDisplayNamePosition => 31;
        protected override int? GeoHeaderScalePosition => 8;
        protected override int? GeoLineStylesPosition => 12;
        protected override int? GeoPlotLinePosition => 40;
        protected override int? OGColorPosition => 15;
        protected override int? OGDisplayNamePosition => 14;
        protected override int? OGHeaderScalePosition => 7;
        protected override int? OGLineStylesPosition => 14;
        protected override int? OGPlotLinePosition => 27;
        public override int? OGPlotOverscalePosition => 19;
        public override int? OGPlotScalePosition => 5;
    }
}
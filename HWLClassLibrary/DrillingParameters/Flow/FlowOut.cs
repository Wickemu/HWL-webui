namespace HwlFileAnalyzer
{
    public class FlowOut : DrillingParameter
    {
        public FlowOut(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 5;
        public override string ShortName => "Flow Out";
        public override string UnitOfMeasurement => "%";
        protected override int? GeoColorPosition => 22;
        protected override int? GeoDisplayNamePosition => 32;
        protected override int? GeoHeaderScalePosition => 9;
        protected override int? GeoLineStylesPosition => 13;
        protected override int? GeoPlotLinePosition => 41;
        protected override int? OGColorPosition => 16;
        protected override int? OGDisplayNamePosition => 15;
        protected override int? OGHeaderScalePosition => 8;
        protected override int? OGLineStylesPosition => 15;
        protected override int? OGPlotLinePosition => 28;
        public override int? OGPlotOverscalePosition => 20;
        public override int? OGPlotScalePosition => 6;
    }
}

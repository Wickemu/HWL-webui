namespace HwlFileAnalyzer
{
    public class DrillRate : DrillingParameter
    {
        public DrillRate(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 0;
        public override string ShortName => "ROP";
        public override string UnitOfMeasurement => "Ft/Hr";
        protected override int? GeoColorPosition => 0;
        protected override int? GeoDisplayNamePosition => 2;
        protected override int? GeoHeaderScalePosition => 0;
        protected override int? GeoLineStylesPosition => 1;
        protected override int? GeoPlotLinePosition => 1;
        protected override int? OGColorPosition => 4;
        protected override int? OGDisplayNameOverscalePosition => 5;
        protected override int? OGDisplayNamePosition => 4;
        protected override int? OGHeaderScalePosition => 0;
        protected override int? OGLineStylesPosition => 1;
        protected override int? OGPlotLinePosition => 1;
        public override int? OGPlotOverscalePosition => 18;
        public override int? OGPlotScalePosition => 4;
    }
}

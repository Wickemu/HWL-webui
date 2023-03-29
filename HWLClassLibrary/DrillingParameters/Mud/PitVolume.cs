namespace HwlFileAnalyzer
{
    public class PitVolume : DrillingParameter
    {
        public PitVolume(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/21/2020
        //NOT including the DP alternatives of Geo Linestyles (16) and Geo Header (35)
        public override int Column => 6;
        public override string ShortName => "Pit Vol";
        public override string UnitOfMeasurement => "BBLs";
        protected override int? GeoColorPosition => 4;
        protected override int? GeoDisplayNamePosition => 8;
        protected override int? GeoHeaderScalePosition => 4;
        protected override int? GeoLineStylesPosition => 5;
        protected override int? GeoPlotLinePosition => 17;
        protected override int? OGColorPosition => 18;
        protected override int? OGDisplayNamePosition => 17;
        protected override int? OGHeaderScalePosition => 12;
        protected override int? OGLineStylesPosition => 17;
        protected override int? OGPlotLinePosition => 32;
        public override int? OGPlotOverscalePosition => 22;
        public override int? OGPlotScalePosition => 8;
    }
}
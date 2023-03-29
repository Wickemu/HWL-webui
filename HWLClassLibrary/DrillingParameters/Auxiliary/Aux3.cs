namespace HwlFileAnalyzer
{
    public class Aux3 : Auxiliary
    {
        public Aux3(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 7;
        public override string ShortName => "Aux3";
        protected override int? GeoColorPosition => 28;
        protected override int? GeoDisplayNamePosition => 41;
        protected override int? GeoHeaderOverscalePosition => 18;
        protected override int? GeoHeaderScalePosition => 17;
        protected override int? GeoLineStylesPosition => 20;
        protected override int? GeoPlotLinePosition => 47;
        protected override int? OGColorPosition => 23;
        protected override int? OGDisplayNamePosition => 22;
        protected override int? OGLineStylesPosition => 22;
        protected override int? OGPlotLinePosition => 36;
        public override int? OGPlotOverscalePosition => 42;
        public override int? OGPlotScalePosition => 41;
    }
}

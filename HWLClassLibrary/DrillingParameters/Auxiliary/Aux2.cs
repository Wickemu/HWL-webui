namespace HwlFileAnalyzer
{
    public class Aux2 : Auxiliary
    {
        public Aux2(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 4;
        public override string ShortName => "Aux 2";
        protected override int? GeoColorPosition => 27;
        protected override int? GeoDisplayNamePosition => 40;
        protected override int? GeoHeaderOverscalePosition => 16;
        protected override int? GeoHeaderScalePosition => 15;
        protected override int? GeoLineStylesPosition => 19;
        protected override int? GeoPlotLinePosition => 46;
        protected override int? OGColorPosition => 22;
        protected override int? OGDisplayNamePosition => 21;
        protected override int? OGLineStylesPosition => 21;
        protected override int? OGPlotLinePosition => 35;
        public override int? OGPlotOverscalePosition => 40;
        public override int? OGPlotScalePosition => 39;
    }
}
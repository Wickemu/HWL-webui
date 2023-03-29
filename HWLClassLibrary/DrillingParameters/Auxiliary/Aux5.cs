namespace HwlFileAnalyzer
{
    public class Aux5 : Auxiliary
    {
        public Aux5(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 4;
        public override string ShortName => "Aux5";
        protected override int? GeoColorPosition => 30;
        protected override int? GeoDisplayNamePosition => 43;
        protected override int? GeoHeaderOverscalePosition => 22;
        protected override int? GeoHeaderScalePosition => 21;
        protected override int? GeoLineStylesPosition => 22;
        protected override int? GeoPlotLinePosition => 49;
        protected override int? OGColorPosition => 25;
        protected override int? OGDisplayNamePosition => 25;
        protected override int? OGLineStylesPosition => 24;
        protected override int? OGPlotLinePosition => 38;
        public override int? OGPlotOverscalePosition => 46;
        public override int? OGPlotScalePosition => 45;
    }
}

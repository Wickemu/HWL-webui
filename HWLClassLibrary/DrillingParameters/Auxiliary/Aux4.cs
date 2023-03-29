namespace HwlFileAnalyzer
{
    public class Aux4 : Auxiliary
    {
        public Aux4(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 0;
        public override string ShortName => "Aux4";
        protected override int? GeoColorPosition => 29;
        protected override int? GeoDisplayNamePosition => 42;
        protected override int? GeoHeaderOverscalePosition => 20;
        protected override int? GeoHeaderScalePosition => 19;
        protected override int? GeoLineStylesPosition => 21;
        protected override int? GeoPlotLinePosition => 48;
        protected override int? OGColorPosition => 24;
        protected override int? OGDisplayNamePosition => 24;
        protected override int? OGLineStylesPosition => 23;
        protected override int? OGPlotLinePosition => 37;
        public override int? OGPlotOverscalePosition => 44;
        public override int? OGPlotScalePosition => 43;
    }
}

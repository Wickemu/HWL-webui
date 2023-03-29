namespace HwlFileAnalyzer
{
    public class Aux1 : Auxiliary
    {
        public Aux1(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 0;
        public override string ShortName => "Aux1";
        protected override int? GeoColorPosition => 26;
        protected override int? GeoDisplayNamePosition => 39;
        protected override int? GeoHeaderOverscalePosition => 14;
        protected override int? GeoHeaderScalePosition => 13;
        protected override int? GeoLineStylesPosition => 18;
        protected override int? GeoPlotLinePosition => 45;
        protected override int? OGColorPosition => 21;
        protected override int? OGDisplayNamePosition => 20;
        protected override int? OGLineStylesPosition => 20;
        protected override int? OGPlotLinePosition => 34;
        public override int? OGPlotOverscalePosition => 38;
        public override int? OGPlotScalePosition => 37;
    }
}

namespace HwlFileAnalyzer
{
    public class Aux6 : Auxiliary
    {
        public Aux6(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 4;
        public override string ShortName => "Aux6";
        protected override int? GeoColorPosition => 31;
        protected override int? GeoDisplayNamePosition => 44;
        protected override int? GeoHeaderOverscalePosition => 24;
        protected override int? GeoHeaderScalePosition => 23;
        protected override int? GeoLineStylesPosition => 23;
        protected override int? GeoPlotLinePosition => 50;
        protected override int? OGColorPosition => 26;
        protected override int? OGDisplayNamePosition => 26;
        protected override int? OGLineStylesPosition => 25;
        protected override int? OGPlotLinePosition => 39;
        public override int? OGPlotOverscalePosition => 48;
        public override int? OGPlotScalePosition => 47;
    }
}

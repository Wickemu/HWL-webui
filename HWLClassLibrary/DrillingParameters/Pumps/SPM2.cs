namespace HwlFileAnalyzer
{
    public class SPM2 : SPM
    {
        public SPM2(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/21/2020
        public override string ShortName => "SPM 2";
        protected override int? GeoColorPosition => 20;
        protected override int? GeoDisplayNamePosition => 29;
        protected override int? GeoLineStylesPosition => 12;
        protected override int? GeoPlotLinePosition => 39;
        protected override int? OGColorPosition => 14;
        protected override int? OGDisplayNamePosition => 13;
        protected override int? OGLineStylesPosition => 12;
        protected override int? OGPlotLinePosition => 26;
        protected override int? GeoHeaderOverscalePosition => null;
        protected override int? GeoHeaderScalePosition => null;
    }
}
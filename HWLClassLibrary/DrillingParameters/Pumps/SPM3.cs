namespace HwlFileAnalyzer
{
    public class SPM3 : SPM
    {
        public SPM3(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override bool PlotEnabled { get; set; }
        public override string ShortName => "SPM 3";
        protected override int? GeoDisplayNamePosition => 45;
        protected override int? OGColorPosition => 24;
        protected override int? OGDisplayNamePosition => 23;
        protected override int? OGLineStylesPosition => 13;
        protected override int? OGPlotLinePosition => 33;

        protected override int? GeoColorPosition => 32;
        protected override int? GeoHeaderOverscalePosition => null;
        protected override int? GeoHeaderScalePosition => null;
        protected override int? GeoLineStylesPosition => 24;
        protected override int? GeoPlotLinePosition => 51;

    }
}
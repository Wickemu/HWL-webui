namespace HwlFileAnalyzer
{
    public class SPM4 : SPM
    {
        public SPM4(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override string ShortName => "SPM 4";
        protected override int? GeoDisplayNamePosition => 46;

        protected override int? GeoColorPosition => 33;
        protected override int? GeoHeaderOverscalePosition => 26;
        protected override int? GeoHeaderScalePosition => 25;
        protected override int? GeoLineStylesPosition => 25;
        protected override int? GeoPlotLinePosition => 52;

    }
}

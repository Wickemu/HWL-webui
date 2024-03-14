namespace HwlFileAnalyzer
{
    public class SPM1 : SPM
    {
        public SPM1(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/21/2020
        public override string ShortName => "SPM 1";
        protected override int? GeoColorPosition => 19;
        protected override int? GeoDisplayNamePosition => 28;
        protected override int? GeoLineStylesPosition => 11;
        protected override int? GeoPlotLinePosition => 38;
        protected override int? OGColorPosition => 13;
        protected override int? OGDisplayNamePosition => 12;
        protected override int? OGLineStylesPosition => 11;
        protected override int? OGPlotLinePosition => 25;
    }
}
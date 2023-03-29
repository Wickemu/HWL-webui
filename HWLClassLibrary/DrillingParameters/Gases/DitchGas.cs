namespace HwlFileAnalyzer

{
    public class DitchGas : DrillingParameter
    {
        public DitchGas(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/19/2020
        public override string ShortName => "Ditch Gas";
        protected override int? GeoColorPosition => 3;
        protected override int? GeoDisplayNamePosition => 7;
        protected override int? GeoHeaderScalePosition => 3;
        protected override int? GeoLineStylesPosition => 3;
        protected override int? GeoPlotLinePosition => 16;
        protected override int? OGColorOverscalePosition => 3;
        protected override int? OGColorPosition => 2;
        protected override int? OGDisplayNamePosition => 2;
        protected override int? OGHeaderScalePosition => 3;
        protected override int? OGLineStylesPosition => 3;
        protected override int? OGPlotLinePosition => 3;
        public override int? OGPlotOverscalePosition => 17;
        public override int? OGPlotScalePosition => 3;
        public override int Column => 4;
    }
}

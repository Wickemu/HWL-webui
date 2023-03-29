namespace HwlFileAnalyzer
{
    public class C4 : Chromatography
    {
        public C4(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 5;
        public override string ShortName => "C4";
        public override string UnitOfMeasurement => "PPM";
        protected override int? GeoPlotLinePosition => 20;
        protected override int? OGColorPosition => 0;
        protected override int? OGDisplayNamePosition => 0;
        protected override int? OGLineStylesPosition => 8;
        protected override int? OGPlotLinePosition => 7;
    }
}

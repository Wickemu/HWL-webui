namespace HwlFileAnalyzer
{
    public class C2 : Chromatography
    {
        public C2(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 5;
        public override string ShortName => "C2";
        public override string UnitOfMeasurement => "PPM";
        protected override int? GeoPlotLinePosition => 20;
        protected override int? OGColorPosition => 6;
        protected override int? OGDisplayNamePosition => 6;
        protected override int? OGLineStylesPosition => 6;
        protected override int? OGPlotLinePosition => 5;
    }
}

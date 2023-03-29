namespace HwlFileAnalyzer
{
    public class C1 : Chromatography
    {
        public C1(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 5;
        public override string ShortName => "C1";
        public override string UnitOfMeasurement => "PPM";
        protected override int? GeoPlotLinePosition => 19;
        protected override int? OGColorPosition => 7;
        protected override int? OGDisplayNamePosition => 7;
        protected override int? OGLineStylesPosition => 5;
        protected override int? OGPlotLinePosition => 4;
    }
}

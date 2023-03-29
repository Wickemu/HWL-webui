namespace HwlFileAnalyzer
{
    public class C5 : Chromatography
    {
        public C5(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 5;
        public override string ShortName => "C5";
        public override string UnitOfMeasurement => "PPM";
        protected override int? GeoPlotLinePosition => 23;
        protected override int? OGColorPosition => 8;
        protected override int? OGDisplayNamePosition => 8;
        protected override int? OGLineStylesPosition => 9;
        protected override int? OGPlotLinePosition => 9;
    }
}

namespace HwlFileAnalyzer
{
    public class C3 : Chromatography
    {
        public C3(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        public override int Column => 5;
        public override string ShortName => "C3";
        public override string UnitOfMeasurement => "PPM";
        protected override int? GeoPlotLinePosition => 21;
        protected override int? OGColorPosition => 9;
        protected override int? OGDisplayNamePosition => 9;
        protected override int? OGLineStylesPosition => 7;
        protected override int? OGPlotLinePosition => 6;
    }
}

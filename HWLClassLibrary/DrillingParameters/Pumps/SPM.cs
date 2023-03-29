namespace HwlFileAnalyzer
{
    public class SPM : DrillingParameter
    {
        public SPM(HwlData hwlData) : base(hwlData) { }

        //public override int Column => 4;
        public override string UnitOfMeasurement => "SPM";
        protected override int? GeoHeaderScalePosition => 7;
        public override int? OGPlotOverscalePosition => 26;
        public override int? OGPlotScalePosition => 12;
    }
}

namespace HwlFileAnalyzer.DrillingParameters.Mud
{
    public class MudLoss : DrillingParameter
    {
        public MudLoss(HwlData hwlData) : base(hwlData) { }
        public override string ShortName => "Mudloss";
        protected override int? GeoColorPosition => 9;
        protected override int? GeoDisplayNamePosition => 20;
        protected override int? GeoHeaderScalePosition => 5;
        protected override int? GeoPlotLinePosition => 18;

    }
}

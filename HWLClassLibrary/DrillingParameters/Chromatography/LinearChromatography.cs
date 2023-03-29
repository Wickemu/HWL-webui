namespace HwlFileAnalyzer
{
    class LinearChromatography : DrillingParameter
    {
        public LinearChromatography(HwlData hwlData) : base(hwlData) { }

        protected override int? OGHeaderOverscalePosition => 20;
        protected override int? OGHeaderScalePosition => 19;
        public override int? OGPlotScalePosition => 35;
        public override int? OGPlotOverscalePosition => 36;
        public override bool PlotEnabled => false;
    }
}

namespace HwlFileAnalyzer
{
    class Character : DrillingParameter
    {
        public Character(HwlData hwlData) : base(hwlData) { }

        protected override int? OGHeaderOverscalePosition => 18;
        protected override int? OGHeaderScalePosition => 17;
        //Unusual OGPlotscale and Overscale = input * 10
        public override int? OGPlotOverscalePosition => 34;
        public override int? OGPlotScalePosition => 33;
        public override bool PlotEnabled => false;
    }
}

namespace HwlFileAnalyzer
{
    public class Wetness : DrillingParameter
    {
        public Wetness(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        protected override int? OGHeaderScalePosition => 13;
        protected override int? OGHeaderOverscalePosition => 14;
        public override bool PlotEnabled => false;
        //Unusual OGPlotScale and Overscale: Over fixed at 200
        public override int? OGPlotScalePosition => 29;
        public override int? OGPlotOverscalePosition => 30;
    }
}

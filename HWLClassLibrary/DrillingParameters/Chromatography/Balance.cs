namespace HwlFileAnalyzer
{
    public class Balance : DrillingParameter
    {
        public Balance(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        protected override int? OGHeaderScalePosition => 15;
        protected override int? OGHeaderOverscalePosition => 16;
        public override bool PlotEnabled => false;
        //Unusual OGPlotScale and Overscale: Over fixed at 200
        public override int? OGPlotScalePosition => 31;
        public override int? OGPlotOverscalePosition => 32;

    }
}

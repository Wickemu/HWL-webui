namespace HwlFileAnalyzer
{
    public abstract class Chromatography : DrillingParameter
    {
        public Chromatography(HwlData hwlData) : base(hwlData) { }

        public override int? OGPlotScalePosition => 1;
        public override int? OGPlotOverscalePosition => 15;
        public override bool PlotEnabled => false;
    }
}

namespace HwlFileAnalyzer
{
    public class CuttingsGas : DrillingParameter
    {
        public CuttingsGas(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled and accurate as of 7/20/2020
        private Dictionary<double, int> cuttingsData;
        public override int Column => 4;
        public override string ShortName => "Cuttings";
        public override string UnitOfMeasurement => "Units";
        protected override int? OGColorPosition => 1;
        protected override int? OGDisplayNamePosition => 1;
        protected override int? OGHeaderScalePosition => 4;
        protected override int? OGLineStylesPosition => 4;
        protected override int? OGPlotLinePosition => 12;
        public override int? OGPlotOverscalePosition => 16;
        public override int? OGPlotScalePosition => 2;
        public override bool PlotEnabled => false;

    }
}

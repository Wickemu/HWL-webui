namespace HwlFileAnalyzer
{
    public class TotalGas : DrillingParameter
    {
        public TotalGas(HwlData hwlData)
            : base(hwlData)
        {
        }

        //NOT READY
        public override int Column => 4;
        public override string ShortName => "TotalGas";
        public override string UnitOfMeasurement => "Units";
        protected override int? OGColorPosition => 20;
        protected override int? OGLineStylesPosition => 18;
        protected override int? OGPlotLinePosition => 3;
        public override int? OGPlotOverscalePosition => 16;
        public override int? OGPlotScalePosition => 2;
    }
}

namespace HwlFileAnalyzer
{
    public class Torque : DrillingParameter
    {
        public Torque(HwlData hwlData)
            : base(hwlData)
        {
        }

        //Fully filled out and accurate as of 07/21/2020
        public override int Column => 7;
        public override string ShortName => "Torque";
        public override string UnitOfMeasurement => "lbft";
        protected override int? GeoColorPosition => 25;
        protected override int? GeoDisplayNamePosition => 38;
        protected override int? GeoHeaderScalePosition => 12;
        protected override int? GeoLineStylesPosition => 18;
        protected override int? GeoPlotLinePosition => 44;
        protected override int? OGColorPosition => 20;
        protected override int? OGDisplayNamePosition => 19;
        protected override int? OGHeaderScalePosition => 11;
        protected override int? OGLineStylesPosition => 19;
        protected override int? OGPlotLinePosition => 31;
        public override int? OGPlotOverscalePosition => 27;
        public override int? OGPlotScalePosition => 13;
    }
}
namespace HwlFileAnalyzer
{
    public class PumpPressure : DrillingParameter
    {
        public PumpPressure(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/21/2020
        public override int Column => 4;
        public override string ShortName => "Pump Press";
        public override string UnitOfMeasurement => "PSI";
        protected override int? GeoColorPosition => 18;
        protected override int? GeoDisplayNamePosition => 27;
        protected override int? GeoHeaderScalePosition => 6;
        protected override int? GeoLineStylesPosition => 10;
        protected override int? GeoPlotLinePosition => 37;
        protected override int? OGColorPosition => 12;
        protected override int? OGDisplayNamePosition => 11;
        protected override int? OGHeaderScalePosition => 5;
        protected override int? OGLineStylesPosition => 10;
        protected override int? OGPlotLinePosition => 24;
        public override int? OGPlotOverscalePosition => 23;
        public override int? OGPlotScalePosition => 9;
    }
}
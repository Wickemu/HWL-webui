namespace HwlFileAnalyzer
{
    public class TemperatureOut : DrillingParameter
    {
        public TemperatureOut(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override int Column => 7;
        public override string ShortName => "TempOut";
        public override string UnitOfMeasurement => "Degrees F";
        protected override int? GeoColorPosition => 6;
        protected override int? GeoDisplayNamePosition => 24;
        protected override int? GeoLineStylesPosition => 8;
        protected override int? GeoPlotLinePosition => 13;
        public override bool PlotEnabled => true;
    }
}

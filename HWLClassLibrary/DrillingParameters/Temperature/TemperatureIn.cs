namespace HwlFileAnalyzer
{
    public class TemperatureIn : DrillingParameter
    {
        public TemperatureIn(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override int Column => 7;
        public override string ShortName => "TempIn";
        public override string UnitOfMeasurement => "Degrees F";
        protected override int? GeoHeaderScalePosition => 25;
        protected override int? GeoHeaderOverscalePosition => 26;
        protected override int? GeoColorPosition => 5;
        protected override int? GeoDisplayNamePosition => 23;
        protected override int? GeoLineStylesPosition => 7;
        protected override int? GeoPlotLinePosition => 12;
        public override bool PlotEnabled => true;
    }
}
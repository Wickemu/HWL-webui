namespace HwlFileAnalyzer
{
    public class Pyrite : DrillingParameter
    {
        public Pyrite(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 12;
        protected override int? GeoDisplayNamePosition => 13;
        protected override int? GeoPlotLinePosition => 6;
    }
}

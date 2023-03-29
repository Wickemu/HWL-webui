namespace HwlFileAnalyzer
{
    public class Fractures : DrillingParameter
    {
        public Fractures(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 8;
        protected override int? GeoDisplayNamePosition => 19;
        protected override int? GeoPlotLinePosition => 3;
    }
}

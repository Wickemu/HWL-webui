namespace HwlFileAnalyzer
{
    public class Sericite : DrillingParameter
    {
        public Sericite(HwlData hwlData) : base(hwlData) { }

        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 17;
        protected override int? GeoDisplayNamePosition => 18;
        protected override int? GeoPlotLinePosition => 11;
    }
}

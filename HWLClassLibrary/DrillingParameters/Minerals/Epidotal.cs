namespace HwlFileAnalyzer
{
    public class Epidotal : DrillingParameter
    {
        public Epidotal(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 14;
        protected override int? GeoDisplayNamePosition => 15;
        protected override int? GeoPlotLinePosition => 8;
    }
}

namespace HwlFileAnalyzer
{
    public class Anhydrite : DrillingParameter
    {
        public Anhydrite(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 16;
        protected override int? GeoDisplayNamePosition => 17;
        protected override int? GeoPlotLinePosition => 10;
    }
}

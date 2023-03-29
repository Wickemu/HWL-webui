namespace HwlFileAnalyzer
{
    public class Quartzite : DrillingParameter
    {
        public Quartzite(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 10;
        protected override int? GeoDisplayNamePosition => 11;
        protected override int? GeoPlotLinePosition => 4;
    }
}

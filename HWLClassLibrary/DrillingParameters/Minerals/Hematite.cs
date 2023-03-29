namespace HwlFileAnalyzer
{
    public class Hematite : DrillingParameter
    {
        public Hematite(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 13;
        protected override int? GeoDisplayNamePosition => 14;
        protected override int? GeoPlotLinePosition => 7;
    }
}

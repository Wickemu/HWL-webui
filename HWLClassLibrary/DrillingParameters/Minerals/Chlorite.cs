namespace HwlFileAnalyzer
{
    public class Chlorite : DrillingParameter
    {
        public Chlorite(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 15;
        protected override int? GeoDisplayNamePosition => 16;
        protected override int? GeoPlotLinePosition => 9;
    }
}

namespace HwlFileAnalyzer
{
    public class Calcite : DrillingParameter
    {
        public Calcite(HwlData hwlData)
            : base(hwlData)
        {
        }
        //Fully filled and accurate as of 7/19/2020
        public override int Column => 3;
        protected override int? GeoColorPosition => 11;
        protected override int? GeoDisplayNamePosition => 12;
        protected override int? GeoPlotLinePosition => 5;
    }
}

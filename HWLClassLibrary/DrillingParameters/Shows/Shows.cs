namespace HwlFileAnalyzer
{
    public class Shows : DrillingParameter
    {
        public Shows(HwlData hwlData) : base(hwlData) { }

        //Fully filled out and accurate as of 07/21/2020
        public override string ShortName => "Shows";
        public override int Column => 3;
        protected override int? OGColorPosition => 10;
        protected override int? OGHeaderScalePosition => 2;
        protected override int? OGPlotLinePosition => 23;
        public override int? OGPlotOverscalePosition => 25;
        public override int? OGPlotScalePosition => 11;
    }
}

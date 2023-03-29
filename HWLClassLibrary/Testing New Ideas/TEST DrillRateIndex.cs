namespace HwlFileAnalyzer.Testing_New_Ideas
{
    public static class DrillRateFile
    {
        static int GeoColorPosition => 0;
        static int GeoHeaderLegendPosition => 3;
        static int GeoHeaderPosition => 2;
        static int GeoHeadScalesPosition => 0;
        static int GeoLineStylesPosition => 1;
        static int GeoPlotPosition => 1;
        static int OGColorsPosition => 4;
        static int OGColorsOverscale => 5;
        static int OGHeaderLegend2 => 4;
        static int OGHeadLegend2Overscale => 5;
        static int OGHeadScalesPosition => 0;
        static int OGLinestylesPosition => 1;
        static int OGPlotPosition => 1;
        static int OGPlotOverscalePosition => 18;
        static int OGPlotScalePosition => 4;
    }

    public class Test
    {
        HwlData Hwl;
        Importer imp;
        TableOfContents TOC;

        public Test(HwlData hwl, Importer importer)
        {
            Hwl = hwl;
            imp = importer;
        }



        public List<int> ImportPlotItem(int position)
        {
            var dpData = new List<int>();
            foreach (var i in TOC.Plot)
            {
                var splitLine = HwlParser.ParsePlotLine(imp.RawText[i]);
                dpData.Add(int.Parse(splitLine[position]));
            }

            return dpData;
        }

    }
}

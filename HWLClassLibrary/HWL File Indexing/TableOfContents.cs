namespace HwlFileAnalyzer.Testing_New_Ideas
{
    public class TableOfContents
    {


        private readonly List<string> File;
        public Dictionary<int, int> expectedHeaderItems;

        private string FileType { get; }
        public TableOfContents(List<string> rawText)
        {
            File = rawText ?? throw new ArgumentNullException(nameof(File));
            FileType = HwlParser.ParseHeaderLine(rawText[0])[1];
            InitializeExpectedHeaderItems();
        }

        public void InitializeExpectedHeaderItems()
        {
            expectedHeaderItems = new Dictionary<int, int>
    {
        { ProgramInfo, FileType == "OG" ? 20 : 18 },
        { WellInfo, 22 },
        { Colors, FileType == "OG" ? 28 : 34 },
        // Try to figure out how you had done HeaderLegend stuff later because OG is split across two lines
        //{ HeaderLegend, FileType == "OG" ? 12 : 34 },
        { LineStyles, FileType == "OG" ? 26 : 27 },
        { Lithology, 63 }
    };
        }


        public int ProgramInfo => 0;
        public int WellInfo => 1;
        public int Colors => 2;

        private int GeoDisplay => 3;
        private int OGDisplay1 => 3;
        private int OGDisplay2 => 4;
        public int HeaderLegend => FileType == "OG" ? OGDisplay2 : GeoDisplay;

        private int GeoLineStyles => 4;
        private int OGLineStyles => 5;
        public int LineStyles => FileType == "OG" ? OGLineStyles : GeoLineStyles;

        private int GeoLithology => 5;
        private int OGLithology => 6;
        public int Lithology => FileType == "OG" ? OGLithology : GeoLithology;

        private int[] GeoHeadScales => new int[] { 7 };
        private int[] OGHeadScales => Enumerable.Range(8, 21).ToArray();
        public int[] HeadScales => FileType == "OG" ? OGHeadScales : GeoHeadScales;

        private int OGHeadScalesHeader => 7;

        public List<int> Abbreviations
        {
            get
            {
                if (AbbreviationsHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = AbbreviationsHeader + 1; i < File.Count; i++)
                    {
                        if (File[i].Contains("|")) lineList.Add(i);
                        else break;
                    }

                    return lineList;
                }
            }
        }

        public List<int> Annotations
        {
            get
            {
                if (AnnotationsHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = AnnotationsHeader + 1; i < File.Count; i++)
                    {
                        if (File[i].Contains("|")) lineList.Add(i);
                        else break;
                    }

                    return lineList;
                }
            }
        }

        public List<int> CasingPoints
        {
            get
            {
                if (CasingPointsHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = CasingPointsHeader + 1; i < File.Count; i++)
                    {
                        if (File[i].Contains("|")) lineList.Add(i);
                        else break;
                    }

                    return lineList;
                }
            }
        }

        public List<int> LithSymbols
        {
            get
            {
                if (LithSymbolsHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    if (FileType == "OG")
                    {
                        for (int i = LithSymbolsHeader + 1; i < File.Count; i++)
                        {
                            if (File[i].StartsWith(" ")) lineList.Add(i);
                            else break;
                        }
                    }
                    else
                    {
                        lineList.Add(LithSymbolsHeader + 1);
                    }

                    return lineList;
                }
            }
        }

        public List<int> NewBits
        {
            get
            {
                if (NewBitsHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = NewBitsHeader + 1; i < File.Count; i++)
                    {
                        if (File[i].Contains("|")) lineList.Add(i);
                        else break;
                    }

                    return lineList;
                }
            }
        }

        public List<int> OGPlotScales
        {
            get
            {
                if (PlotHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = PlotHeader + 1; i < File.Count; i++)
                    {
                        if (File[i].StartsWith("$")) lineList.Add(i);
                        else continue;
                    }

                    return lineList;
                }
            }
        }

        public List<int> Plot
        {
            get
            {
                if (PlotHeader == -1)
                {
                    return null;
                }
                else
                {
                    List<int> lineList = new List<int>();
                    for (int i = PlotHeader + 1; i < File.Count; i++)
                    {
                        if (i < File.Count - 1 && File[i + 1].StartsWith("$")) continue;
                        if (File[i].StartsWith("$")) continue;
                        else lineList.Add(i);
                    }

                    return lineList;
                }
            }
        }

        public int? TVD
        {
            get
            {
                if (TVDHeader == -1)
                {
                    return null;
                }
                else
                {
                    return TVDHeader + 1;
                }
            }
        }

        public int[] PlotLithology
        {
            get
            {
                if (FileType == "OG") return new int[] { 0, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22 };
                else return new int[] { 0, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35 };
            }
        }

        private int AbbreviationsHeader => File.IndexOf("Abbreviations");
        private int AnnotationsHeader => File.IndexOf("Annotations");
        private int CasingPointsHeader => File.IndexOf("CasingPoints");
        private int LithSymbolsHeader => File.IndexOf("LithSymbols");
        private int NewBitsHeader => File.IndexOf("NewBits");
        private int PlotHeader => File.IndexOf("Data");
        private int TVDHeader => File.IndexOf("TVD");

    }
}
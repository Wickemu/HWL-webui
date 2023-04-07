using HwlFileAnalyzer.Drilling_Parameters.Temperature;
using HwlFileAnalyzer.Testing_New_Ideas;

namespace HwlFileAnalyzer;

public class Importer
{
    public readonly TableOfContents TOC;

    private readonly ProgramInfo Program;

    public Importer(string filePath)
    {
        RawText = GetFileText(filePath);
        TOC = new TableOfContents(RawText);
        Program = ImportProgramInfo();
    }

    public string FileType => Program.WellType;

    //public List<string[]> PlotScales { get; set; }
    public List<string> RawText { get; set; }
    private HwlData Hwl { get; set; }

    public string UnparseOGPlotScales(List<string> items)
    {
        var result = "$" + string.Join("|", items);
        return result;
    }

    public static List<string> GetFileText(string filePath)
    {
        return File.ReadAllLines(filePath).ToList();
    }

    public bool ValidateItemsCount(List<string> splitLine, int tocIndex)
    {
        if (TOC.expectedHeaderItems.ContainsKey(tocIndex))
        {
            var expectedCount = TOC.expectedHeaderItems[tocIndex];
            var foundCount = splitLine.Count;

            if (foundCount != expectedCount)
                throw new Exception(
                    $"Invalid number of items on HWL file line {tocIndex + 1}. Expected: {expectedCount}, Found: {foundCount}");

            return true;
        }

        throw new Exception($"No expected item count is defined for TOC index {tocIndex}");
    }


    public Dictionary<string, string> ImportAbbreviations()
    {
        var abbreviations = new Dictionary<string, string>();
        foreach (var i in TOC.Abbreviations)
        {
            var line = HwlParser.ParsePlotLine(RawText[i]);
            if (line[0] == "") continue;
            abbreviations.Add(line[0], line[1]);
        }

        return abbreviations;
    }

    public List<AnnotationLine> ImportAnnotations()
    {
        var annotations = new List<AnnotationLine>();
        if (TOC.Annotations != null)
        {
            foreach (var i in TOC.Annotations)
            {
                var line = HwlParser.ParsePlotLine(RawText[i]);
                annotations.Add(new AnnotationLine(line));
            }

            var sortedAnnotations = annotations.OrderBy(o => o.Depth).ToList();
            return sortedAnnotations;
        }

        return annotations;
    }

    public List<CasingPointLine>? ImportCasingPoints()
    {
        if (TOC.CasingPoints == null) return null;
        var casingPoints = new List<CasingPointLine>();
        foreach (var i in TOC.CasingPoints)
        {
            var line = HwlParser.ParsePlotLine(RawText[i]);
            casingPoints.Add(new CasingPointLine(line));
        }

        var sortedCasingPoints = casingPoints.OrderBy(o => o.Depth).ToList();
        return sortedCasingPoints;
    }

    public List<ChromatographyLine> ImportChromatography()
    {
        var chromatography = new List<ChromatographyLine>();
        foreach (var i in TOC.Plot)
        {
            var splitLine = HwlParser.ParsePlotLine(RawText[i]);
            chromatography.Add(new ChromatographyLine(splitLine));
        }

        return chromatography;
    }

    public List<int> ImportColors()
    {
        var colors = new List<int>();
        var splitLine = HwlParser.ParseHeaderLine(RawText[TOC.Colors]);
        // Call the ValidateItemsCount method
        if (!ValidateItemsCount(splitLine, TOC.Colors))
            throw new Exception("Invalid number of items in the Colors line.");
        foreach (var item in splitLine) colors.Add(int.Parse(item));

        return colors;
    }

    public List<double> ImportDepths()
    {
        var depths = new List<double>();
        foreach (var line in TOC.Plot)
        {
            var depth = Convert.ToDouble(HwlParser.ParsePlotLine(RawText[line])[0]);
            depths.Add(depth);
        }

        return depths;
    }

    public List<DescriptionLine> ImportDescriptions()
    {
        var descriptions = new List<DescriptionLine>();
        foreach (var i in TOC.Plot)
        {
            var line = RawText[i];
            var splitLine = HwlParser.ParsePlotLine(line); // line.Split("|");
            int descSlot;
            if (FileType == "OG") descSlot = 11;
            else descSlot = 36;
            if (splitLine[descSlot].Count() > 23)
            {
                var lineToSend = new List<string>();
                lineToSend.Add(splitLine[0]);
                lineToSend.Add(splitLine[descSlot]);
                descriptions.Add(new DescriptionLine(lineToSend));
            }
        }

        return descriptions;
    }

    public List<string> ImportDisplaynames()
    {
        var listy = HwlParser.ParseHeaderLine(RawText[TOC.HeaderLegend]);
        //listy.Add(HwlParser.ParseHeaderLine(RawText[TableOfContents["HeaderLegend"]]).ToList());
        //listy.Add(HwlParser.ParseHeaderLine(RawText[TableOfContents["HeaderLegend"] + 1]).ToList());
        return listy;
    }

    //! For now, you need to add each parameter to this import method so don't forget to do so if you add or remove any!!
    public List<DrillingParameter> ImportDrillingParameters()
    {
        var drillingParameters = new List<DrillingParameter>();
        drillingParameters.Add(new DrillRate(Hwl));
        drillingParameters.Add(new WOB(Hwl));
        drillingParameters.Add(new DitchGas(Hwl));
        drillingParameters.Add(new C1(Hwl));
        drillingParameters.Add(new C2(Hwl));
        drillingParameters.Add(new C3(Hwl));
        drillingParameters.Add(new C4(Hwl));
        drillingParameters.Add(new C5(Hwl));
        drillingParameters.Add(new PumpPressure(Hwl));
        drillingParameters.Add(new Torque(Hwl));
        drillingParameters.Add(new PitVolume(Hwl));
        drillingParameters.Add(new GainLoss(Hwl));
        drillingParameters.Add(new FlowIn(Hwl));
        drillingParameters.Add(new FlowOut(Hwl));
        drillingParameters.Add(new SPM1(Hwl));
        drillingParameters.Add(new SPM2(Hwl));
        drillingParameters.Add(new SPM3(Hwl));
        drillingParameters.Add(new Aux1(Hwl));
        drillingParameters.Add(new Aux2(Hwl));
        drillingParameters.Add(new Aux3(Hwl));
        drillingParameters.Add(new Aux4(Hwl));
        drillingParameters.Add(new Aux5(Hwl));
        drillingParameters.Add(new Aux6(Hwl));

        if (FileType == "OG")
        {
            drillingParameters.Add(new Shows(Hwl));
            drillingParameters.Add(new Balance(Hwl));
            drillingParameters.Add(new Wetness(Hwl));
            drillingParameters.Add(new CuttingsGas(Hwl));
            drillingParameters.Add(new Character(Hwl));
            drillingParameters.Add(new LinearChromatography(Hwl));
        }

        else
        {
            drillingParameters.Add(new Quartzite(Hwl));
            drillingParameters.Add(new Calcite(Hwl));
            drillingParameters.Add(new Chlorite(Hwl));
            drillingParameters.Add(new Epidotal(Hwl));
            drillingParameters.Add(new Hematite(Hwl));
            drillingParameters.Add(new Pyrite(Hwl));
            drillingParameters.Add(new Anhydrite(Hwl));
            drillingParameters.Add(new Sericite(Hwl));
            drillingParameters.Add(new Fractures(Hwl));
            drillingParameters.Add(new TemperatureIn(Hwl));
            drillingParameters.Add(new TemperatureOut(Hwl));
            drillingParameters.Add(new TempDiff(Hwl));
        }

        return drillingParameters;
    }

    public List<int> ImportHeaderScales()
    {
        var heads = new List<int>();
        foreach (var line in TOC.HeadScales)
        {
            var splitLine = HwlParser.ParsePlotLine(RawText[line]);
            foreach (var i in splitLine) heads.Add(int.Parse(i));
        }

        return heads;
    }

    public HwlData ImportHWL()
    {
        Hwl = new HwlData
        {
            Type = FileType,
            ProgramInfo = ImportProgramInfo(),
            WellInfo = ImportWellInfo(),
            Colors = ImportColors(),
            LineStyles = ImportLineStyles(),
            DisplayNames = ImportDisplaynames(),
            LithologyLegend = ImportLithologyLegend(),
            HeaderScales = ImportHeaderScales(),
            TVDs = ImportTVD(),
            CasingPoints = ImportCasingPoints(),
            NewBits = ImportNewBits(),
            Annotations = ImportAnnotations(),
            Abbreviations = ImportAbbreviations(),
            LithSymbols = ImportLithSymbols(),
            Chromatography = ImportChromatography(),
            Descriptions = ImportDescriptions(),
            Lithology = ImportLithology(),
            PlotScales = ImportPlotScales()
        };
        Hwl.Depths = ImportDepths();
        Hwl.DrillingParameterData = ImportPlotLines();
        Hwl.DrillingParameters = ImportDrillingParameters();

        return Hwl;
    }

    public List<string> ImportLineStyles()
    {
        var styleLine = TOC.LineStyles;
        var splitLine = HwlParser.ParseHeaderLine(RawText[styleLine]);
        // Call the ValidateItemsCount method
        if (!ValidateItemsCount(splitLine, TOC.LineStyles))
            throw new Exception("Invalid number of items in the LineStyles line.");
        //splitLine.RemoveAt(0);
        //List<int> lineStyles = splitLine.Select(int.Parse).ToList();
        return splitLine;
    }

    //NEEDS TO BE UPDATED FOR GEOTHERMAL (and not depth-dependent)
    public List<LithologyLine> ImportLithology()
    {
        var lithology = new List<LithologyLine>();
        foreach (var line in TOC.Plot)
        {
            var splitLine = HwlParser.ParsePlotLine(RawText[line]);
            if (string.IsNullOrEmpty(splitLine[TOC.PlotLithology[1]])) continue;
            var lithline = new List<string>();
            foreach (var i in TOC.PlotLithology)
                if (i == TOC.PlotLithology.Last() && FileType == "OG")
                {
                    var subSplitLine = HwlParser.ParseHeaderSub(splitLine[i]);
                    lithline.Add(subSplitLine[0]);
                    lithline.Add(subSplitLine[1]);
                }
                else
                {
                    lithline.Add(splitLine[i]);
                }

            lithology.Add(new LithologyLine(lithline));
        }

        return lithology;
    }

    public Dictionary<int, string> ImportLithologyLegend()
    {
        var lithologyLegend = new Dictionary<int, string>();
        var lithLine = TOC.Lithology;
        var splitLine = HwlParser.ParseHeaderLine(RawText[lithLine]);
        // Call the ValidateItemsCount method
        if (!ValidateItemsCount(splitLine, TOC.Lithology))
            throw new Exception("Invalid number of items in the Lithology line.");
        foreach (var item in splitLine)
        {
            var splitItem = HwlParser.ParseHeaderSub(item);
            //var dict = new Dictionary<int, string>();
            lithologyLegend.Add(int.Parse(splitItem[0]), splitItem[1]);
            //lithologyLegend.Add(dict);
        }

        return lithologyLegend;
    }

    public List<int> ImportLithSymbols()
    {
        var lith = new List<int>();
        {
            foreach (var i in TOC.LithSymbols)
                if (FileType == "OG")
                {
                    lith.Add(int.Parse(RawText[i].Trim()));
                }
                else
                {
                    var splitLine = HwlParser.ParsePlotLine(RawText[i]);
                    foreach (var item in splitLine) lith.Add(int.Parse(item.Trim()));
                }
        }
        return lith;
    }

    public List<NewBitLine>? ImportNewBits()
    {
        if (TOC.NewBits == null) return null;
        var newbits = new List<NewBitLine>();
        foreach (var i in TOC.NewBits)
        {
            var line = HwlParser.ParsePlotLine(RawText[i]);
            newbits.Add(new NewBitLine(line));
        }

        var sortedNewBits = newbits.OrderBy(o => o.Depth).ToList();
        return sortedNewBits;
    }

    public List<List<string>> ImportPlotLines()
    {
        var dpData = new List<List<string>>();
        foreach (var i in TOC.Plot) dpData.Add(HwlParser.ParsePlotLine(RawText[i]));

        return dpData;
    }

    public List<List<string>> ImportPlotScales()
    {
        var plotScales = new List<List<string>>();
        {
            foreach (var i in TOC.OGPlotScales)
            {
                var line = RawText[i];
                var splitLine = HwlParser.ParsePlotLine(line.Trim('$'));
                plotScales.Add(splitLine);
            }
        }

        return plotScales;
    }

    public ProgramInfo ImportProgramInfo()
    {
        var splitLine = HwlParser.ParseHeaderLine(RawText[TOC.ProgramInfo]);
        // Call the ValidateItemsCount method
        if (!ValidateItemsCount(splitLine, TOC.ProgramInfo))
            throw new Exception("Invalid number of items in the ProgramInfo line.");
        var programInfo = new ProgramInfo(splitLine);
        return programInfo;
    }

    public List<TVDLine> ImportTVD()
    {
        if (!TOC.TVD.HasValue)
        {
            return null;
        }

        var tvd = new List<TVDLine>();
        var splitLines = RawText[TOC.TVD.Value].Split("|").ToList();
        foreach (var item in splitLines)
        {
            var splitLine = item.Split("/").ToList();
            tvd.Add(new TVDLine(splitLine));
        }

        return tvd;
    }

    public WellInfo ImportWellInfo()
    {
        var splitLine = HwlParser.ParseHeaderLine(RawText[TOC.WellInfo]);
        // Call the ValidateItemsCount method
        if (!ValidateItemsCount(splitLine, TOC.WellInfo))
            throw new Exception("Invalid number of items in the WellInfo line.");
        var wellInfo = new WellInfo(splitLine);
        return wellInfo;
    }
}
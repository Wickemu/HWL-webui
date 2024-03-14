namespace HwlFileAnalyzer;

public class HwlData
{
    private List<int> depthIndex;

    private List<DrillingParameter> plottedDrillingParameters;
    private List<double> roundedDepths;

    private List<DrillingParameter> unplottedDrillingParameters;

    public string Type { get; set; }
    public ProgramInfo ProgramInfo { get; set; }
    public WellInfo WellInfo { get; set; }
    public List<int> Colors { get; set; }
    public List<string> DisplayNames { get; set; }
    public List<string> LineStyles { get; set; }
    public Dictionary<int, string> LithologyLegend { get; set; }
    public List<int> HeaderScales { get; set; }
    public List<TVDLine>? TVDs { get; set; }
    public List<CasingPointLine>? CasingPoints { get; set; }
    public List<NewBitLine>? NewBits { get; set; }
    public List<AnnotationLine> Annotations { get; set; }
    public Dictionary<string, string> Abbreviations { get; set; }
    public List<int> LithSymbols { get; set; }
    public List<List<string>> PlotScales { get; set; }
    public List<double> Depths { get; set; }

    public double TopDepth => Depths.Min();
    public double DepthOffset => TopDepth - 1;
    public double BottomDepth => Depths.Max();
    public List<ChromatographyLine> Chromatography { get; set; }
    public List<DescriptionLine> Descriptions { get; set; }
    public List<List<string>> DrillingParameterData { get; set; }
    public List<DrillingParameter> DrillingParameters { get; set; }

    public List<DrillingParameter> PlottedDrillingParameters
    {
        get
        {
            if (plottedDrillingParameters != null) plottedDrillingParameters.Clear();
            else plottedDrillingParameters = new List<DrillingParameter>();
            foreach (var item in DrillingParameters)
                if (item.PlotEnabled)
                    plottedDrillingParameters.Add(item);
                else continue;

            return plottedDrillingParameters;
        }
    }

    public List<DrillingParameter> UnplottedDrillingParameters
    {
        get
        {
            if (unplottedDrillingParameters != null) unplottedDrillingParameters.Clear();
            else unplottedDrillingParameters = new List<DrillingParameter>();
            foreach (var item in DrillingParameters)
                if (item.PlotEnabled == false)
                    unplottedDrillingParameters.Add(item);
                else continue;

            return unplottedDrillingParameters;
        }
    }

    public List<LithologyLine> Lithology { get; set; }

    public List<double> RoundedDepths
    {
        get
        {
            var rounded = new List<double>();
            foreach (var item in Depths)
                if (Math.Abs(item) % 50 == 0)
                    rounded.Add(item);

            return rounded;
        }
    }

    public void FixScales()
    {
        foreach (var parameter in PlottedDrillingParameters)
        {
            parameter.ApplySuggestedScales();
        }
    }
}
using System.Drawing;
using System.Reflection.Metadata;
using HWLClassLibrary.Data_Service;

namespace HwlFileAnalyzer;

public abstract class DrillingParameter
{
    protected Dictionary<double, int> data;

    protected HwlData hwl;

    protected bool plotEnabled;

    public int SuggestedScale
    {
        get
        {
            if (plotEnabled) return (int)Math.Ceiling((double)Data.Values.Max() / 50) * 50;
            else return 0;
        }
        set => SuggestedScale = value;
    }

    public virtual int SuggestedOverscale => SuggestedScale * 2;

    public DrillingParameter(HwlData hwlData)
    {
        hwl = hwlData;
        if (PlotLinePosition.HasValue)
        {
            SetPlotData();
            if (Data.Values.Any(i => i != 0)) PlotEnabled = true;
            else plotEnabled = false;
        }

        if (hwl.Type == "OG" && OGPlotScalePosition.HasValue) PlotScales = GetPlotScales();
        if (hwl.Type == "OG" && OGPlotOverscalePosition.HasValue) PlotOverscales = SetPlotOverscales();
        if (hwl.Type == "GEO" && GeoHeaderScalePosition.HasValue)
            HeaderScale = hwl.HeaderScales[HeaderScalePosition.Value];
        //if (hwl.Type == "GEO" && GeoHeaderOverscalePosition.HasValue)
        //    HeaderOverscale = hwl.HeaderScales[HeaderOverscalePosition.Value];


        if (ColorPosition.HasValue) Color = ColorTranslator.FromWin32(hwl.Colors[ColorPosition.Value]);

        if (DisplayNamePosition.HasValue) DisplayName = hwl.DisplayNames[DisplayNamePosition.Value];
        else DisplayName = GetType().Name;

        if (LineStylePosition.HasValue) LineStyle = int.Parse(hwl.LineStyles[LineStylePosition.Value]);
        else LineStyle = 0;
    }

    protected virtual int? OGColorPosition { get; }
    protected virtual int? GeoColorPosition { get; }
    public int? ColorPosition => hwl.Type == "OG" ? OGColorPosition : GeoColorPosition;
    public Color Color { get; set; } = Color.Black;

    protected virtual int? OGColorOverscalePosition { get; }
    protected virtual int? GeoColorOverscalePosition { get; }
    public int? ColorOverscalePosition => hwl.Type == "OG" ? OGColorOverscalePosition : GeoColorOverscalePosition;

    protected virtual int? OGDisplayNamePosition { get; }
    protected virtual int? GeoDisplayNamePosition { get; }
    public int? DisplayNamePosition => hwl.Type == "OG" ? OGDisplayNamePosition : GeoDisplayNamePosition;
    public string DisplayName { get; set; }

    protected virtual int? OGDisplayNameOverscalePosition { get; }
    public int? DisplayNameOverscale { get; }

    protected virtual int? OGLineStylesPosition { get; }
    protected virtual int? GeoLineStylesPosition { get; }
    public int? LineStylePosition => hwl.Type == "OG" ? OGLineStylesPosition : GeoLineStylesPosition;
    public int LineStyle { get; set; }

    protected virtual int? OGHeaderScalePosition { get; }
    protected virtual int? GeoHeaderScalePosition { get; } = null;
    public int? HeaderScalePosition => GeoHeaderScalePosition;
    public int? HeaderScale { get; set; }

    protected virtual int? OGHeaderOverscalePosition { get; }
    protected virtual int? GeoHeaderOverscalePosition { get; } = null;
    public int? HeaderOverscalePosition => GeoHeaderOverscalePosition;
    public int? HeaderOverscale { get; set; }

    protected virtual int? GeoPlotLinePosition { get; }
    protected virtual int? OGPlotLinePosition { get; }
    public int? PlotLinePosition => hwl.Type == "OG" ? OGPlotLinePosition : GeoPlotLinePosition;

    public virtual int? OGPlotScalePosition { get; }
    public Dictionary<double, int> PlotScales { get; }

    public virtual int? OGPlotOverscalePosition { get; }
    public Dictionary<double, int> PlotOverscales { get; }

    public virtual bool PlotEnabled
    {
        get => plotEnabled;
        set => plotEnabled = value;
    }

    public virtual Dictionary<double, int> Data
    {
        get => data;
        private set => data = value;
    }

    public virtual string ShortName { get; }

    public virtual string UnitOfMeasurement => "Units";

    protected virtual bool HasUniqueParser => false;

    public virtual int Column { get; set; } = 7;

    public virtual bool InvertedScale { get; set; }

    public List<int> GetPlotData()
    {
        var plot = new List<int>();
        foreach (var item in hwl.DrillingParameterData) plot.Add(int.Parse(item[PlotLinePosition.Value]));

        return plot;
    }

    private Dictionary<double, int> GetPlotScales()
    {
        var dict = new Dictionary<double, int>();
        foreach (var item in hwl.PlotScales)
            dict.Add(double.Parse(item[0]), int.Parse(item[OGPlotScalePosition.Value]));

        return dict;
    }

    protected virtual void SetPlotData()
    {
        if (data is null) data = new Dictionary<double, int>();
        else data.Clear();
        foreach (var item in hwl.DrillingParameterData)
            if (string.IsNullOrEmpty(item[PlotLinePosition.Value])) continue;
            else data.Add(double.Parse(item[0]), int.Parse(item[PlotLinePosition.Value]));
    }

    private Dictionary<double, int> SetPlotOverscales()
    {
        var dict = new Dictionary<double, int>();
        foreach (var item in hwl.PlotScales)
            dict.Add(double.Parse(item[0]), int.Parse(item[OGPlotOverscalePosition.Value]));

        return dict;
    }


    public List<ScaleWarning> TestPlotScales()
    {
        var warnings = new List<ScaleWarning>();
        var issueCount = 0;
        if (hwl.Type == "OG" && plotEnabled)
            if (PlotScales.Count == PlotOverscales.Count)
                // Require equal count.
                foreach (var pair in PlotScales)
                {
                    int overvalue;
                    if (PlotOverscales.TryGetValue(pair.Key, out overvalue))
                    {
                        if (pair.Value <= 0)
                        {
                            issueCount += 1;
                            warnings.Add(new ScaleWarning
                            {
                                Parameter = this,
                                Message =
                                    $"Warning: segment scale at depth {pair.Key} for field {DisplayName}; Scale value is {pair.Value} which should be greater than 0."
                            });
                        }
                        else if (overvalue <= pair.Value)
                        {
                            issueCount += 1;
                            warnings.Add(new ScaleWarning
                            {
                                Parameter = this,
                                Message =
                                    $"Warning: segment overscale at depth {pair.Key} for field {DisplayName}; Scale value is {pair.Value} while Overscale value is {overvalue}."
                            });
                        }
                    }
                }

        return warnings;
    }

    public List<ScaleWarning> TestHeaderScales()
    {
        var warnings = new List<ScaleWarning>();

        if (hwl.Type.ToUpper() == "GEO")
            if (HeaderScale.HasValue)
                if (HeaderScale <= 0)
                {
                    var scaleWarning = new ScaleWarning
                    {
                        Parameter = this,
                        Message =
                            $"Warning: header scale for field {DisplayName}; Scale value is {HeaderScale} which is less than or below 0. The recommended value is {SuggestedScale}",
                        SuggestedScale = this.SuggestedScale,
                        SuggestedOverscale = this.SuggestedOverscale
                    };

                    warnings.Add(scaleWarning);
                }

        return warnings;
    }

    public void ApplySuggestedScales()
    {
        if (hwl.Type.ToUpper() == "GEO")
        {
            if (HeaderScalePosition.HasValue)
            {
                HeaderScale = SuggestedScale;
                HeaderOverscale = SuggestedOverscale;
                hwl.HeaderScales[(int)HeaderScalePosition] = (int)HeaderScale;
            }
            else
            {
                // Log or handle the case when HeaderScalePosition is null
                Console.WriteLine($"HeaderScalePosition is null for parameter {DisplayName}");
            }
        }
        else if (hwl.Type.ToUpper() == "OG")
        {
            // Update all PlotScales and PlotOverscales values to the suggested scale
            foreach (var key in PlotScales.Keys.ToList())
            {
                PlotScales[key] = SuggestedScale;
                PlotOverscales[key] = SuggestedOverscale;
            }

            // Update all hwl.PlotScales values to the suggested scale
            for (int row = 0; row < hwl.PlotScales.Count; row++)
            {
                if (hwl.PlotScales[row].Count > (int)OGPlotScalePosition)
                {
                    hwl.PlotScales[row][(int)OGPlotScalePosition] = SuggestedScale.ToString();
                }
                else
                {
                    // Log the out-of-range position or handle the error as needed
                    Console.WriteLine($"OGPlotScalePosition {(int)OGPlotScalePosition} is out of range for row {row}");
                }
            }
        }
    }




}
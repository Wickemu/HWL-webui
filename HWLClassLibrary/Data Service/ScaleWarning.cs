using HwlFileAnalyzer;

namespace HWLClassLibrary.Data_Service;

public class ScaleWarning
{
    public DrillingParameter Parameter { get; set; }
    public string Message { get; set; }
    public double SuggestedScale { get; set; }
    public double SuggestedOverscale { get; set; }
}
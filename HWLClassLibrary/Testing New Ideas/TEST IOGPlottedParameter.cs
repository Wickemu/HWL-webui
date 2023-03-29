namespace HwlFileAnalyzer.Testing_New_Ideas
{
    interface IOGPlottedParameter
    {
        List<int> Data { get; }
        List<int> Scales { get; set; }
        List<int> Overscales { get; set; }
    }
}

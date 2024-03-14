using System.Text;
using HWLClassLibrary.Data_Service;
using HwlFileAnalyzer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HWLRazor.Pages;

public class IndexModel : PageModel
{
    private readonly IHwlDataService _hwlDataService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, IHwlDataService hwlDataService)
    {
        _logger = logger;
        _hwlDataService = hwlDataService;
    }

    [BindProperty] public IFormFile UploadedFile { get; set; }

    public string Message { get; private set; }
    public bool WellInfoInitialized { get; set; } = false;
    public bool FileUploaded { get; private set; }

    public Importer Importer
    {
        get => _hwlDataService.Importer;
        set => _hwlDataService.Importer = value;
    }

    public string FileContentBase64 { get; set; }
    public string Filename { get; set; }

    public HwlData HwlData
    {
        get => _hwlDataService.HwlData;
        set => _hwlDataService.HwlData = value;
    }

    public List<ScaleWarning> ScaleWarnings
    {
        get => _hwlDataService.ScaleWarnings;
        set=> _hwlDataService.ScaleWarnings = value;
    }

    public Func<object, string> RenderTableHeaderProperty => RenderTableHeader;
    public Func<object, string> RenderTableRowProperty => RenderTableRow;


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (UploadedFile == null || UploadedFile.Length == 0)
        {
            Message = "Please select a file to upload.";
            return Page();
        }

        var filePath = Path.GetTempFileName();
        using (var stream = System.IO.File.Create(filePath))
        {
            await UploadedFile.CopyToAsync(stream);
        }

        Filename = Path.GetFileName(filePath);
        _hwlDataService.FileContent = await System.IO.File.ReadAllTextAsync(filePath);
        FileContentBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_hwlDataService.FileContent));

        _hwlDataService.Importer = new Importer(filePath);
        _hwlDataService.HwlData = Importer.ImportHWL();
        HttpContext.Session.SetString("HwlData", JsonConvert.SerializeObject(HwlData));
        FileUploaded = true;
        _logger.LogInformation($"HwlData: {HwlData}");

        // Cleanup the temporary file
        System.IO.File.Delete(filePath);
        Message = "File uploaded and processed successfully.";
        return Page();
    }

    [HttpPost]
    public IActionResult OnPostUpdatePlotScales([FromBody] EditedCellsWrapper editedCellsWrapper)
    {
        var editedCells = editedCellsWrapper.EditedCells;

        if (editedCells == null) return BadRequest();

        if (HwlData == null || HwlData.PlottedDrillingParameters == null)
        {
            Console.WriteLine("Error: HwlData or HwlData.PlottedDrillingParameters is null");
            return BadRequest();
        }

        foreach (var cell in editedCells)
        {
            var row = cell.Row;
            var col = cell.Col;
            var newValue = int.Parse(cell.NewValue);

            // Update the relevant DrillingParameter object using row and col information
            var parameter = HwlData.PlottedDrillingParameters.ElementAtOrDefault(col - 1);
            if (parameter == null)
            {
                Console.WriteLine($"Error: Drilling parameter not found at index {col - 1}");
                continue;
            }

            var depth = Convert.ToDouble(HwlData.PlotScales[row][0]);
            if (col % 2 == 1) // Update PlotScales
            {
                if (parameter.PlotScales.ContainsKey(depth))
                {
                    parameter.PlotScales[depth] = newValue;
                    HwlData.PlotScales[row][parameter.OGPlotScalePosition.Value] = Convert.ToString(newValue);
                    Importer.RawText[Importer.TOC.OGPlotScales[row]] =
                        Importer.UnparseOGPlotScales(HwlData.PlotScales[row]);
                }
                else
                {
                    Console.WriteLine(
                        $"Error: Depth {depth} not found in PlotScales for parameter {parameter.ShortName}");
                }
            }
            else // Update PlotOverscales
            {
                if (parameter.PlotOverscales.ContainsKey(depth))
                {
                    parameter.PlotOverscales[depth] = newValue;
                    HwlData.PlotScales[row][parameter.OGPlotOverscalePosition.Value] = Convert.ToString(newValue);
                    Importer.RawText[Importer.TOC.OGPlotScales[row - 1]] =
                        Importer.UnparseOGPlotScales(HwlData.PlotScales[row - 1]);
                }
                else
                {
                    Console.WriteLine(
                        $"Error: Depth {depth} not found in PlotOverscales for parameter {parameter.ShortName}");
                }
            }
        }

        // Save the updated HwlData object back to the session
        HttpContext.Session.SetString("HwlData", JsonConvert.SerializeObject(HwlData));

        return new JsonResult(new { success = true, hwlData = HwlData });
    }

    public string RenderTableHeader(object item)
    {
        var properties = item.GetType().GetProperties();
        var headerBuilder = new StringBuilder();

        headerBuilder.Append("<tr>");

        // Put the Depth property first if it exists
        var depthProperty = properties.FirstOrDefault(p => p.Name == "Depth");
        if (depthProperty != null) headerBuilder.AppendFormat("<th>{0}</th>", depthProperty.Name);

        // Add the remaining properties, excluding Depth
        foreach (var property in properties.Where(p => p.Name != "Depth"))
            headerBuilder.AppendFormat("<th>{0}</th>", property.Name);

        headerBuilder.Append("</tr>");

        return headerBuilder.ToString();
    }

    public string RenderTableRow(object item)
    {
        var properties = item.GetType().GetProperties();
        var rowBuilder = new StringBuilder();

        rowBuilder.Append("<tr>");

        // Put the Depth property value first if it exists
        var depthProperty = properties.FirstOrDefault(p => p.Name == "Depth");
        if (depthProperty != null) rowBuilder.AppendFormat("<td>{0}</td>", depthProperty.GetValue(item));

        // Add the remaining property values, excluding Depth
        foreach (var property in properties.Where(p => p.Name != "Depth"))
            rowBuilder.AppendFormat("<td>{0}</td>", property.GetValue(item));

        rowBuilder.Append("</tr>");

        return rowBuilder.ToString();
    }

    public List<ScaleWarning> TestScales()
    {
        if (HwlData.Type.ToUpper() == "GEO")
            _hwlDataService.ScaleWarnings = TestHeaderScales();
        if (HwlData.Type.ToUpper() == "OG")
            _hwlDataService.ScaleWarnings = TestPlotScales();

        return _hwlDataService.ScaleWarnings;
    }

    public List<ScaleWarning> TestPlotScales()
    {
        _logger.LogInformation($"HwlData: {HwlData}");

        var allWarnings = new List<ScaleWarning>();

        foreach (var parameter in HwlData.PlottedDrillingParameters)
            if (parameter.PlotEnabled)
                allWarnings.AddRange(parameter.TestPlotScales());

        return allWarnings;
    }

    public List<ScaleWarning> TestHeaderScales()
    {
        _logger.LogInformation($"HwlData: {HwlData}");

        foreach (var parameter in HwlData.PlottedDrillingParameters)
            if (parameter.HeaderScalePosition.HasValue || parameter.HeaderOverscalePosition.HasValue)
                ScaleWarnings.AddRange(parameter.TestHeaderScales());

        return ScaleWarnings;
    }

    public async Task<IActionResult> OnPostApplySuggestedScalesAsync()
    {
        if (ScaleWarnings != null)
        {
            foreach (var scaleWarning in ScaleWarnings)
            {
                var parameter = scaleWarning.Parameter;
                parameter.ApplySuggestedScales();

                if (HwlData.Type.ToUpper() == "GEO")
                {
                    Importer.RawText[Importer.TOC.HeadScales[0]] = Importer.UnparseHeaderLine(HwlData.HeaderScales);
                }
                else if (HwlData.Type.ToUpper() == "OG")
                {
                    for (int row = 0; row < HwlData.PlotScales.Count; row++)
                    {
                        Importer.RawText[Importer.TOC.OGPlotScales[row]] = Importer.UnparseOGPlotScales(HwlData.PlotScales[row]);
                    }
                }
            }
        }
        HttpContext.Session.SetString("HwlData", JsonConvert.SerializeObject(HwlData));

        return new JsonResult(new { success = true, hwlData = HwlData });
    }




    public IActionResult OnGetGetRawFileContent()
    {
        return Content(_hwlDataService.FileContent, "text/plain", Encoding.UTF8);
    }

    public IActionResult OnPostUpdateHeaderScales([FromBody] EditedCellsWrapper editedCellsWrapper)
    {
        var editedCells = editedCellsWrapper.EditedCells;

        if (editedCells == null) return BadRequest();

        if (HwlData == null || HwlData.DrillingParameters == null)
        {
            Console.WriteLine("Error: HwlData or HwlData.DrillingParameters is null");
            return BadRequest();
        }

        foreach (var cell in editedCells)
        {
            var row = cell.Row;
            var col = cell.Col;
            var newValue = int.Parse(cell.NewValue);

            // Update the relevant DrillingParameter object using col information
            var parameter = HwlData.DrillingParameters.ElementAtOrDefault(col);
            if (parameter == null)
            {
                Console.WriteLine($"Error: Drilling parameter not found at index {col}");
                continue;
            }

            // Update HeaderScales
            HwlData.HeaderScales[col] = newValue;
        }

        Importer.RawText[Importer.TOC.HeadScales[0]] =
            Importer.UnparseHeaderLine(HwlData.HeaderScales); // Save the updated HwlData object back to the session
        HttpContext.Session.SetString("HwlData", JsonConvert.SerializeObject(HwlData));

        return new JsonResult(new { success = true, hwlData = HwlData });
    }

    public IActionResult OnGetExportData()
    {
        if (Importer?.RawText == null) return BadRequest("Error: Importer or RawText is null.");

        return new JsonResult(new { rawText = Importer.RawText });
    }

    public async Task<IActionResult> OnPostAutoSetAllScalesAsync()
    {
        foreach (var parameter in HwlData.PlottedDrillingParameters)
        {
            parameter.ApplySuggestedScales();

            if (HwlData.Type.ToUpper() == "GEO")
            {
                Importer.RawText[Importer.TOC.HeadScales[0]] = Importer.UnparseHeaderLine(HwlData.HeaderScales);
            }
            else if (HwlData.Type.ToUpper() == "OG")
            {
                for (int row = 0; row < HwlData.PlotScales.Count; row++)
                {
                    Importer.RawText[Importer.TOC.OGPlotScales[row]] = Importer.UnparseOGPlotScales(HwlData.PlotScales[row]);
                }
            }
        }

        HttpContext.Session.SetString("HwlData", JsonConvert.SerializeObject(HwlData));

        return new JsonResult(new { success = true, hwlData = HwlData });
    }


    public class EditedCell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string NewValue { get; set; }
    }

    public class EditedCellsWrapper
    {
        public List<EditedCell> EditedCells { get; set; }
    }
}
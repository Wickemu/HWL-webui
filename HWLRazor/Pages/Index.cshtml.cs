using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HwlFileAnalyzer;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;


namespace HWLRazor.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        private readonly IHwlDataService _hwlDataService;
        public IndexModel(ILogger<IndexModel> logger, IHwlDataService hwlDataService)
        {
            _logger = logger;
            _hwlDataService = hwlDataService;
        }


        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public string Message { get; private set; }
        public bool WellInfoInitialized { get; set; } = false;
        public bool FileUploaded { get; private set; } = false;
        Importer Importer { get; set; }

        public HwlData HwlData
        {
            get => _hwlDataService.HwlData;
            set => _hwlDataService.HwlData = value;
        }

        public Func<object, string> RenderTableHeaderProperty => RenderTableHeader;
        public Func<object, string> RenderTableRowProperty => RenderTableRow;


        public void OnGet()
        {
            string hwlDataJson = HttpContext.Session.GetString("HwlData");
            if (!string.IsNullOrEmpty(hwlDataJson))
            {
                HwlData = JsonSerializer.Deserialize<HwlData>(hwlDataJson);
            }
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                Message = "Please select a file to upload.";
                return Page();
            }

            string filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await UploadedFile.CopyToAsync(stream);
            }

            // Now the file is saved, you can use the Importer class
            Importer = new Importer(filePath);
            var hwlData = Importer.ImportHWL();
            HttpContext.Session.SetString("HwlData", JsonSerializer.Serialize(hwlData));
            FileUploaded = true;
            // You can now use the hwlData variable as you wish
            _hwlDataService.HwlData = hwlData;
            _logger.LogInformation($"HwlData: {HwlData}");

            // Cleanup the temporary file
            System.IO.File.Delete(filePath);
            Message = "File uploaded and processed successfully.";
            return Page();
        }

        [HttpPost]
        public IActionResult OnPostUpdatePlotScales([FromBody] EditedCellsWrapper editedCellsWrapper)
        {
            List<EditedCell> editedCells = editedCellsWrapper.EditedCells;

            if (editedCells == null)
            {
                return BadRequest();
            }

            if (HwlData == null || HwlData.PlotteDrillingParameters == null)
            {
                Console.WriteLine("Error: HwlData or HwlData.PlotteDrillingParameters is null");
                return BadRequest();
            }

            foreach (var cell in editedCells)
            {
                int row = cell.Row;
                int col = cell.Col;
                int newValue = int.Parse(cell.NewValue);

                // Update the relevant DrillingParameter object using row and col information
                var parameter = HwlData.PlotteDrillingParameters.ElementAtOrDefault(col - 1);
                if (parameter == null)
                {
                    Console.WriteLine($"Error: Drilling parameter not found at index {col - 1}");
                    continue;
                }

                double depth = Convert.ToDouble(HwlData.PlotScales[row][0]);
                if (col % 2 == 1) // Update PlotScales
                {
                    if (parameter.PlotScales.ContainsKey(depth))
                    {
                        parameter.PlotScales[depth] = newValue;
                        HwlData.PlotScales[row][parameter.OGPlotScalePosition.Value] = Convert.ToString(newValue);
                        Importer.RawText[Importer.TOC.OGPlotScales[row]] = Importer.UnparseOGPlotScales(HwlData.PlotScales[row]);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Depth {depth} not found in PlotScales for parameter {parameter.ShortName}");
                    }
                }
                else // Update PlotOverscales
                {
                    if (parameter.PlotOverscales.ContainsKey(depth))
                    {
                        parameter.PlotOverscales[depth] = newValue;
                        HwlData.PlotScales[row][parameter.OGPlotOverscalePosition.Value] = Convert.ToString(newValue);
                        Importer.RawText[Importer.TOC.OGPlotScales[row - 1]] = Importer.UnparseOGPlotScales(HwlData.PlotScales[row - 1]);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Depth {depth} not found in PlotOverscales for parameter {parameter.ShortName}");
                    }
                }
            }
            // Save the updated HwlData object back to the session
            HttpContext.Session.SetString("HwlData", JsonSerializer.Serialize(HwlData));

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


        public string RenderTableHeader(object item)
        {
            var properties = item.GetType().GetProperties();
            StringBuilder headerBuilder = new StringBuilder();

            headerBuilder.Append("<tr>");

            // Put the Depth property first if it exists
            var depthProperty = properties.FirstOrDefault(p => p.Name == "Depth");
            if (depthProperty != null)
            {
                headerBuilder.AppendFormat("<th>{0}</th>", depthProperty.Name);
            }

            // Add the remaining properties, excluding Depth
            foreach (var property in properties.Where(p => p.Name != "Depth"))
            {
                headerBuilder.AppendFormat("<th>{0}</th>", property.Name);
            }

            headerBuilder.Append("</tr>");

            return headerBuilder.ToString();
        }
        public string RenderTableRow(object item)
        {
            var properties = item.GetType().GetProperties();
            StringBuilder rowBuilder = new StringBuilder();

            rowBuilder.Append("<tr>");

            // Put the Depth property value first if it exists
            var depthProperty = properties.FirstOrDefault(p => p.Name == "Depth");
            if (depthProperty != null)
            {
                rowBuilder.AppendFormat("<td>{0}</td>", depthProperty.GetValue(item));
            }

            // Add the remaining property values, excluding Depth
            foreach (var property in properties.Where(p => p.Name != "Depth"))
            {
                rowBuilder.AppendFormat("<td>{0}</td>", property.GetValue(item));
            }

            rowBuilder.Append("</tr>");

            return rowBuilder.ToString();
        }

        public List<string> TestScalesForPlottedParameters()
        {
            _logger.LogInformation($"HwlData: {HwlData}");

            List<string> allWarnings = new List<string>();

            foreach (var parameter in HwlData.PlotteDrillingParameters)
            {
                if (parameter.PlotEnabled)
                {
                    allWarnings.AddRange(parameter.TestScales());
                }
            }

            return allWarnings;
        }





    }
}

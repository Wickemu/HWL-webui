using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HwlFileAnalyzer; // Don't forget to include this
using System.Text;

namespace HWLRazor.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public string Message { get; private set; }
        public bool WellInfoInitialized { get; set; } = false;
        public bool FileUploaded { get; private set; } = false;
        public string FileType { get; private set; }
        public Dictionary<string, string> Abbreviations { get; private set; }
        public WellInfo WellInfo { get; private set; }
        public List<AnnotationLine> Annotations { get; set; }
        public List<List<string>> PlotScales { get; set; }
        public List<DrillingParameter> DrillingParameters { get; set; }
        public List<DrillingParameter> PlottedDrillingParameters { get; private set; }
        public ProgramInfo ProgramInfo { get; private set; }
        public List<CasingPointLine> CasingPoints { get; set; }
        public List<NewBitLine> NewBits { get; set; }
        public List<TVDLine> TVDs { get; set; }
        public Dictionary<int, string> LithologyLegend { get; set; }
        public List<DescriptionLine> Descriptions { get; private set; }

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

            string filePath = Path.GetTempFileName();
            using (var stream = System.IO.File.Create(filePath))
            {
                await UploadedFile.CopyToAsync(stream);
            }

            // Now the file is saved, you can use the Importer class
            Importer importer = new Importer(filePath);
            var hwlData = importer.ImportHWL();
            FileUploaded = true;
            // You can now use the hwlData variable as you wish

            FileType = hwlData.Type;
            Abbreviations = hwlData.Abbreviations;
            WellInfo = hwlData.WellInfo;
            Annotations = hwlData.Annotations;
            PlotScales = hwlData.PlotScales;
            DrillingParameters = hwlData.DrillingParameters;
            PlottedDrillingParameters = hwlData.PlotteDrillingParameters;
            ProgramInfo = hwlData.ProgramInfo;
            WellInfoInitialized = true;
            CasingPoints = hwlData.CasingPoints;
            NewBits = hwlData.NewBits;
            TVDs = hwlData.TVDs;
            LithologyLegend = hwlData.LithologyLegend;
            Descriptions = hwlData.Descriptions;
            // Cleanup the temporary file
            System.IO.File.Delete(filePath);

            Message = "File uploaded and processed successfully.";
            return Page();
        }


        [HttpPost]
        public IActionResult OnPostUpdatePlotScales([FromBody] List<List<string>> plotScalesData)
        {
            // Update the PlotScales property
            PlotScales = plotScalesData;

            // Save the data as needed

            return new JsonResult("Success");
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
            List<string> allWarnings = new List<string>();

            foreach (var parameter in PlottedDrillingParameters)
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

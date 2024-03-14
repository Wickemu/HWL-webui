using HWLClassLibrary.Data_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwlFileAnalyzer
{
    public interface IHwlDataService
    {
        HwlData HwlData { get; set; }
        string FileContent { get; set; }
        Importer Importer { get; set; }
        public List<ScaleWarning> ScaleWarnings { get; set; }
    }

    public class HwlDataService : IHwlDataService
    {
        public HwlData HwlData { get; set; }
        public string FileContent { get; set; }
        public Importer Importer { get; set; }
        public List<ScaleWarning> ScaleWarnings { get; set; } = new List<ScaleWarning>(); // Initialize ScaleWarnings with an empty list
    }
}

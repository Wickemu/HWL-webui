using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwlFileAnalyzer
{
    public class HwlDataService : IHwlDataService
    {
        public HwlData HwlData { get; set; }
        public string FileContent { get; set; }
        public Importer Importer { get; set; }
    }
}

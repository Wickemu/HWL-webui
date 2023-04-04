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
    }
}

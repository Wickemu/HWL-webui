namespace HwlFileAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public struct WellInfo
    {
        public WellInfo(List<string> line)
        {
            Company = line[0];
            if (line[1].Contains('^'))
            {
                var wellNames = HwlParser.ParseHeaderSub(line[1]);
                Well = wellNames[0];
                HeaderTitle = wellNames[1];
            }
            else
            {
                Well = line[1];
                HeaderTitle = Well;
            }
            Location = line[2];
            Elevation = line[3];
            Field = line[4];
            County = line[5];
            State = line[6];
            var apiSplit = line[7].Split("$", StringSplitOptions.None);
            API = apiSplit[0];
            UWI = apiSplit[1];
            SpudDate = line[8];
            StartDate = line[9];
            EndDate = line[10];
            CoGeologist = line[11];
            DrillSiteManager = line[12];
            Contractor = line[13];
            Rig = line[14];
            MudCompany = line[15];
            Loggers = line[16].Split(",".Trim(), StringSplitOptions.None).ToList();
            BitSize = HwlParser.ParseMultiLine(line[17]);
            Casing = HwlParser.ParseMultiLine(line[18]);
            Type = line[19];
            Calibration = line[20];
            LoggingDepths = line[21];
        }

        public string API { get; }
        public List<string> BitSize { get; }
        public string Calibration { get; }
        public List<string> Casing { get; }
        public string CoGeologist { get; }
        public string Company { get; }
        public string Contractor { get; }
        public string County { get; }
        public string DrillSiteManager { get; }
        public string Elevation { get; }
        public string EndDate { get; }
        public string Field { get; }
        public string HeaderTitle { get; }
        public string Location { get; }
        public List<string> Loggers { get; }
        public string LoggingDepths { get; }
        public string MudCompany { get; }
        public string Rig { get; }
        public string SpudDate { get; }
        public string StartDate { get; }
        public string State { get; }
        public string Type { get; }
        public string UWI { get; }
        public string Well { get; }
        //Company           
        //Well^HeaderTitle
        //Location
        //Elevation
        //Field
        //County
        //State
        //API
        //SpudDate
        //LoggingStart
        //LogEnd
        //CompanyGeologist
        //DrillSiteManager
        //Contractor
        //Rig
        //MudCompany
        //Loggers
        //BitSize
        //Casing
        //Type
        //(Unknown... Calibration?)
        //LoggingDepths

    }

    public struct ProgramInfo
    {
        public ProgramInfo(List<string> line)
        {
            SoftwareName = line[0];
            WellType = line[1];
            NarrowFormat = Convert.ToBoolean(int.Parse(line[2]));
            DPI = int.Parse(line[3]);
            UnitsPerInch = int.Parse(line[4]);
            DrawInterpretiveLithology = Convert.ToBoolean(int.Parse(line[5]));
            DrawTVDAnnotations = Convert.ToBoolean(int.Parse(line[6]));
            VersionNumber = int.Parse(line[7]);
            DepthNotations00 = Convert.ToBoolean(int.Parse(line[8]));
            TVDNotationsOn = Convert.ToBoolean(int.Parse(line[9]));
            PlotAux1 = Convert.ToBoolean(int.Parse(line[10]));
            PlotAux2 = Convert.ToBoolean(int.Parse(line[11]));
            PlotAux3 = Convert.ToBoolean(int.Parse(line[12]));
            PlotAux4 = Convert.ToBoolean(int.Parse(line[13]));
            PlotAux5 = Convert.ToBoolean(int.Parse(line[14]));
            PlotAux6 = Convert.ToBoolean(int.Parse(line[15]));
        }

        public string SoftwareName { get; }
        public string WellType { get; }
        public bool NarrowFormat { get; }
        public int DPI { get; }
        public int UnitsPerInch { get; }
        public bool DrawInterpretiveLithology { get; set; }
        public bool DrawTVDAnnotations { get; set; }
        public int VersionNumber { get; }
        public bool DepthNotations00 { get; set; }
        public bool TVDNotationsOn { get; set; }
        public bool PlotAux1 { get; set; }
        public bool PlotAux2 { get; set; }
        public bool PlotAux3 { get; set; }
        public bool PlotAux4 { get; set; }
        public bool PlotAux5 { get; set; }
        public bool PlotAux6 { get; set; }
    }

    //VersionNumber	DepthNotationson00s	Unknown	PlotAux1	PlotAux2	PlotAux3	PlotAux4	PlotAux5	PlotAux6


}

namespace HwlFileAnalyzer
{
    using System;
    using CsvHelper;
    using System.Collections.Generic;
    using System.Drawing;

    public struct AnnotationLine
    {
        public AnnotationLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            Section = int.Parse(line[1]);
            Offset = int.Parse(line[2]);
            Text = line[3];
            FontHeight = int.Parse(line[4]);
            FontWidth = int.Parse(line[5]);
            Font = line[6];
            Italic = Convert.ToBoolean(int.Parse(line[7]));
            Underline = Convert.ToBoolean(int.Parse(line[8]));
            Color = ColorTranslator.FromWin32(int.Parse(line[9]));
            Bold = Convert.ToBoolean(int.Parse(line[10]));
            if (line.Count == 16)
            {
                Display = Convert.ToBoolean(int.Parse(line[11]));
                DisplayOnColor = Convert.ToBoolean(int.Parse(line[12]));
                DisplayOnBlack = Convert.ToBoolean(int.Parse(line[13]));
                DisplayOnMD = Convert.ToBoolean(int.Parse(line[14]));
                DisplayOnDP = Convert.ToBoolean(int.Parse(line[15]));
            }
            else
            {
                Display = false;
                DisplayOnColor = false;
                DisplayOnBlack = false;
                DisplayOnMD = false;
                DisplayOnDP = false;
            }
        }

        public bool Bold { get; set; }
        public Color Color { get; set; }
        public double Depth { get; set; }
        public bool Display { get; set; }
        public bool DisplayOnBlack { get; set; }
        public bool DisplayOnColor { get; set; }
        public bool DisplayOnDP { get; set; }
        public bool DisplayOnMD { get; set; }
        public string Font { get; set; }
        public int FontHeight { get; set; }
        public int FontWidth { get; set; }
        public bool Italic { get; set; }
        public int Offset { get; set; }
        public int Section { get; set; }
        public string Text { get; set; }
        public bool Underline { get; set; }
    }

    public struct CasingPointLine
    {
        public CasingPointLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            Diameter = line[1];
            Date = line[2];
        }

        public string Date { get; set; }
        public double Depth { get; set; }

        public string Diameter { get; set; }
    }

    public struct TVDLine
    {
        public TVDLine(List<string> line)
        {
            MD = int.Parse(line[0]);
            TVD = int.Parse(line[1]);
        }

        public int MD { get; set; }
        public int TVD { get; set; }
    }

    public struct ChromatographyLine
    {
        public ChromatographyLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            Methane = int.Parse(line[4]);
            Ethanol = int.Parse(line[5]);
            Propane = int.Parse(line[6]);
            Butanel = int.Parse(line[7]);
            Pentane = int.Parse(line[9]);
            TotalGas = int.Parse(line[3]);
        }

        public int Butanel { get; set; }
        public double Depth { get; set; }

        public int Ethanol { get; set; }
        public int Methane { get; set; }
        public int Pentane { get; set; }
        public int Propane { get; set; }
        public int TotalGas { get; set; }
    }

    public struct DescriptionLine
    {
        public DescriptionLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            var splitLine = line[1].Split("*/", StringSplitOptions.None);
            Description = splitLine[0];
            Boxed = Convert.ToBoolean(int.Parse(splitLine[1]));
            UnderlineRockType = Convert.ToBoolean(int.Parse(splitLine[2]));
            Unknown = splitLine[3];
            Italic = Convert.ToBoolean(int.Parse(splitLine[4]));
            Underlined = Convert.ToBoolean(int.Parse(splitLine[5]));
            Color = ColorTranslator.FromWin32(int.Parse(splitLine[6]));
            Font = splitLine[7];
            Bold = Convert.ToBoolean(int.Parse(splitLine[8]));
        }

        public bool Bold { get; set; }
        public bool Boxed { get; set; }
        public Color Color { get; set; }
        public double Depth { get; set; }

        public string Description { get; set; }
        public string Font { get; set; }
        public bool Italic { get; set; }
        public bool Underlined { get; set; }
        public bool UnderlineRockType { get; set; }

        public string Unknown { get; set; }
    }

    public struct DrillingParameterLine
    {
        public DrillingParameterLine(string[] line)
        {
            Depth = double.Parse(line[0]);
            ROP = int.Parse(line[1]);
            WOB = int.Parse(line[2]);
            PumpPressure = int.Parse(line[24]);
            SPM1 = int.Parse(line[25]);
            SPM2 = int.Parse(line[26]);
            FlowIn = int.Parse(line[27]);
            FlowOut = int.Parse(line[28]);
            GainLoss = int.Parse(line[29]);
            RPM = int.Parse(line[30]);
            Torque = int.Parse(line[31]);
            PitVolume = int.Parse(line[32]);
            SPM3 = int.Parse(line[33]);
            Aux1 = int.Parse(line[34]);
            Aux2 = int.Parse(line[35]);
            Aux3 = int.Parse(line[36]);
            Aux4 = int.Parse(line[37]);
            Aux5 = int.Parse(line[38]);
            Aux6 = int.Parse(line[39]);
        }

        public int Aux1 { get; set; }
        public int Aux2 { get; set; }
        public int Aux3 { get; set; }
        public int Aux4 { get; set; }
        public int Aux5 { get; set; }
        public int Aux6 { get; set; }
        public double Depth { get; set; }

        public int FlowIn { get; set; }
        public int FlowOut { get; set; }
        public int GainLoss { get; set; }
        public int PitVolume { get; set; }
        public int PumpPressure { get; set; }
        public int ROP { get; set; }

        public int RPM { get; set; }
        public int SPM1 { get; set; }
        public int SPM2 { get; set; }
        public int SPM3 { get; set; }
        public int Torque { get; set; }
        public int WOB { get; set; }
    }

    public struct NewBitLine
    {
        public NewBitLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            Number = int.Parse(line[1]);
            Size = line[2];
            Model = line[3];
            Comments = line[4];
        }

        public string Comments { get; set; }
        public double Depth { get; set; }

        public int expectedElements { get => 5; }
        public string Model { get; set; }
        public int Number { get; set; }

        public string Size { get; set; }
        public string ShowAll()
        {
            return $"{Depth}: Bit #{Number}; size {Size}; model {Model}.\nComments: \"{Comments}\"";
        }
    }

    public struct PlotScaleLine
    {
        public PlotScaleLine(string[] line)
        {
            Depth = double.Parse(line[0]);
            Unknown = int.Parse(line[1]);
            CuttingsGas = int.Parse(line[2]);
            DitchGas = int.Parse(line[3]);
            DrillRate = int.Parse(line[4]);
            FlowIn = int.Parse(line[5]);
            FlowOut = int.Parse(line[6]);
            GainLoss = int.Parse(line[7]);
            PitVolume = int.Parse(line[8]);
            PumpPressure = int.Parse(line[9]);
            RPM = int.Parse(line[10]);
            OilShows = int.Parse(line[11]);
            SPM = int.Parse(line[12]);
            Torque = int.Parse(line[13]);
            WeightOnBit = int.Parse(line[14]);
            Unknown2 = int.Parse(line[15]);
            CuttingsGasOverscale = int.Parse(line[16]);
            DitchGasOverscale = int.Parse(line[17]);
            DrillRateOverscale = int.Parse(line[18]);
            FlowInOverscale = int.Parse(line[19]);
            FlowOutOverscale = int.Parse(line[20]);
            GainLossOverscale = int.Parse(line[21]);
            PitVolumeOverscale = int.Parse(line[22]);
            PumpPressureOverscale = int.Parse(line[23]);
            RPMOverscale = int.Parse(line[24]);
            OilShowsOverscale = int.Parse(line[25]);
            SPMOverscale = int.Parse(line[26]);
            TorqueOverscale = int.Parse(line[27]);
            WeightOnBitOverscale = int.Parse(line[28]);
            Wetness = int.Parse(line[29]);
            WetnessOverscale = int.Parse(line[30]);
            Balance = int.Parse(line[31]);
            BalanceOverscale = int.Parse(line[32]);
            Character = int.Parse(line[33]);
            CharacterOverscale = int.Parse(line[34]);
            LinearChromatography = int.Parse(line[35]);
            LinearChromatographyOverscale = int.Parse(line[36]);
            Aux1 = int.Parse(line[37]);
            Aux1Overscale = int.Parse(line[38]);
            Aux2 = int.Parse(line[39]);
            Aux2Overscale = int.Parse(line[40]);
            Aux3 = int.Parse(line[41]);
            Aux3Overscale = int.Parse(line[42]);
            Aux4 = int.Parse(line[43]);
            Aux4Overscale = int.Parse(line[44]);
            Aux5 = int.Parse(line[45]);
            Aux5Overscale = int.Parse(line[46]);
            Aux6 = int.Parse(line[47]);
            Aux6Overscale = int.Parse(line[48]);
        }

        public int Aux1 { get; private set; }
        public int Aux1Overscale { get; private set; }
        public int Aux2 { get; private set; }
        public int Aux2Overscale { get; private set; }
        public int Aux3 { get; private set; }
        public int Aux3Overscale { get; private set; }
        public int Aux4 { get; private set; }
        public int Aux4Overscale { get; private set; }
        public int Aux5 { get; private set; }
        public int Aux5Overscale { get; private set; }
        public int Aux6 { get; private set; }
        public int Aux6Overscale { get; private set; }
        public int Balance { get; private set; }
        public int BalanceOverscale { get; private set; }
        public int Character { get; private set; }
        public int CharacterOverscale { get; private set; }
        public int CuttingsGas { get; private set; }
        public int CuttingsGasOverscale { get; private set; }
        public double Depth { get; private set; }

        public int DitchGas { get; private set; }
        public int DitchGasOverscale { get; private set; }
        public int DrillRate { get; private set; }
        public int DrillRateOverscale { get; private set; }
        public int FlowIn { get; private set; }
        public int FlowInOverscale { get; private set; }
        public int FlowOut { get; private set; }
        public int FlowOutOverscale { get; private set; }
        public int GainLoss { get; private set; }
        public int GainLossOverscale { get; private set; }
        public int LinearChromatography { get; private set; }
        public int LinearChromatographyOverscale { get; private set; }
        public int OilShows { get; private set; }
        public int OilShowsOverscale { get; private set; }
        public int PitVolume { get; private set; }
        public int PitVolumeOverscale { get; private set; }
        public int PumpPressure { get; private set; }
        public int PumpPressureOverscale { get; private set; }
        public int RPM { get; private set; }
        public int RPMOverscale { get; private set; }
        public int SPM { get; private set; }
        public int SPMOverscale { get; private set; }
        public int Torque { get; private set; }
        public int TorqueOverscale { get; private set; }
        public int Unknown { get; private set; }
        public int Unknown2 { get; private set; }
        public int WeightOnBit { get; private set; }
        public int WeightOnBitOverscale { get; private set; }
        public int Wetness { get; private set; }

        public int WetnessOverscale { get; private set; }
    }

    public struct LithologyLine
    {
        public double Depth { get; set; }

        public int Lithology1 { get; set; }

        public int Lithology2 { get; set; }

        public int Lithology3 { get; set; }

        public int Lithology4 { get; set; }

        public int Lithology5 { get; set; }

        public int Lithology6 { get; set; }

        public int Lithology7 { get; set; }

        public int Lithology8 { get; set; }

        public int Lithology9 { get; set; }

        public int Lithology10 { get; set; }

        public int? LithologyInterpreted { get; set; }

        public LithologyLine(List<string> line)
        {
            Depth = double.Parse(line[0]);
            Lithology1 = int.Parse(line[1]);
            Lithology2 = int.Parse(line[2]);
            Lithology3 = int.Parse(line[3]);
            Lithology4 = int.Parse(line[4]);
            Lithology5 = int.Parse(line[5]);
            Lithology6 = int.Parse(line[6]);
            Lithology7 = int.Parse(line[7]);
            Lithology8 = int.Parse(line[8]);
            Lithology9 = int.Parse(line[9]);
            if (line.Count == 12)
            {
                Lithology10 = int.Parse(line[10]);
                LithologyInterpreted = int.Parse(line[11]);
            }
            else
            {
                Lithology10 = int.Parse(line[10]);
                LithologyInterpreted = null;
            }

        }
    }
}

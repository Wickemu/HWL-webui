using System.Drawing;

namespace HwlFileAnalyzer.Columns
{
    public class ParameterColumn : IColumn
    {
        public Color BackgroundColor { get; set; } = Color.White;
        public List<DrillingParameter> Fields { get; set; }
        public string HeaderName { get; set; }
        public int Position { get; set; }
        public int fieldWidth => Width / Fields.Count;
        public int NumOfFields => Fields.Count;

        public int Width
        {
            get
            {
                if (Fields is null) return 7;
                else return 7 * Fields.Count;
            }
        }
        private List<string> headerItems;
        List<string> IColumn.HeaderItems
        {
            get
            {
                if (headerItems is null)
                {
                    headerItems = new List<string>();
                    foreach (var item in Fields)
                    {
                        headerItems.Add(item.DisplayName);
                    }
                    return headerItems;
                }
                else return headerItems;
            }
        }

        public void AddParameter(DrillingParameter dp)
        {
            if (Fields is null) Fields = new List<DrillingParameter>();
            if (!Fields.Contains(dp))
            {
                Fields.Add(dp);
                dp.Column = Position;
            }
        }

        public string GetPlotLine(double depth)
        {
            List<string> fieldList = new List<string>();
            foreach (var dp in Fields)
            {
                if (!dp.Data.ContainsKey(depth)) return "".PadRight(Width);
                Color textColor = dp.Color;
                System.Console.WriteLine(dp.Data[depth].ToString().PadRight(fieldWidth).Substring(0, fieldWidth));
                fieldList.Add(dp.Data[depth].ToString().PadRight(fieldWidth).Substring(0, fieldWidth));
            }

            return string.Join(string.Empty, fieldList);
        }

        public void PrintPlotLine(double depth)
        {
            foreach (var dp in Fields)
            {
                if (!dp.Data.ContainsKey(depth)) System.Console.WriteLine("".PadRight(Width));
                Color textColor = dp.Color;
                System.Console.Write(dp.Data[depth].ToString().PadRight(fieldWidth).Substring(0, fieldWidth));
            }
        }

        public void PrintHeaderItem(int i)
        {
            if (i < NumOfFields)
            {
                Color textColor = Fields[i].Color;
                Console.Write(Fields[i].DisplayName.PadRight(Width).Substring(0, Width));
            }
            else Console.Write("".PadRight(Width));
        }
    }
}

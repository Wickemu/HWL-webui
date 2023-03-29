namespace HwlFileAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using Console = Colorful.Console;
    public class ConsoleInterface
    {

        private HwlData hwl;
        private HwlPlotter plotter;

        public ConsoleInterface()
        {
            HwlFile = GetFile();
            Importer import = new Importer(HwlFile);
            hwl = import.ImportHWL();
            plotter = new HwlPlotter(hwl);
        }

        private string HwlFile { get; set; }

        public void Introduction()
        {
            Console.WriteAscii("Horizon Well Logging", Color.DarkOrange);
            ShowWellInfo();
        }

        public static void SetAppearance()
        {
            Console.SetWindowSize(200, 50);
            Console.Title = "HWL File Reader";
            Console.BackgroundColor = Color.LightGray;
            Console.ForegroundColor = Color.Black;
            Console.Clear();
        }

        public void GetDepths()
        {
            System.Console.WriteLine();
            Console.WriteLine("Top depth: " + hwl.Depths.Min() + "; Bottom depth: " + hwl.Depths.Max());
            System.Console.WriteLine();
            Console.Write("Pick a starting depth for parameters: ");
            var topper = Console.ReadLine().ToLower();
            if (topper == "top" || topper == "spud")
            {
                plotter.SelectedTop = hwl.TopDepth;
            }
            else plotter.SelectedTop = int.Parse(topper);
            Console.Write("Pick an ending depth: ");
            var botter = Console.ReadLine().ToLower();
            if (botter == "bottom" || botter == "md")
            {
                plotter.SelectedBottom = hwl.BottomDepth;
            }
            else plotter.SelectedBottom = int.Parse(botter);
            plotter.SetRange();
            System.Console.WriteLine();
        }

        public string GetFile()
        {
            while (true)
            {
                Console.WriteLine("Please specify the file path");
                string userInput = Console.ReadLine();
                if (userInput == null)
                {
                    Console.WriteLine("You did not provide any text.");
                    continue;
                }

                string givenPath = userInput.Trim('"');
                if (File.Exists(givenPath))
                {
                    if (Path.GetExtension(givenPath).ToLower() == ".hwl")
                    {
                        return givenPath;
                    }
                    else
                    {
                        Console.WriteLine("The given file is not a proper HWL file.");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("This is not a proper file path.");
                    continue;
                }
            }
        }
        public void Header()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("**************************************************");
            Console.WriteLine(hwl.WellInfo.HeaderTitle);
            Console.WriteLine(hwl.WellInfo.Company);
            System.Console.WriteLine();
            Console.WriteLine($"Logged by: ");
            foreach (var item in hwl.WellInfo.Loggers)
            {
                Console.Write(item + "  ");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("**************************************************");
            System.Console.WriteLine();
            System.Console.WriteLine($"Number of TVD items: {(hwl.TVDs is null ? 0 : hwl.TVDs.Count)}");
            System.Console.WriteLine($"Number of Casing Points: {(hwl.CasingPoints is null ? 0 : hwl.CasingPoints.Count)}");
            System.Console.WriteLine($"Number of New Bits: {(hwl.NewBits is null ? 0 : hwl.NewBits.Count)}");
            System.Console.WriteLine($"Number of Annotation: {hwl.Annotations.Count}");
            System.Console.WriteLine($"Number of Abbreviations: {hwl.Abbreviations.Count}");
            System.Console.WriteLine($"Number of LithSymbols: {hwl.LithSymbols.Count}");
            System.Console.WriteLine($"Top Depth: {hwl.TopDepth}; Bottom Depth: {hwl.BottomDepth}");
            System.Console.WriteLine($"Number of depths plotted: {hwl.Depths.Count}");
            System.Console.WriteLine($"Number of Oil plot scale lines: {hwl.PlotScales.Count}");
            System.Console.WriteLine($"Number of Descriptions: {hwl.Descriptions.Count}");
        }

        public void Menu()
        {
            while (true)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Which do you wanna do");
                System.Console.WriteLine($"1: View Plot");
                System.Console.WriteLine($"2: View Stats");
                System.Console.WriteLine($"3: View Descriptions");
                System.Console.WriteLine($"4: View Annotations");
                System.Console.WriteLine($"5: Well Info");
                System.Console.WriteLine($"6: Show Casings");
                System.Console.WriteLine($"7: Show New Bits");
                System.Console.WriteLine($"8: Do the thing");
                System.Console.WriteLine($"9: Test plot scales");
                System.Console.WriteLine($"0: Exit");
                System.Console.WriteLine();
                var input = Console.ReadLine();
                Console.Clear();
                switch (input)
                {
                    case "1":
                        GetDepths();
                        PickPlots();
                        System.Console.WriteLine($"Data plot of {hwl.WellInfo.HeaderTitle} from {plotter.SelectedTop} to {plotter.SelectedBottom}");
                        List<string[]> plot = plotter.GetPlot(hwl.PlotteDrillingParameters);
                        foreach (var line in plot)
                        {
                            foreach (var item in line)
                            {
                                Console.Write(item.PadRight(15));
                            }

                            Console.WriteLine();
                        }

                        break;
                    case "2":
                        GetDepths();
                        System.Console.WriteLine($"Drilling statistics of {hwl.WellInfo.HeaderTitle} from {plotter.SelectedTop} to {plotter.SelectedBottom}");
                        ShowCalcs();
                        break;
                    case "3":
                        System.Console.WriteLine($"Drilling descriptions of {hwl.WellInfo.HeaderTitle} from {plotter.SelectedTop} to {plotter.SelectedBottom}");

                        foreach (var item in hwl.Descriptions)
                        {
                            if (plotter.SelectedTop <= item.Depth && item.Depth <= plotter.SelectedBottom)
                            {
                                System.Console.WriteLine($"{item.Depth}: {item.Description}");
                            }
                        }

                        break;
                    case "4":
                        GetDepths();
                        System.Console.WriteLine($"Drilling annotations of {hwl.WellInfo.HeaderTitle} from {plotter.SelectedTop} to {plotter.SelectedBottom}");

                        foreach (var item in hwl.Annotations)
                        {
                            //if (Plotter.SelectedTop <= item.Depth && item.Depth <= Plotter.SelectedBottom)
                            {
                                if (item.Depth >= plotter.SelectedTop && item.Depth <= plotter.SelectedBottom)
                                    System.Console.WriteLine($"{item.Depth}: {item.Text}");
                            }
                        }

                        break;
                    case "5":
                        Header();
                        break;
                    case "6":
                        ShowCasings();
                        break;
                    case "7":
                        ShowNewBits();
                        break;
                    case "8":
                        GetDepths();
                        //plotter.SetMDDefaults();
                        plotter.GetMudLogLayout();
                        PickPlots();
                        plotter.PrintLineHeader();
                        foreach (var depth in plotter.SelectedRange)
                        {
                            if (depth % 5 == 0)
                            {
                                Console.Write("|");
                                plotter.PrintLine(depth);
                                System.Console.WriteLine();
                            }
                            else continue;
                        }
                        System.Console.WriteLine();
                        System.Console.WriteLine("Press any button to continue...");
                        Console.Read();
                        break;
                    case "9":
                        foreach (var item in hwl.DrillingParameters)
                        {
                            item.TestScales();
                        }
                        break;
                    case "0":
                        System.Console.WriteLine("Goodbye");
                        Environment.Exit(0);
                        break;

                }
            }

        }

        public void ShowCalcs()
        {
            var dpList = hwl.PlotteDrillingParameters;
            System.Console.WriteLine();
            Console.Write("Depth".PadRight(10));
            foreach (var item in dpList)
            {
                Console.Write(item.DisplayName.PadRight(16).Substring(0, 16) + " ");
            }

            System.Console.WriteLine();
            Console.Write("(ft)".PadRight(10));
            foreach (var item in dpList)
            {
                Console.Write($"({item.UnitOfMeasurement})".PadRight(17).Substring(0, 17));
            }

            System.Console.WriteLine();
            Console.Write("Min:".PadRight(10));
            foreach (var item in dpList)
            {
                string dataPoint = plotter.Min(plotter.GetDpPlot(item)).ToString();
                Console.Write(dataPoint.PadRight(17));
            }

            System.Console.WriteLine();
            Console.Write("Max:".PadRight(10));
            foreach (var item in dpList)
            {
                string dataPoint = plotter.Max(plotter.GetDpPlot(item)).ToString();
                Console.Write(dataPoint.PadRight(17));
            }

            System.Console.WriteLine();
            Console.Write("Avg:".PadRight(10));
            foreach (var item in dpList)
            {
                string dataPoint = Math.Round(plotter.GetDpPlot(item).Average(), 0).ToString();
                Console.Write(dataPoint.PadRight(17));
            }

            System.Console.WriteLine();
            Console.Write("Non0Avg:".PadRight(10));
            foreach (var item in dpList)
            {
                string dataPoint = plotter.No0Avg(plotter.GetDpPlot(item)).ToString();
                Console.Write(dataPoint.PadRight(17));
            }

            System.Console.WriteLine();
        }

        public void ShowCasings()
        {
            if (hwl.CasingPoints is null)
            {
                System.Console.WriteLine($"{hwl.WellInfo.HeaderTitle} contains no recorded casing points.");
                return;
            }
            foreach (var item in hwl.CasingPoints)
            {
                Console.Write($"{item.Depth.ToString()}: {item.Diameter} on {item.Date}");
                Console.WriteLine();
            }
        }

        public void ShowMenu()
        {
            SetAppearance();
            Introduction();
            Menu();
        }

        public void ShowNewBits()
        {
            if (hwl.NewBits is null)
            {
                System.Console.WriteLine($"{hwl.WellInfo.HeaderTitle} contains no recorded new bits.");
                return;
            }
            foreach (var item in hwl.NewBits)
            {
                Console.Write($"{item.Depth.ToString("#,###")}ft: ".PadRight(10));
                Console.Write($"bit #{item.Number}".PadRight(8));
                Console.Write($"({item.Model})".PadRight(15));
                Console.Write($"{item.Comments}");
                System.Console.WriteLine();
            }
        }

        public void ShowPlot()
        {
            //var startDepthPos = Plotter.SelectedTop - Hwl.Depths.Min() + 1;
            //var endDepthPos = Plotter.SelectedBottom - Hwl.Depths.Min() + 1;
            //Console.Write("Depth".PadRight(10));
            //foreach (var item in Hwl.DrillingParameters)
            //{
            //    Console.Write(item.DisplayName.PadRight(16).Substring(0, 16) + " ");
            //}

            System.Console.WriteLine();
            Console.Write("(ft)".PadRight(10));
            foreach (var item in hwl.DrillingParameters)
            {
                Console.Write($"({item.UnitOfMeasurement})".PadRight(17).Substring(0, 17));
            }

            Console.WriteLine();
            foreach (var i in plotter.SelectedRange)
            {
                string printedDepth = i.ToString();
                Console.Write(printedDepth.PadRight(10));
                foreach (var item in hwl.DrillingParameters)
                {
                    var printedString = item.Data[i].ToString();
                    Console.Write(printedString.PadRight(17));
                }

                Console.WriteLine();
            }
        }

        public void ShowWellInfo()
        {
            for (int i = 0; i < 130; i++) { Console.Write('*'); }
            Console.WriteLine();

            Console.Write("Well: ".PadLeft(15));
            Console.Write(hwl.WellInfo.HeaderTitle.ToUpper().PadRight(50));

            Console.Write("Contractor: ".PadLeft(15));
            Console.Write(hwl.WellInfo.Contractor.PadRight(15));

            Console.Write("Spud Date:".PadLeft(15));
            Console.Write(hwl.WellInfo.SpudDate.PadRight(20));

            Console.WriteLine();

            Console.Write("Company: ".PadLeft(15));
            Console.Write(hwl.WellInfo.Company.PadRight(50));

            Console.Write("Rig: ".PadLeft(15));
            Console.Write(hwl.WellInfo.Rig.PadRight(15));

            Console.Write("Logging Start:".PadLeft(15));
            Console.Write(hwl.WellInfo.StartDate.PadRight(20));

            Console.WriteLine();

            Console.Write("API: ".PadLeft(15));
            Console.Write(hwl.WellInfo.API.PadRight(50));

            Console.Write("Field: ".PadLeft(15));
            Console.Write(hwl.WellInfo.Field.PadRight(15));

            Console.Write("Logging End: ".PadLeft(15));
            Console.Write(hwl.WellInfo.EndDate.PadRight(20));

            Console.WriteLine();
            for (int i = 0; i < 130; i++) { Console.Write('*'); }
            Console.WriteLine();
        }

        private void PickPlots()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("By default, all fields which contain any non-zero data are enabled.");
            while (true)
            {
                System.Console.WriteLine("These fields are currently enabled:");
                System.Console.WriteLine();
                for (int i = 0; i < hwl.PlotteDrillingParameters.Count; i++)
                {
                    Console.Write($"{i}: {hwl.PlotteDrillingParameters[i].DisplayName}".PadRight(25).Substring(0, 25));
                    if (i % 5 == 0 && i != 0) System.Console.WriteLine();
                }
                System.Console.WriteLine();
                System.Console.WriteLine("Input the number of any field (one at a time) that you would like to disable.\nAlternatively, type 'next' to view disabled plots, 'none' to disable all plots, 'all' to enable all plots, or 'done' to go straight to the plots with no more changes.");
                System.Console.WriteLine();
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out int indexchoice))
                {
                    try
                    {
                        hwl.PlotteDrillingParameters[indexchoice].PlotEnabled = false;
                        continue;
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("Error: Not a valid entry");
                        continue;
                    }
                }
                else if (choice.ToLower() == "next") break;
                else if (choice.ToLower() == "done") return;
                else if (choice.ToLower() == "none")
                {
                    foreach (var item in hwl.DrillingParameters)
                    {
                        item.PlotEnabled = false;
                    }
                    break;
                }
                else continue;
            }
            while (true)
            {
                System.Console.WriteLine();
                System.Console.WriteLine("These fields are currently disabled:");
                System.Console.WriteLine();
                for (int i = 0; i < hwl.UnplottedDrillingParameters.Count; i++)
                {
                    Console.Write($"{i}: {hwl.UnplottedDrillingParameters[i].DisplayName}".PadRight(25).Substring(0, 25));
                    if (i % 5 == 0 && i != 0) System.Console.WriteLine();
                }
                System.Console.WriteLine();
                System.Console.WriteLine("Input the number of any field (one at a time) that you would like to enable.\nAlternatively, type 'none' to disable all plots, 'all' to enable all plots, or 'done' to go straight to the plots with no more changes.");
                System.Console.WriteLine();
                var choice = Console.ReadLine();
                if (int.TryParse(choice, out int indexchoice))
                {
                    try
                    {
                        hwl.UnplottedDrillingParameters[indexchoice].PlotEnabled = true;
                        continue;
                    }
                    catch (Exception)
                    {
                        System.Console.WriteLine("Error: Not a valid entry");
                        continue;
                    }
                }
                else if (choice.ToLower() == "next") break;
                else continue;
            }
        }
    }
}
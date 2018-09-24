/*Recreation of previous C++ program to read data for millikan's experiment from a text file and calculate mean, standard deviation
 and standard error for charge on an electron. Any erroneous data is discarded*/
//Main Lesson: Reading data from a file and validating it


namespace Millikan
{
    class Program
    {
        static void Main()
        {
            //Main program
            string[] ContinueOptions = new string[] { "Y", "N" };
            bool ContinueProgram = true;
            while (ContinueProgram)
            {
                bool FileNameValid = false;
                string FileName = "";
                while (!FileNameValid)
                {
                    System.Console.Write("Enter a filename: ");
                    FileName = System.Console.ReadLine();
                    if (FileHandler.FileExists(FileName))
                    {
                        break;
                    }
                    System.Console.WriteLine("Invalid filename");
                }
                string[] FileContents = FileHandler.ReadFileToArray(FileName);
                double[] Values = new double[0];
                //For every line from the file, check it is a double and if so, expand the double array and put it in
                foreach(string Line in FileContents)
                {
                    if (IsDouble(Line))
                    {
                        System.Array.Resize<double>(ref Values, Values.Length + 1);
                        Values[Values.Length - 1] = double.Parse(Line);
                    }
                }
                string Output = "No usable data";
                //Only work on arrays with something in
                if (Values.Length > 0)
                {
                    Output = "-----" + FileName + "-----\n\n" + "Mean: " + StatsCalculator.CalculateMean(Values).ToString() + "\nStandard Deivation: " +
                        StatsCalculator.CalculateStdDev(Values).ToString() + "\nStandard Error: " + StatsCalculator.CalculateStdErr(Values).ToString();
                }
                System.Console.WriteLine(Output);
                string ContinueChoice = TakeStringInput(ContinueOptions, "Restart program? (Y/N): ");
                if (ContinueChoice.ToLower() == "n")
                {
                    ContinueProgram = true;
                }
                System.Console.Clear();
            }
        }

        //Function to check a string is a double
        private static bool IsDouble(string StringToCheck)
        {
            return double.TryParse(StringToCheck, out double Result);
        }
        //Function to validate input with list of input options - use for choosing whether to exit
        private static string TakeStringInput(string[] Options, string CustomMessage)
        {
            string InputString = "InputString";
            string LoweredString = "LoweredString";
            bool GettingInput = true;
            while (GettingInput)
            {
                System.Console.Write(CustomMessage);
                InputString = System.Console.ReadLine();
                LoweredString = InputString.ToLower();
                bool ValidInputFound = false;
                foreach (string Option in Options)
                {
                    if (LoweredString == Option.ToLower())
                    {
                        //Valid option found
                        ValidInputFound = true;
                        break;
                    }
                }
                if (ValidInputFound)
                {
                    break;
                }
                //Else no matching input found, so give an error message and try again
                System.Console.WriteLine("Invalid input, please input one of the following:");
                foreach (string Option in Options)
                {
                    System.Console.WriteLine(Option);
                }
            }
            return InputString;
        }
    }

    class FileHandler
    {
        /*STATIC FUNCTIONS*/

        //Function to check file exists
        public static bool FileExists(string FileName)
        {
            if (System.IO.File.Exists(FileName))
            {
                return true;
            }
            return false;
        }

        //Function to calculate file length
        public static int GetFileLength(string FileName)
        {
            return System.IO.File.ReadAllLines(FileName).Length;
        }

        //Function to read each line in a file to an array
        public static string[] ReadFileToArray(string FileName)
        {
            return System.IO.File.ReadAllLines(FileName);
        }
    }

    class StatsCalculator
    {
        /*STATIC FUNCTIONS*/

        //Function to calculate mean
        public static double CalculateMean(double[] ValueArray)
        {
            double Sum = 0;
            foreach(double Value in ValueArray)
            {
                Sum += Value;
            }
            return Sum / ValueArray.Length;
        }

        //Function to calculate standard deviation
        public static double CalculateStdDev(double[] ValueArray)
        {
            //Prevent division by 0
            if (ValueArray.Length == 0)
            {
                return 0.0;
            }

            double Mean = CalculateMean(ValueArray);
            double TempSquareSum = 0;
            foreach(double Value in ValueArray)
            {
                TempSquareSum += System.Math.Pow((Value - Mean), 2);
            }
            return System.Math.Sqrt(TempSquareSum / (ValueArray.Length - 1));
        }

        //Function to calculate standard error
        public static double CalculateStdErr(double[] ValueArray)
        {
            return CalculateStdDev(ValueArray) / System.Math.Sqrt(ValueArray.Length);
        }
    }
}

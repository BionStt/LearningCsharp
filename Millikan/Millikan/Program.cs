/*Recreation of previous C++ program to read data for millikan's experiment from a text file and calculate mean, standard deviation
 and standard error for charge on an electron. Any erroneous data is discarded*/


namespace Millikan
{
    class Program
    {
        static void Main()
        {
            System.Console.Write("Please enter a file path: ");
            string Result = "File does not exist";
            string FilePath = System.Console.ReadLine();
            if (FileHandler.FileExists(FilePath))
            {
                Result = "File exists";
            }
            System.Console.WriteLine(Result);
            if (FileHandler.FileExists(FilePath))
            {
                string Header = "-----" + FilePath + "-----\n" + "Line count: " + FileHandler.GetFileLength(FilePath).ToString() + "\n";
                System.Console.WriteLine(Header);
                string[] FileContents = FileHandler.ReadFileToArray(FilePath);
                //Use foreach to print every line in the array
                foreach(string line in FileContents)
                {
                    System.Console.WriteLine(line);
                }
            }
            System.Console.Read();
        }

        //Function to check a string is a double

        //Function to validate input with list of input options

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

        //Function to calculate standard deviation

        //Function to calculate standard error
    }
}

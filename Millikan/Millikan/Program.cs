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
                System.Console.WriteLine(System.IO.File.ReadAllText(FilePath));
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

        //Function to open user file

        //Function to calculate file length

        //Function to read each line in a file to an array
    }

    class StatsCalculator
    {
        /*STATIC FUNCTIONS*/

        //Function to calculate mean

        //Function to calculate standard deviation

        //Function to calculate standard error
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpLearningApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello, world!");
            System.DateTime CurrentTime = System.DateTime.Now;
            System.String MonthName = System.DateTime.Now.ToString("MMMM");
            System.String OutputString = "Today is " + CurrentTime.DayOfWeek + ", " + CurrentTime.Day.ToString() + " of " + MonthName + ", " + CurrentTime.Year.ToString()
                + "\n" + "The current time is " + CurrentTime.Hour.ToString() + ":" + CurrentTime.Minute.ToString();
            System.Console.WriteLine(OutputString);
            System.Console.ReadLine();  //Prevent closing of window
        }
    }
}

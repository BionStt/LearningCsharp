/*Recreation of previous c++ code for a complex number class*/
//Main Lesson: Operator overloads, input/output overloads (eg Tostring()) overload, regex for user input
namespace ComplexNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexNumber CN1 = new ComplexNumber();
            ComplexNumber CN2 = new ComplexNumber(12.678, -4);
            ComplexNumber CN3 = new ComplexNumber(-6, 6.2);
            System.Console.WriteLine(CN1);
            System.Console.WriteLine(CN2);
            System.Console.WriteLine(CN3);

            //Test regex
            string CNRegex = @"^-?[0-9]+(\.[0-9]+)?(i\s?$)?([\+\-][0-9]+(\.[0-9]+)?i)?\s?$";
            while (true)
            {
                System.Console.Write("Please enter a complex number or x/X to exit: ");
                string UserInput = System.Console.ReadLine();
                if (UserInput == "x" || UserInput == "X")
                {
                    break;
                }
                System.Text.RegularExpressions.Match CNMatch = System.Text.RegularExpressions.Regex.Match(UserInput, CNRegex);
                if (CNMatch.Success)
                {
                    System.Console.WriteLine("Correct!");
                }
                else
                {
                    System.Console.WriteLine("False!");
                }
            }
            System.Console.ReadLine();
        }
    }

    class ComplexNumber
    {
        //Variables
        public double X { get; set; }
        public double Y { get; set; }

        //Constructors
        public ComplexNumber()
        {
            X = 0;
            Y = 0;
        }
        public ComplexNumber(double Xin, double Yin)
        {
            X = Xin;
            Y = Yin;
        }

        //Mathematical overrides

        //I/O overrides
        public override string ToString()
        {
            string Joiner = "+";
            if (Y < 0)
            {
                Joiner = "";
            }
            return X.ToString() + Joiner + Y.ToString() + "i";
        }

        public void TakeInput(string UserInput)
        {

        }
    }
}

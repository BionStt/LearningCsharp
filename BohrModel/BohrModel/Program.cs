//Recreation of Bohr model calculator from previous C++ code
//Main Lesson: Handling and validating user input and outputting to console

namespace BohrModel
{
    class Program
    {
        private static int TakePositiveIntegerInput(string VariableName)
        {
            bool GettingInput = true;
            int InputValue = 0;
            while (GettingInput)
            {
                System.Console.Write("Please enter an integer value for " + VariableName + ": ");
                if (int.TryParse(System.Console.ReadLine(), out InputValue))
                {
                    if (InputValue > 0)
                    {
                        break;
                    }
                }
                System.Console.WriteLine("Invalid input, please try again");
            }
            return InputValue;
        }

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
                foreach(string Option in Options)
                {
                    if(LoweredString == Option.ToLower())
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
                foreach(string Option in Options)
                {
                    System.Console.WriteLine(Option);
                }
            }
            return InputString;
        }

        private static double CalculateEnergy(int n1, int n2, double z, string unit)
        {
            double EnergyDifference = 13.6 * (System.Math.Pow(z, 2)) * ((1 / (System.Math.Pow(n2, 2)) - (1 / (System.Math.Pow(n1, 2)))));
            if (unit.ToLower() == "j")
            {
                EnergyDifference *= 1.6e-19;
            }
            return EnergyDifference;
        }

        static void Main()
        {
            string[] UnitList = new string[] { "eV", "J" };
            string[] ContinueOptions = new string[] { "Y", "N" };
            //Intro message displayed on startup
            string IntroMessage = "Welcome to the bohr model energy calculator\n" +
                "You can calculate the energy of a photon produced by the transition of\n" +
                "an electron from initial energy level n1 down to final energy level n2.\n" +
                "You may specify a proton number Z for the atom and choose to display the\n" +
                "result in eV or J.";
            //While user chooses to not close the program
            bool CloseProgram = false;
            while (!CloseProgram)
            {
                System.Console.WriteLine(IntroMessage);
                //Take input of variables
                int z = TakePositiveIntegerInput("z");
                int n1 = 1;
                int n2 = 1;
                //n1 must be greater than n2
                bool ComparingN = true;
                while (ComparingN)
                {
                    n1 = TakePositiveIntegerInput("n1");
                    n2 = TakePositiveIntegerInput("n2");
                    if (n1 > n2)
                    {
                        break;
                    }
                    System.Console.WriteLine("n1 must be larger than n2");
                }
                //At this point, z, n1 and n2 are valid. Take input of units
                string Unit = TakeStringInput(UnitList, "Please choose the units (eV or J): ");
                double EnergyDifference = CalculateEnergy(n1, n2, z, Unit);
                //Format units for displaying
                string PrintedUnits = "error";
                switch (Unit.ToLower())
                {
                    case "ev":
                        PrintedUnits = "eV";
                        break;
                    case "j":
                        //With 3 decimal places precision, most answers would come out as 0J, so make it a bit more useful
                        EnergyDifference *= 1e19;
                        PrintedUnits = "x e-19 J";
                        break;
                }
                string ResultsMessage = "For a proton number of " + z.ToString() + " the photon energy from transition between levels " + n1.ToString() + " and " + n2.ToString() + " is:\n" +
                    System.Math.Round(EnergyDifference, 3).ToString() + " " + PrintedUnits;
                System.Console.WriteLine(ResultsMessage);
                string ProgramContinueChoice = TakeStringInput(ContinueOptions, "Would you like to restart the program? (Y/N): ");
                if (ProgramContinueChoice.ToLower() == "n")
                {
                    CloseProgram = true;
                }
                System.Console.Clear();
            }
        }
    }
}
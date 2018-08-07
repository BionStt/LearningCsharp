//Recreation of Bohr model calculator from previous C++ code

namespace BohrModel
{
    class Program
    {
        private static int TakeIntegerInput(string VariableName)
        {
            bool GettingInput = true;
            int InputValue = 0;
            while (GettingInput)
            {
                System.Console.Write("Please enter an integer value for " + VariableName + ": ");
                if (int.TryParse(System.Console.ReadLine(), out InputValue))
                {
                    break;
                }
                System.Console.WriteLine("Invalid input, please try again");
            }
            return InputValue;
        }
        static void Main()
        {
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
                int z = TakeIntegerInput("z");
                int n1;
                int n2;
                //n1 must be greater than n2
                bool ComparingN = true;
                while (ComparingN)
                {
                    n1 = TakeIntegerInput("n1");
                    n2 = TakeIntegerInput("n2");
                    if (n1 > n2)
                    {
                        break;
                    }
                    System.Console.WriteLine("n1 must be larger than n2");
                }
                CloseProgram = true;    //TEST
            }
            System.Console.ReadLine();  //TEST
        }
    }
}
/*Recreation of physics course register from previous c++ code. Instead of course code and name, the input will be
 film name and year of release*/
 //Main Lesson: Handling user input, use of containers and sorting said containers
 //Bonus: add an option to write database to a text file to practice file writing

 //Plan: Use a list<T> of tuples of int and string

namespace FilmRegister
{
    class Program
    {
        private static readonly int MinimumYear = 1950;
        static void Main(string[] args)
        {
            /*Menu: -Add films, -View films, -Exit. 
             View films: filter by decade, then sort by name or by year
             */
            System.Collections.Generic.List<System.Tuple<int, string>> FilmList = new System.Collections.Generic.List<System.Tuple<int, string>> { };
            bool ContinueProgram = true;
            string[] MenuOptions = new string[] { "a", "v", "x" };
            string[] DecadeFilterOptions = GenerateDecadeList();
            string[] SortOptions = new string[] { "y", "n", "s" };
            string MenuMessage = "----MENU----\nWelcome to the Film register.\nEnter (a) to add films, (v) to view stored films or (x) to exit: ";
            string AddFilmsMessage = "Enter film year (" + MinimumYear.ToString() + " - " + System.DateTime.Now.Year.ToString() + ") and film name or (x) to exit.";
            string DecadeFilterMessage = "Enter decade to filter films by or (a) to view all films: ";
            string SortMessage = "Enter (y) to sort by year, (n) to sort by name or (s) to skip: ";
            while (ContinueProgram)
            {
                //Menu screen
                string MenuChoice = TakeStringInput(MenuOptions, MenuMessage);
                switch (MenuChoice)
                {
                    case "x":
                        ContinueProgram = false;
                        break;
                    case "a":
                        bool ContinueAddingFilms = true;
                        System.Console.WriteLine(AddFilmsMessage);
                        while (ContinueAddingFilms)
                        {
                            System.Console.Write("Input: ");
                            string UserInput = System.Console.ReadLine();
                            if (UserInput == "x" || UserInput == "X")
                            {
                                ContinueAddingFilms = false;
                            }
                            else
                            {
                                if(!ValidateFilmInput(ref FilmList, UserInput, out string ErrorMsg))
                                {
                                    System.Console.WriteLine(ErrorMsg);
                                }
                            }
                        }
                        break;
                    case "v":
                        System.Collections.Generic.List<System.Tuple<int, string>> AdaptedList = new System.Collections.Generic.List<System.Tuple<int, string>>(FilmList);
                        string FilterChoice = TakeStringInput(DecadeFilterOptions, DecadeFilterMessage);
                        if (FilterChoice != "a")
                        {
                            AdaptedList = new System.Collections.Generic.List<System.Tuple<int, string>>(FilterByDecade(ref FilmList, int.Parse(FilterChoice)));
                        }
                        string SortChoice = TakeStringInput(SortOptions, SortMessage);
                        switch (SortChoice)
                        {
                            case "y":
                                SortByYear(ref AdaptedList);
                                break;
                            case "n":
                                SortByName(ref AdaptedList);
                                break;
                            case "s":
                                break;
                        }
                        PrintFilmList(ref AdaptedList);
                        break;
                }
            }

        }
        //Function to print a film list with adaptive title bar size
        private static void PrintFilmList(ref System.Collections.Generic.List<System.Tuple<int, string>> FilmList)
        {
            if(FilmList.Count <= 0)
            {
                System.Console.WriteLine("No films available.");
                return;
            }
            //Calculate max number of dashes to go under year and name titles
            System.Collections.Generic.List<System.Tuple<int, string>> CopiedList = new System.Collections.Generic.List<System.Tuple<int, string>>(FilmList);
            CopiedList.Sort((i, j) => j.Item2.Length.CompareTo(i.Item2.Length));
            int NumberOfDashes = CopiedList[0].Item2.Length + 8;
            if (NumberOfDashes < 12)
            {
                NumberOfDashes = 12;
            }
            System.Console.WriteLine("Year    Name");
            string DashedLine = "";
            for(int i = 0; i < NumberOfDashes; i++)
            {
                DashedLine += "-";
            }
            System.Console.WriteLine(DashedLine);
            foreach (System.Tuple<int, string> Element in FilmList)
            {
                System.Console.WriteLine("{0}    {1}", Element.Item1.ToString(), Element.Item2);
            }
        }

        //Function to take user input of YEAR FILMNAME - return true if input valid and appended, else return false
        private static bool ValidateFilmInput(ref System.Collections.Generic.List<System.Tuple<int,string>> FilmList, string UserInput, out string ErrorMsg)
        {
            ErrorMsg = "No error";
            //FilmList.Add(System.Tuple.Create(4, "Four"));
            if(UserInput.Length <= 0)
            {
                ErrorMsg = "No input given.";
                return false;
            }
            //Find first space (separator of year and name)
            if(UserInput.IndexOf(" ") < 0)
            {
                ErrorMsg = "No separator given";
                return false;
            }
            //Pick out year and film name
            string YearString = UserInput.Substring(0, UserInput.IndexOf(" "));
            string NameString = UserInput.Substring(UserInput.IndexOf(" ") + 1);
            //Check year is int, convert it and validate it
            if (!int.TryParse(YearString, out int YearInt))
            {
                ErrorMsg = "Year not integer";
                return false;
            }
            if (!ValidateYear(YearInt))
            {
                ErrorMsg = "Year not in range";
                return false;
            }
            //Check film name was there
            if (NameString.Length <= 0)
            {
                ErrorMsg = "No film name given";
                return false;
            }
            //See if film name already exists in list
            if (FilmList.Exists(Film => Film.Item2 == NameString))
            {
                //Find all the elements with matching name and check their years
                System.Collections.Generic.List<System.Tuple<int, string>> MatchingFilms = FilmList.FindAll(Film => Film.Item2 == NameString);
                foreach(System.Tuple<int, string> Film in MatchingFilms)
                {
                    if (Film.Item1 == YearInt)
                    {
                        ErrorMsg = "Film name already in list";
                        return false;
                    }
                }
            }
            //Add year and film name to list
            FilmList.Add(System.Tuple.Create(YearInt, NameString));
            return true;
        }

        //Function to validate year
        private static bool ValidateYear(int YearInput)
        {
            //Only allow years between minimum year and now
            if (YearInput < MinimumYear || YearInput > System.DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }
        //Function to filter by decade - decade input must be between minimum and maximum decade and divisible by 10
        private static System.Collections.Generic.List<System.Tuple<int, string>> FilterByDecade(ref System.Collections.Generic.List<System.Tuple<int, string>> FilmList, int DecadeIn)
        {
            System.Collections.Generic.List<System.Tuple<int, string>> FilteredList = new System.Collections.Generic.List<System.Tuple<int, string>> { };
            foreach(System.Tuple<int, string> Film in FilmList)
            {
                //Eg decade: 1970's, film year: 1975, remainder is 5 so it is added to filtered list
                if (Film.Item1 - DecadeIn >= 0 && Film.Item1 - DecadeIn < 10)
                {
                    FilteredList.Add(Film);
                }
            }
            return FilteredList;
        }

        //Function to sort by year - will act on filtered list
        private static void SortByYear(ref System.Collections.Generic.List<System.Tuple<int, string>> FilteredList)
        {
            FilteredList.Sort((i, j) => i.Item1.CompareTo(j.Item1));
        }

        //Function to sort by name
        private static void SortByName(ref System.Collections.Generic.List<System.Tuple<int, string>> FilteredList)
        {
            FilteredList.Sort((i, j) => i.Item2.CompareTo(j.Item2));
        }

        //Function to generate decade list
        private static string[] GenerateDecadeList()
        {
            System.Collections.Generic.List<string> DecadeList = new System.Collections.Generic.List<string> { };
            int Decade = MinimumYear - (MinimumYear % 10);
            while (true)
            {
                if (Decade > System.DateTime.Now.Year)
                {
                    break;
                }
                DecadeList.Add(Decade.ToString());
                Decade += 10;
            }
            DecadeList.Add("a");
            return DecadeList.ToArray();
        }

        //Function to print film list to a file

        //Function to validate input with list of input options
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
}

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
            //TEST
            System.Collections.Generic.List<System.Tuple<int, string>> FilmList = new System.Collections.Generic.List<System.Tuple<int, string>> { };
            while (true)
            {
                System.Console.WriteLine("Enter Year (1950-2018) and Name or x to exit.");  //Make this reponsive
                System.Console.Write("Input: ");
                string UserInput = System.Console.ReadLine();
                if(UserInput == "x")
                {
                    break;
                }
                if(!ValidateFilmInput(ref FilmList,UserInput,out string ErrorMsg))
                {
                    System.Console.WriteLine(ErrorMsg);
                }
            }
            //Filter list by 1990s decade
            System.Collections.Generic.List<System.Tuple<int, string>> FilteredList = FilterByDecade(ref FilmList, 1990);
            //Print list of films
            System.Console.WriteLine("Unfiltered:");
            System.Console.WriteLine("Year   Name\n------------------");
            foreach(System.Tuple<int, string> Element in FilmList)
            {
                System.Console.WriteLine("{0}   {1}", Element.Item1.ToString(), Element.Item2);
            }
            System.Console.WriteLine("\nFiltered:");
            System.Console.WriteLine("Year   Name\n------------------");
            foreach (System.Tuple<int, string> Element in FilteredList)
            {
                System.Console.WriteLine("{0}   {1}", Element.Item1.ToString(), Element.Item2);
            }
            //Sort lists by name and year
            FilteredList = FilmList;
            SortByName(ref FilteredList);
            System.Console.WriteLine("\nSorted by name:");
            System.Console.WriteLine("Year   Name\n------------------");
            foreach (System.Tuple<int, string> Element in FilteredList)
            {
                System.Console.WriteLine("{0}   {1}", Element.Item1.ToString(), Element.Item2);
            }
            SortByYear(ref FilteredList);
            System.Console.WriteLine("\nSorted by year:");
            System.Console.WriteLine("Year   Name\n------------------");
            foreach (System.Tuple<int, string> Element in FilteredList)
            {
                System.Console.WriteLine("{0}   {1}", Element.Item1.ToString(), Element.Item2);
            }
            //END TEST
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

        //Function to print film list to a file
    }
}

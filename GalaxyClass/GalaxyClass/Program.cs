/*Recreation of previous C++ program that utilises classes to store and display information about galaxies and their satellites*/
//Main Lesson: Basic class encapsulation, exception handling
namespace GalaxyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Collections.Generic.List<Galaxy> GalaxyList = new System.Collections.Generic.List<Galaxy> { };
            try
            {
                Galaxy Galaxy_1 = new Galaxy(Galaxy.HubbleType.SBa, 2, 1e7, 0.01);
                Galaxy_1.AddSatellite(Galaxy.HubbleType.Irr, 9, 1e7, 0.02);

                Galaxy Galaxy_2 = new Galaxy(Galaxy.HubbleType.E2, 4, 2.4e8, 0.043);

                Galaxy Galaxy_3 = new Galaxy();

                GalaxyList.Add(Galaxy_1);
                GalaxyList.Add(Galaxy_2);
                GalaxyList.Add(Galaxy_3);

                foreach(Galaxy Gxy in GalaxyList)
                {
                    Gxy.Print();
                }

                System.Console.WriteLine("Changing Galaxy_2's hubble type.");
                GalaxyList[1].GalaxyHubbleType = Galaxy.HubbleType.E3;

                foreach (Galaxy Gxy in GalaxyList)
                {
                    Gxy.Print();
                }
            }
            catch(System.Exception ErrMessage)
            {
                System.Console.WriteLine(ErrMessage.ToString());
            }
        }
    }

    class Galaxy
    {
        public enum HubbleType { E0, E1, E2, E3, E4, E5, E6, E7, S0, Sa, Sb, Sc, SBa, SBb, SBc, Irr };
        private static readonly System.Collections.Generic.List<string> HubbleTypeStrings = 
            new System.Collections.Generic.List<string>
            { "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "S0", "Sa", "Sb", "Sc", "SBa", "SBb", "SBc", "Irr" };
        private static readonly double SolarMass = 1.98855e30;

        private double Redshift, TotalMass, MassFraction;
        private System.Collections.Generic.List<Galaxy> Satellites;
        public HubbleType GalaxyHubbleType { get; set; }    //Auto property

        //Default constructor
        public Galaxy()
        {
            GalaxyHubbleType = HubbleType.E0;
            Redshift = 0;
            TotalMass = 1e7;
            MassFraction = 0;
            Satellites = new System.Collections.Generic.List<Galaxy> { };
        }

        //Parameterised constructor
        public Galaxy(HubbleType HubbleTypeIn, double RedShiftIn, double TotalMassIn, double MassFractionIn)
        {
            if (RedShiftIn < 0 || RedShiftIn > 10)
            {
                throw new System.Exception("Redshift not in range");
            }
            if (TotalMassIn < 1e7 || TotalMassIn > 1e12)
            {
                throw new System.Exception("Total mass not in range");
            }
            if (MassFractionIn < 0 || MassFractionIn > 0.05)
            {
                throw new System.Exception("Mass fraction not in range");
            }
            GalaxyHubbleType = HubbleTypeIn;
            Redshift = RedShiftIn;
            TotalMass = TotalMassIn * SolarMass;
            MassFraction = MassFractionIn;
            Satellites = new System.Collections.Generic.List<Galaxy> { };
        }

        public double GetStellarMass()
        {
            return TotalMass * MassFraction;
        }

        public void AddSatellite(HubbleType HubbleTypeIn, double RedShiftIn, double TotalMassIn, double MassFractionIn)
        {
            Satellites.Add(new Galaxy(HubbleTypeIn, RedShiftIn, TotalMassIn, MassFractionIn));
        }

        public void Print()
        {
            System.Console.WriteLine("Hubble type: " + HubbleTypeStrings[(int)GalaxyHubbleType]);
            System.Console.WriteLine("Redshift: " + Redshift.ToString());
            System.Console.WriteLine("Total mass: " + TotalMass.ToString() + "kg");
            System.Console.WriteLine("Stellar mass fraction: " + MassFraction.ToString());
            System.Console.WriteLine("Stellar mass content: " + GetStellarMass().ToString() + "kg");
            System.Console.WriteLine(Satellites.Count.ToString() + " satellite(s)\n");

            int SatelliteCount = 1;
            foreach (Galaxy Satellite in Satellites)
            {
                System.Console.WriteLine("Satellite " + SatelliteCount.ToString() + " data:\n");
                Satellite.Print();
            }
        }
    }
}

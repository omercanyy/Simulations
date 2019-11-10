using System;

namespace SimulatorLib
{
    /// <summary>
    /// Simulates how a virus epidemic kill cities in a country
    /// </summary>
    public class ExtinctionSimulator: ISimulator
    {
        public int[] Country { get; private set; }

        public int YearsPassed { get; private set; }

        public ExtinctionSimulator(int[] countries)
        {
            if (countries == null || countries.Length == 0)
            {
                throw new ArgumentException("Country cannot be empty or null!");
            }

            this.Country = countries;
            YearsPassed = 0;
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        public void Run()
        {
            PrintCurrentState();
            while (IsAnyoneAlive())
            {
                IterateOneYear();
                PrintCurrentState();
            }
        }

        /// <summary>
        /// Iterates simulation by one year
        /// </summary>
        public void IterateOneYear()
        {
            var nextYearsCountry = new int[Country.Length];
            for (int i = 0; i < Country.Length; i++)
            {
                if (isNeighborToVirus(i))
                {
                    nextYearsCountry[i] = Country[i] / 2;
                }
                else
                {
                    nextYearsCountry[i] = Country[i];
                }
            }
            Country = nextYearsCountry;
            YearsPassed++;
        }

        /// <summary>
        /// Checks if there are still people alive in the country
        /// </summary>
        public bool IsAnyoneAlive()
        {
            foreach(var city in Country)
            {
                if (city > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void PrintCurrentState()
        {
            Console.WriteLine($"year {YearsPassed}: {string.Join(", ", Country)}");
            if (!IsAnyoneAlive())
            {
                Console.WriteLine("__!!!EXTINCT!!!__");
            }
        }

        //Checks (by city's index) if a city has contacted the virus
        private bool isNeighborToVirus(int i)
        {
            if (i == 0)
                return Country[i + 1] == 0;
            if (i == Country.Length - 1)
                return Country[i - 1] == 0;
            return Country[i - 1] == 0 || Country[i + 1] == 0;
        }
    }
}

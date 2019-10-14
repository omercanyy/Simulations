using System;

namespace Simulators
{
    /// <summary>
    /// Simulates how a virus epidemic kill cities in a country
    /// </summary>
    public class ExtinctionSimulator: ISimulator
    {
        private int[] _country;

        private int _yearsPassed;

        public ExtinctionSimulator(int[] countries)
        {
            this._country = countries;
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        public void Run()
        {
            //TODO: Implement proper formating for pretty-print
            _yearsPassed = 0;
            Console.WriteLine($"year {_yearsPassed}: {string.Join(", ", _country)}");
            while (IsAnyoneAlive())
            {
                IterateOneYear();
                _yearsPassed++;
                Console.WriteLine($"year {_yearsPassed}: {string.Join(", ", _country)}");
            }
            Console.WriteLine("__!!!EXTINCT!!!__");
        }

        /// <summary>
        /// Iterates simulation by one year
        /// </summary>
        public void IterateOneYear()
        {
            for (int i = 0; i < _country.Length; i++)
            {
                //skip if city is already dead
                if (_country[i] == 0)
                {
                    continue;
                }

                if (isNeighborToVirus(i))
                {
                    _country[i] = _country[i] / 2;
                }
            }
        }

        /// <summary>
        /// Get how many years passed since simulation started
        /// </summary>
        public int GetYearsPassed()
        {
            return _yearsPassed;
        }

        /// <summary>
        /// Checks if there are still people alive in the country
        /// </summary>
        public bool IsAnyoneAlive()
        {
            foreach(var city in _country)
            {
                if (city > 0)
                {
                    return true;
                }
            }
            return false;
        }

        //Checks (by city's index) if a city has contacted the virus
        private bool isNeighborToVirus(int i)
        {
            if (i == 0)
                return _country[i + 1] == 0;
            if (i == _country.Length - 1)
                return _country[i - 1] == 0;
            return _country[i - 1] == 0 || _country[i + 1] == 0;
        }
    }
}

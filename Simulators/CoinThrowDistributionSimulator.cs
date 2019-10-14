using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulators
{
    /// <summary>
    /// Simulates distribution of given number of coins being thrown for a given number of times
    /// </summary>
    public class CoinThrowDistributionSimulator : ISimulator
    {
        private int _numberOfCoins;

        private int _numberOfThrows;

        private Random _random;

        private readonly string _head = "Head";

        private readonly string _tail = "Tail";

        private readonly char _separator = '|';

        public CoinThrowDistributionSimulator(int NumberOfCoins, int NumberOfThrows)
        {
            this._numberOfCoins = NumberOfCoins;
            this._numberOfThrows = NumberOfThrows;
            this._random = new Random();
        }

        /// <summary>
        /// Runs the simulation
        /// </summary>
        public void Run()
        {
            var throwResults = executeAllThrows();
            var outcomeDistribution = calcOutcomeDistribution(throwResults);
            graphOutcomeDistribution(outcomeDistribution);
        }
        
        //Executes all throws one by one
        private List<List<string>> executeAllThrows()
        {
            var throwResults = new List<List<string>>();
            for(int i = 0; i < _numberOfThrows; i++)
            {
                throwResults.Add(throwCoinsOnce());
            }
            return throwResults;
        }

        //Calcs throw outcome distribution
        private List<Tuple<string, int>> calcOutcomeDistribution(List<List<string>> throwResults)
        {
            var throwResultFrequencies = new List<string>();
            foreach (var throwResult in throwResults)
            {
                var freq = getFrequencyTable(throwResult);
                var headCount = (freq.ContainsKey(_head)) ? freq[_head] : 0;
                var tailCount = (freq.ContainsKey(_tail)) ? freq[_tail] : 0;
                throwResultFrequencies.Add($"{headCount}{_separator}{tailCount}");
            }
            var outcomeDistribution = getFrequencyTable(throwResultFrequencies);
            return outcomeDistribution
                .OrderBy(q => Int32.Parse(q.Key.Split(_separator)[0]))
                .ThenByDescending(q => Int32.Parse(q.Key.Split(_separator)[1]))
                .Select(q => new Tuple<string, int> (q.Key, q.Value))
                .ToList();
        }

        //Graphs outcome distribution
        private void graphOutcomeDistribution(List<Tuple<string, int>> outcomeDistribution)
        {
            var paddingSize = $"{_head}: {_numberOfCoins} Tail:{_numberOfCoins}".Length;
            foreach(var outcomeFreqTuple in outcomeDistribution)
            {
                var head = outcomeFreqTuple.Item1.Split(_separator)[0].PadLeft(("" + _numberOfCoins).Length);
                var tail = outcomeFreqTuple.Item1.Split(_separator)[1].PadLeft(("" + _numberOfCoins).Length);
                var result = $"Head:{head} Tail:{tail}".PadRight(paddingSize);
                var bar = string.Concat(Enumerable.Repeat("x", outcomeFreqTuple.Item2/5));
                Console.WriteLine($"{result}=> {bar}");
            }
        }

        //Throw all the coins one-time
        private List<string> throwCoinsOnce()
        {
            var outcomeSpace = new string[] { _head, _tail };
            var throwResult = new List<string>();
            for(int i = 0; i < _numberOfCoins; i++)
            {

                throwResult.Add(outcomeSpace[_random.Next(0, outcomeSpace.Length)]);
            }
            return throwResult;
        }

        //Calcs freq table
        private Dictionary<string, int> getFrequencyTable(List<string> list)
        {
            var freqTable = new Dictionary<string, int>();
            foreach(var item in list)
            {
                if (!freqTable.ContainsKey(item))
                    freqTable[item] = 0;
                freqTable[item]++;
            }
            return freqTable;
        }
    }
}

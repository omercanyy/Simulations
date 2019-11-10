using System;
using SimulatorLib;

namespace SimulatorTool
{
    internal enum Simulators
    {
        ExtinctionSimulator,
        CoinThrowDistributionSimulator,//TODO: Add unit tests for edge cases
        RabbitsBreedSimulator/*out of order*/ //TODO: Fix
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var firstArg = args.Length > 0 ? args[0] : null;
            firstArg = "ExtinctionSimulator";
            var secondArg = args.Length > 1 ? args[1] : null;
            secondArg = @"C:\Users\izmir\Desktop\demo.csv";
            var thirdArg = args.Length > 1 ? args[2] : null;

            if (Enum.TryParse(firstArg, out Simulators simulatorName))
            {
                Console.WriteLine($"Simulating {simulatorName} {secondArg} {thirdArg}");
                switch (simulatorName)
                {
                    case Simulators.ExtinctionSimulator:
                        var extinctionSimulator = new ExtinctionSimulator(Helpers.ReadIntArrayFromFile(secondArg));
                        extinctionSimulator.Run();
                        break;
                    case Simulators.CoinThrowDistributionSimulator:
                        var coinThrowDistributionSimulator = new CoinThrowDistributionSimulator(Int32.Parse(secondArg), Int32.Parse(thirdArg));
                        coinThrowDistributionSimulator.Run();
                        break;
                    default:
                        Console.WriteLine("Module you are looking may not be active in the current release!");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Couldn't find the simulator! Check the spelling or check back later.");
                return;
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

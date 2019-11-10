using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimulatorLib;
using System;
using System.Collections.Generic;

namespace SimulatorLibTests
{
    [TestClass]
    public class ExtinctionSimulatorTests
    {
        /// <summary>
        /// Increases yearsPassed by one and halfs the number of people in a city neighbour to a zero-city
        /// </summary>
        [TestMethod]
        public void IterateOneYearTest1()
        {
            var input = new int[] { 1, 0, 1};
            var expected = new int[] { 0, 0, 0 };
            var simulator = new ExtinctionSimulator(input);

            simulator.IterateOneYear();
            var actual = simulator.Country;

            Assert.AreEqual(1, simulator.YearsPassed);
            Assert.IsTrue(AssertHelper.CompareArrays(expected, actual));
        }

        /// <summary>
        /// Increases yearsPassed by one and halfs the number of people in a city neighbour to a zero-city
        /// </summary>
        [TestMethod]
        public void IterateOneYearTest2()
        {
            var input = new int[] { 2, 1, 0, 1, 2 };
            var expected = new int[] { 1, 0, 0, 0, 1 };
            var simulator = new ExtinctionSimulator(input);

            simulator.IterateOneYear();
            simulator.IterateOneYear();
            var actual = simulator.Country;

            Assert.AreEqual(2, simulator.YearsPassed);
            Assert.IsTrue(AssertHelper.CompareArrays(expected, actual));
        }

        /// <summary>
        /// If city is all zeros, YearsPassed does not change, country does not change
        /// </summary>
        [TestMethod]
        public void ExtinctionSimulatorRunTest1()
        {
            var input = new int[] { 0, 0, 0, 0 };
            var expected = new int [] { 0, 0, 0, 0 };
            var simulator = new ExtinctionSimulator(input);

            simulator.Run();
            var actual = simulator.Country;

            Assert.AreEqual(0, simulator.YearsPassed);
            Assert.IsTrue(AssertHelper.CompareArrays(expected, actual));
        }

        /// <summary>
        /// If city is null throws ArgumentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExtinctionSimulatorTest2()
        {
            var simulator = new ExtinctionSimulator(null);
        }

        /// <summary>
        /// If city is empty throws ArgumentException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExtinctionSimulatorTest3()
        {
            var simulator = new ExtinctionSimulator(new int[] { });
        }

        /// <summary>
        /// If country array has at least one non zero returns true otherwise returns false
        /// </summary>
        [TestMethod]
        public void ExtinctionSimulatorIsAnyoneAliveTest1()
        {
            var inputs = new List<int[]>
            {
                new int[] { 1, 5, 2, 5, 7, 4, 2, 1 },
                new int[] { 1, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 1 },
                new int[] { 0, 0, 0, 0, 1, 0, 0, 0 },
                new int[] { 0, 1, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 1, 0 }
            };
            
            foreach(var input in inputs)
            {
                var simulator = new ExtinctionSimulator(input);
                Assert.IsTrue(simulator.IsAnyoneAlive());
            }
        }

        /// <summary>
        /// If country array has a non zero return true otherwise returns false
        /// </summary>
        [TestMethod]
        public void ExtinctionSimulatorIsAnyoneAliveTest2()
        {
            var inputs = new List<int[]>
            {
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0 }
            };

            foreach (var input in inputs)
            {
                var simulator = new ExtinctionSimulator(input);
                Assert.IsFalse(simulator.IsAnyoneAlive());
            }
        }
    }
}

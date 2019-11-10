using System;
using System.Collections.Generic;
using System.Text;

namespace SimulatorLibTests
{
    public class AssertHelper
    {
        public static bool CompareArrays<T>(T[] expected, T[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; i++)
            {
                if (!expected[i].Equals(actual[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

using System.Collections.Generic;
using NUnit.Framework;

namespace Casino.Tests.DSL
{
    public static class AssertCurrentBets
    {
        public static void AreEqual(int[] expectedValues, IList<Bet> actualValues)
        {
            for (var i = 0; i < expectedValues.Length; i++)
            {
                Assert.AreEqual(expectedValues[i], actualValues[i].Number);
            }
        }
    }
}
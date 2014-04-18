using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitCalculate
    {
        [TestMethod]
        public void Test_Calculate_PossibleResolutions1()
        {
            Calculate calculate = new Calculate();

            Dictionary<Resolution, int> actual = calculate.PossibleResolutions(new Bitmap(11, 7), 3, 5);

            Dictionary<Resolution, int> expected = new Dictionary<Resolution, int>();
            int startWidth = 3;
            int startHeight = 2;

            for (int i = 2; i < 5 + 2; i++)
                expected.Add(new Resolution(i * startWidth, i * startHeight), i);

            Assert.IsTrue(expected.Count == actual.Count);

            foreach (Resolution key in expected.Keys)
            {

                Resolution resol = new Resolution(key.Width, key.Height);
                int expectedValue;
                expected.TryGetValue(resol, out expectedValue);

                int actualValue;
                actual.TryGetValue(key, out actualValue);

                Assert.AreEqual(expectedValue, actualValue);
            }

        }
    }
}

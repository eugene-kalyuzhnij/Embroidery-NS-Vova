using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitCalculate
    {

        private string DictionaryToString(Dictionary<Resolution, int> dictionary)
        {
            String resultString = "";
            foreach (var key in dictionary.Keys)
            {
                resultString += key.ToString() + ":";
                resultString += dictionary[key].ToString() + ";";
            }

            return resultString;
        }

        [TestMethod]
        public void Test_Calculate_PossibleResolutions_Count()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();

            Dictionary<Resolution, int> actual = calculate.PossibleResolutions(new Bitmap(11, 7), 3, 5);

            Dictionary<Resolution, int> expected = new Dictionary<Resolution, int>();
            int startWidth = 3;
            int startHeight = 2;

            for (int i = 2; i < 5 + 2; i++)
                expected.Add(new Resolution(i * startWidth, i * startHeight), i);

            Assert.IsTrue(expected.Count == actual.Count);

            Assert.AreEqual(DictionaryToString(expected), DictionaryToString(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Calculate_PossibleResolutions_CountException1()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(10, 3), 0, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Calculate_PossibleResolutions_CountException2()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(10, 3), 3, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Calculate_PossibleResolutions_CountException3()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(2, 3), 3, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Calculate_PossibleResolutions_CountException4()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(11, 2), 3, 0);
        }

        [TestMethod]
        public void Test_Calculate_PossibleResolutions_Coefficient()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();

            Dictionary<Resolution, int> actual = calculate.PossibleResolutions(new Bitmap(11, 7), 3, 3, 5);

            Dictionary<Resolution, int> expected = new Dictionary<Resolution, int>();
            int startWidth = 3;
            int startHeight = 2;

            for (int i = 3; i <= 5; i++)
                expected.Add(new Resolution(i * startWidth, i * startHeight), i);

            Assert.IsTrue(expected.Count == actual.Count);

            Assert.AreEqual(DictionaryToString(expected), DictionaryToString(actual));
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Calculate_PossibleResolutions_CoefficientException1()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(1, 1), -1, 3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Calculate_PossibleResolutions_CoefficientException2()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(1, 1), 2, 3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Calculate_PossibleResolutions_CoefficientException3()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(11, 2), 3, 3, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_Calculate_PossibleResolutions_CoefficientException4()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(11, 7), 3, 1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_Calculate_PossibleResolutions_CoefficientException5()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(11, 7), 3, 4, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Test_Calculate_PossibleResolutions_CoefficientException6()
        {
            EmbroideryCreator calculate = new EmbroideryCreator();
            calculate.PossibleResolutions(new Bitmap(11, 7), 3, 4, 3);
        }


    }
}

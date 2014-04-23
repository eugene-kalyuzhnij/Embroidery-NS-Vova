﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitPalette
    {
        [TestMethod]
        public void Test_Palette_GetAllColors()
        {

            Color[] expected = new Color[] { Color.Red, Color.Green, Color.Blue };

            Palette palette = new Palette(expected);

            Color[] actual = palette.GetAllColors();

            Assert.IsTrue(actual.Length == expected.Length);

            for (int i = 0; i < actual.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Palette_GetAllColorException()
        {
            Palette palette = new Palette();
            palette.GetAllColors();
        }


    }
}
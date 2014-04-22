using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;
using Moq;
using Moq.Properties;
using NSEmbroidery.Core.Decorators;
using NSEmbroidery.Core.Interfaces;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitSettings
    {
        [TestMethod]
        public void Test_Settings_CreateColorSymbolRelation()
        {
            Settings settings = new Settings();
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Green });
            settings.Symbols = new char[] { '1', '2', '3', '4' };

            settings.CreateColorSymbolRelation();

            Dictionary<Color, char> expected = new Dictionary<Color, char>();
            expected.Add(Color.Red, '1');
            expected.Add(Color.Green, '2');

            Assert.IsTrue(expected.Count == settings.ColorSymbolRelation.Count);

            foreach (var key in expected.Keys)
                Assert.AreEqual(expected[key], settings.ColorSymbolRelation[key]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_Settings_CreateColorSymbolRelationException1()
        {
            Settings settings = new Settings();
            settings.Palette = null;

            settings.CreateColorSymbolRelation();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_Settings_CreateColorSymbolRelationException2()
        {
            Settings settings = new Settings();
            settings.Palette = new Palette();

            settings.CreateColorSymbolRelation();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_Settings_CreateColorSymbolRelationException3()
        {
            Settings settings = new Settings();
            settings.Palette = new Palette(new Color[]{Color.Red});

            settings.CreateColorSymbolRelation();
        }

        [TestMethod]
        [ExpectedException(typeof(WrongSymbolsRealisationException))]
        public void Test_Settings_CreateColorSymbolRelationException4()
        {
            Settings settings = new Settings();
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Black });
            settings.Symbols = new char[] { '1' };

            settings.CreateColorSymbolRelation();
        }

        [TestMethod]
        public void Test_Settings_Decorate()
        {
            Settings settings = new Settings();

            var mock = new Mock<IDecoratorsComposition>();
            mock.Setup(dec => dec.Decorate(It.IsAny<Canvas>(), It.IsAny<Canvas>(), settings));

            settings.DecoratorsComposition = mock.Object;
            settings.Decorate(new Canvas(1, 1), new Canvas(2, 2));

            mock.Verify(d => d.Decorate(It.IsAny<Canvas>(), It.IsAny<Canvas>(), settings));
        }

    }
}

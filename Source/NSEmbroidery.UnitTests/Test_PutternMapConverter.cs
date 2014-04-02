using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class Test_PutternMapConverter
    {
        [TestMethod]
        public void TestMethod_Generate()
        {
            PutternMapGenerator mapGenerator = new PutternMapGenerator();

            mapGenerator.Palette = new Palette(new Color[] { Color.Red, Color.Blue, Color.Green });
            Settings.SquareCount = 2;

            Canvas can = new Canvas(new Resolution(4, 4));

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    can.SetColor(i, j, Color.FromArgb(100, 250, 90));

            Canvas actual = mapGenerator.Generate(can);


            Canvas expectedCanvas = new Canvas(new Resolution(2, 2));
            expectedCanvas.SetColor(0, 0, Color.Green);
            expectedCanvas.SetColor(0 ,1, Color.Green);
            expectedCanvas.SetColor(1 ,0, Color.Green);
            expectedCanvas.SetColor(1, 1, Color.Green);


            Assert.IsTrue(expectedCanvas.Count == actual.Count);

            IEnumerator<Color> e1 = expectedCanvas.GetEnumerator();
            IEnumerator<Color> e2 = actual.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
                Assert.AreEqual(e1.Current, e2.Current);

        }

    }

}

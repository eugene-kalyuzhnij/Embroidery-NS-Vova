using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class Test_SquaresDecorator
    {
        [TestMethod]
        public void TestMethod_Decorate()
        {
            IDecorator decorator = new SquaresDecorator();

            Canvas canvas = new Canvas(new Resolution(2, 2));

            canvas.SetColor(0, 0, Color.FromArgb(255, 0, 0));
            canvas.SetColor(1, 0, Color.FromArgb(255, 0, 0));
            canvas.SetColor(0, 1, Color.FromArgb(0, 255, 0));
            canvas.SetColor(1, 1, Color.FromArgb(0, 255, 0));

            Canvas expected = new Canvas(new Resolution(4, 4));
            

            Color color = Color.FromArgb(255, 0, 0);
            

            for (int i = 0; i < expected.Height; i++)
            {
                if (i == 2) color = Color.FromArgb(0, 255, 0);

                for (int j = 0; j < expected.Width; j++)
                    expected.SetColor(j, i, color);
            }
               

            Canvas actual = new Canvas(new Resolution(4, 4));
            decorator.Decorate(actual, canvas);

            Assert.IsTrue(expected.Count == actual.Count);

            IEnumerator<Color> e1 = expected.GetEnumerator();
            IEnumerator<Color> e2 = actual.GetEnumerator();

            while (e1.MoveNext() && e2.MoveNext())
                Assert.AreEqual(e1.Current, e2.Current);

        }
    }
}

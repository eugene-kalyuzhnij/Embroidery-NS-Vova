using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitCanvas
    {
        [TestMethod]
        public void TestMethod_GetEnumerator()
        {
            Canvas canvas = new Canvas(new Resolution(4, 3));
            #region Filling canvas
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 4; x++)
                {
                    if (x == 1 && y == 1) canvas.SetColor(x, y, Color.Red);
                    else if (x == 2 && y == 0) canvas.SetColor(x, y, Color.White);
                    else canvas.SetColor(x, y, Color.Black);
                }
            #endregion

            Color[] actualColors = new Color[4 * 3];

            int i = 0;
            foreach (Color color in canvas)
                actualColors[i++] = color;


            i = 0;
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 4; x++)
                    Assert.IsTrue(actualColors[i++] == canvas.GetColor(x, y));
        }

        [TestMethod]
        public void TestMethod_GetInnerCanvas()
        {
            Canvas canvas = new Canvas(new Resolution(10, 8));
            #region Filling canvas
            for(int y = 0; y < 8; y++)
                for (int x = 0; x < 10; x++)
                {
                    if (x == 5 && y == 2) canvas.SetColor(x, y, Color.Red);
                    else canvas.SetColor(x, y, Color.Black);
                }
            #endregion

            Canvas expectedCanvas = new Canvas(new Resolution(3, 4));
            #region Filling canvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (x == 0 && y == 0) expectedCanvas.SetColor(x, y, Color.Red);
                    else expectedCanvas.SetColor(x, y, Color.Black);
                }
            #endregion


            Canvas actual = canvas.GetInnerCanvas(5, 2, new Resolution(3, 4));

            Assert.IsTrue(actual.Width == expectedCanvas.Width && actual.Height == expectedCanvas.Height);

            for (int y = 0; y < actual.Height; y++)
                for (int x = 0; x < actual.Width; x++)
                    Assert.IsTrue(actual.GetColor(x, y) == expectedCanvas.GetColor(x, y));

        }


        [TestMethod]
        public void TestMethod_SetInnerCanvas()
        {

        }

    }
}

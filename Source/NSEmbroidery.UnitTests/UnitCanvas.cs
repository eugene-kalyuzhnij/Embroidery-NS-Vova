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
        public void Test_Canvas_GetEnumerator()
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
        public void Test_Canvas_GetInnerCanvas()
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
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Canvas_GetInnerCanvasException()
        {
            Canvas canvas = new Canvas(new Resolution(10, 8));
            #region Filling canvas
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 10; x++)
                {
                    if (x == 5 && y == 2) canvas.SetColor(x, y, Color.Red);
                    else canvas.SetColor(x, y, Color.Black);
                }
            #endregion


            Canvas actual = canvas.GetInnerCanvas(5, 2, new Resolution(10, 4));
        }

        [TestMethod]
        public void Test_Canvas_SetInnerCanvas()
        {
            Canvas canvas = new Canvas(new Resolution(10, 8));
            #region Filling canvas
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 10; x++)
                    canvas.SetColor(x, y, Color.Black);
            #endregion

            Canvas expected = new Canvas(new Resolution(10, 8));
            #region Filling expected canvas
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 10; x++)
                {
                    if (x == 5 && y == 2) expected.SetColor(x, y, Color.Red);
                    else if (x == 8 && y == 4) expected.SetColor(x, y, Color.Green);
                    else expected.SetColor(x, y, Color.Black);
                }
            #endregion


            Canvas inputCanvas = new Canvas(new Resolution(4, 3));
            #region Filling input canvas
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 4; x++)
                {
                    if (x == 0 && y == 0) inputCanvas.SetColor(x, y, Color.Red);
                    else if (x == 3 && y == 2) inputCanvas.SetColor(x, y, Color.Green);
                    else inputCanvas.SetColor(x, y, Color.Black);
                }
            #endregion


            canvas.SetCanvas(5, 2, inputCanvas);

            Assert.IsTrue(canvas.Width == expected.Width && canvas.Height == expected.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expected.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }

        }

        [TestMethod]
        [ExpectedException(typeof(WrongResolutionException))]
        public void Test_Canvas_setInnerCanvasException()
        {
            Canvas canvas = new Canvas(new Resolution(10, 8));
            #region Filling canvas
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 10; x++)
                    canvas.SetColor(x, y, Color.Black);
            #endregion

            Canvas inputCanvas = new Canvas(new Resolution(7, 3));
            #region Filling input canvas
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 7; x++)
                {
                    if (x == 0 && y == 0) inputCanvas.SetColor(x, y, Color.Red);
                    else if (x == 3 && y == 2) inputCanvas.SetColor(x, y, Color.Green);
                    else inputCanvas.SetColor(x, y, Color.Black);
                }
            #endregion


            canvas.SetCanvas(5, 2, inputCanvas);

        }

        [TestMethod]
        public void Test_Canvas_ReduceResolution()
        {
            Canvas canvas = new Canvas(new Resolution(8, 5));
            #region Filling canvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                     if((x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 5 && y == 1))
                        canvas.SetColor(x, y, Color.Green);
                    else if ((x == 0 && y == 2) ||
                            (x == 1 && y == 2) ||
                            (x == 2 && y == 2) ||
                            (x == 3 && y == 2) ||
                            (x == 2 && y == 3))
                        canvas.SetColor(x, y, Color.Blue);
                    else canvas.SetColor(x, y, Color.Red);
                }
            #endregion

            Canvas expectedCanvas = new Canvas(new Resolution(3, 2));
            #region Filling expected canvas
            for(int y = 0; y < 2; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (x == 2 && y == 0)
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 63, 96, 0));
                    else if (x == 1 && y == 1)
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 63, 0, 191));
                    else if (x == 0 && y == 1)
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 127, 0, 127));
                    else
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 255, 0, 0));
                }
            #endregion


            Canvas actualCanvas = canvas.ReduceResolution(3, 2, 2);

            Assert.IsTrue(actualCanvas.Width == expectedCanvas.Width && actualCanvas.Height == expectedCanvas.Height);

            for (int y = 0; y < actualCanvas.Height; y++)
                for (int x = 0; x < actualCanvas.Width; x++)
                {
                    Color actualColor = actualCanvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }

        }

    }
}

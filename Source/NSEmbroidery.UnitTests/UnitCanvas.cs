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
        public void Test_Canvas_SetInnerCanvasException()
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

        [TestMethod]
        public void Test_Canvas_GetResolution()
        {
            Canvas canvas = new Canvas(8, 6);

            Resolution expectedResolution = new Resolution(8, 6);

            Assert.AreEqual(expectedResolution.Width, canvas.GetResolution().Width);
            Assert.AreEqual(expectedResolution.Height, canvas.GetResolution().Height);
        }

        [TestMethod]
        public void Test_Canvas_GetColor()
        {
            Canvas canvas = new Canvas(4, 2);
            canvas.Color[0, 3] = Color.Black;
            
            Assert.AreEqual(Color.Black, canvas.GetColor(3, 0));
        }

        [TestMethod]
        public void Test_Canvas_SetBorderRight()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(3, 0, Color.Black);
            expectedCanvas.SetColor(3, 1, Color.Black);
            expectedCanvas.SetColor(3, 2, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Right, GridType.SolidLine);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }

        }

        [TestMethod]
        public void Test_Canvas_SetColor()
        {
            Canvas canvas = new Canvas(4, 2);

            canvas.SetColor(3, 0, Color.Red);

            Assert.AreEqual(Color.Red, canvas.Color[0, 3]);
        }

        [TestMethod]
        public void Test_Canvas_SetBorderLeft()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(0, 0, Color.Black);
            expectedCanvas.SetColor(0, 1, Color.Black);
            expectedCanvas.SetColor(0, 2, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Left, GridType.SolidLine);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        public void Test_Canvas_SetBorderButtom()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(0, 2, Color.Black);
            expectedCanvas.SetColor(1, 2, Color.Black);
            expectedCanvas.SetColor(2, 2, Color.Black);
            expectedCanvas.SetColor(3, 2, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Buttom, GridType.SolidLine);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        public void Test_Canvas_SetBorderTop()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(0, 0, Color.Black);
            expectedCanvas.SetColor(1, 0, Color.Black);
            expectedCanvas.SetColor(2, 0, Color.Black);
            expectedCanvas.SetColor(3, 0, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Top, GridType.SolidLine);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        public void Test_Canvas_SetBorderPointsTop()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(0, 0, Color.Black);
            expectedCanvas.SetColor(3, 0, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Top, GridType.Points);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        public void Test_Canvas_SetBorderPointsButtom()
        {
            Canvas canvas = new Canvas(4, 3);

            Canvas expectedCanvas = new Canvas(4, 3);
            expectedCanvas.SetColor(0, 2, Color.Black);
            expectedCanvas.SetColor(3, 2, Color.Black);

            canvas.SetBorder(0, 0, 4, 3, Color.Black, Aligns.Buttom, GridType.Points);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        public void Test_Canvas_SetBorderPointsRight()
        {
            Canvas canvas = new Canvas(4, 4);

            Canvas expectedCanvas = new Canvas(4, 4);
            expectedCanvas.SetColor(3, 0, Color.Black);
            expectedCanvas.SetColor(3, 3, Color.Black);

            canvas.SetBorder(0, 0, 4, 4, Color.Black, Aligns.Right, GridType.Points);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }

        }

        [TestMethod]
        public void Test_Canvas_SetBorderPointsLeft()
        {
            Canvas canvas = new Canvas(4, 4);

            Canvas expectedCanvas = new Canvas(4, 4);
            expectedCanvas.SetColor(0, 0, Color.Black);
            expectedCanvas.SetColor(0, 3, Color.Black);

            canvas.SetBorder(0, 0, 4, 4, Color.Black, Aligns.Left, GridType.Points);


            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_Constructor1Exception()
        {
            Resolution resol = null;
            Canvas canvas = new Canvas(resol);
        }

        [TestMethod]
        public void Test_Canvas_Constructor1()
        {
            Canvas canvas = new Canvas(new Resolution(3, 2));

            Assert.AreEqual(canvas.Width, 3);
            Assert.AreEqual(canvas.Height, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_Constructor2Exception()
        {
            Canvas canvas = new Canvas(-1, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_Constructor2Exception2()
        {
            Canvas canvas = new Canvas(1, 0);
        }

        [TestMethod]
        public void Test_Canvas_Constructor2()
        {
            Canvas canvas = new Canvas(3, 2);

            Assert.AreEqual(canvas.Width, 3);
            Assert.AreEqual(canvas.Height, 2);
        }

        [TestMethod]
        public void Test_Canvas_AverageColor()
        {
            Canvas canvas = new Canvas(1, 1);

            Color expectedColor = Color.FromArgb(255, 127, 0, 127);

            Color actualColor = canvas.AverageColor(new Color[] { Color.Red, Color.Blue });

            Assert.AreEqual(expectedColor, actualColor);
        }

        [TestMethod]
        public void Test_Canvas_SetSymbol()
        {
            Bitmap temp = new Bitmap(4, 4);

            Graphics g = Graphics.FromImage(temp);


            Font font = new Font("Arial", 4 - 1, GraphicsUnit.Pixel);
            string sym = "#";
            sym = sym.PadLeft(1, '#');

            SolidBrush brush = new SolidBrush(Color.Black);

            g.DrawString(sym, font, brush, new PointF(0f, 0f));

            CanvasConverter converter = new CanvasConverter();
            Canvas expectedCanvas = converter.ConvertBitmapToCanvas(temp);


            Canvas canvas = new Canvas(4, 4);
            canvas.SetSymbol('#', 0, 0, 4, Color.Black);

            Assert.IsTrue(canvas.Width == expectedCanvas.Width && canvas.Height == expectedCanvas.Height);

            for (int y = 0; y < canvas.Height; y++)
                for (int x = 0; x < canvas.Width; x++)
                {
                    Color actualColor = canvas.GetColor(x, y);
                    Color expectedColor = expectedCanvas.GetColor(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_SetSymbolException1()
        {
            Canvas canvas = new Canvas(4, 4);
            canvas.SetSymbol('#', 0, 0, -4, Color.Black);
        }


        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_SetSymbolException2()
        {
            Canvas canvas = new Canvas(4, 4);
            canvas.SetSymbol('#', 0, -1, 4, Color.Black);
        }


        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_Canvas_SetSymbolException3()
        {
            Canvas canvas = new Canvas(4, 4);
            canvas.SetSymbol('#', -1, 0, 4, Color.Black);
        }

    }
}

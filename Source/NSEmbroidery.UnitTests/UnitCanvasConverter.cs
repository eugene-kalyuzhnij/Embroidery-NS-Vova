using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitCanvasConverter
    {
        [TestMethod]
        public void Test_CanvasConverter_ConvertBitmapToCanvas()
        {
            Bitmap inputImage = new Bitmap(6, 4);
            #region Filling input image
            for(int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 5 && y == 1))
                        inputImage.SetPixel(x, y, Color.White);
                    else if ((x == 1 && y == 1) ||
                            (x == 5 && y == 3))
                        inputImage.SetPixel(x, y, Color.Blue);
                    else if ((x == 3 && y == 1) ||
                            (x == 2 && y == 2))
                        inputImage.SetPixel(x, y, Color.Green);
                    else inputImage.SetPixel(x, y, Color.Black);
                }
            #endregion

            Canvas expectedCanvas = new Canvas(6, 4);
            #region Filling expected canvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 5 && y == 1))
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 255, 255, 255));
                    else if ((x == 1 && y == 1) ||
                            (x == 5 && y == 3))
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 0, 0, 255));
                    else if ((x == 3 && y == 1) ||
                            (x == 2 && y == 2))
                        expectedCanvas.SetColor(x, y, Color.FromArgb(255, 0, 128, 0));
                    else expectedCanvas.SetColor(x, y, Color.FromArgb(255, 0, 0, 0));
                }
            #endregion

            CanvasConverter converter = new CanvasConverter();
            Canvas actual = converter.ConvertBitmapToCanvas(inputImage);

            Assert.IsTrue(expectedCanvas.Width == actual.Width);
            Assert.IsTrue(expectedCanvas.Height == actual.Height);

            for (int y = 0; y < expectedCanvas.Height; y++)
                for (int x = 0; x < expectedCanvas.Width; x++)
                    Assert.AreEqual(expectedCanvas.GetColor(x, y), actual.GetColor(x, y));
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_CanvasConverter_ConvertBitmapToCanvasException()
        {
            Bitmap image = null;
            CanvasConverter converter = new CanvasConverter();
            converter.ConvertBitmapToCanvas(image);
        }


        [TestMethod]
        public void Test_CanvasConverter_ConvertCanvasToBitmap()
        {
            Canvas inputCanvas = new Canvas(7, 3);
            #region Filling input canvas
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 7; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 5 && y == 1))
                        inputCanvas.SetColor(x, y, Color.White);
                    else if ((x == 1 && y == 1) ||
                             (x == 6 && y == 2))
                        inputCanvas.SetColor(x, y, Color.Blue);
                    else if ((x == 3 && y == 1) ||
                             (x == 2 && y == 2))
                        inputCanvas.SetColor(x, y, Color.Green);
                    else inputCanvas.SetColor(x, y, Color.Black);
                }
            #endregion

            Bitmap expectedImage = new Bitmap(7, 3);
            #region Filling expected image
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 7; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 5 && y == 1))
                        expectedImage.SetPixel(x, y, Color.FromArgb(255, 255, 255, 255));
                    else if ((x == 1 && y == 1) ||
                             (x == 6 && y == 2))
                        expectedImage.SetPixel(x, y, Color.FromArgb(255, 0, 0, 255));
                    else if ((x == 3 && y == 1) ||
                            (x == 2 && y == 2))
                        expectedImage.SetPixel(x, y, Color.FromArgb(255, 0, 128, 0));
                    else expectedImage.SetPixel(x, y, Color.FromArgb(255, 0, 0, 0));
                }
            #endregion


            CanvasConverter converter = new CanvasConverter();
            Bitmap actual = converter.ConvertCanvasToBitmap(inputCanvas);

            Assert.AreEqual(expectedImage.Width, actual.Width);
            Assert.AreEqual(expectedImage.Height, actual.Height);

            for (int y = 0; y < expectedImage.Height; y++)
                for (int x = 0; x < expectedImage.Width; x++)
                    Assert.AreEqual(expectedImage.GetPixel(x, y), actual.GetPixel(x, y));

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_CanvasConverter_ConvertCanvasToBitmapException()
        {
            Canvas canvas = null;
            
            CanvasConverter converter = new CanvasConverter();
            converter.ConvertCanvasToBitmap(canvas);
        }

    }
}

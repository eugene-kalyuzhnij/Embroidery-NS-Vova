using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections.Generic;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class Test_CanvasConverter
    {
        [TestMethod]
        public void TestMethod_ConvertBitmapToCanvas()
        {
            Bitmap input = new Bitmap(2, 2);

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (i % 2 == 0)
                        input.SetPixel(i, j, Color.Red);
                    else
                        input.SetPixel(i, j, Color.Blue);
                }

            NSEmbroidery.Core.Canvas expectedCanvas = new NSEmbroidery.Core.Canvas(new Resolution(2, 2));

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (i % 2 == 0)
                        expectedCanvas.SetColor(i, j, Color.FromArgb(255, 0, 0));
                    else
                        expectedCanvas.SetColor(i, j, Color.FromArgb(0, 0, 255));
                }


            NSEmbroidery.Core.Canvas actual = NSEmbroidery.Core.CanvasConverter.ConvertBitmapToCanvas(input);

            Assert.IsTrue(expectedCanvas.Count == actual.Count);

            IEnumerator<Color> e1 = expectedCanvas.GetEnumerator();
            IEnumerator<Color> e2 = actual.GetEnumerator();
            
            while (e1.MoveNext() && e2.MoveNext())
                Assert.AreEqual(e1.Current, e2.Current);

        }


        [TestMethod]
        public void TestMethod_ConvertCanvasToBitmap()
        {
            Canvas canvas = new Canvas(new Resolution(2, 2));

            canvas.SetColor(0, 0, Color.FromArgb(0, 255, 0));
            canvas.SetColor(0, 1, Color.FromArgb(0, 255, 0));
            canvas.SetColor(1, 0, Color.FromArgb(255, 0, 0));
            canvas.SetColor(1, 1, Color.FromArgb(255, 0, 0));

            Bitmap actual = CanvasConverter.ConvertCanvasToBitmap(canvas);


            Bitmap expected = new Bitmap(2, 2);
            for( int i = 0; i < expected.Height; i++)
                for(int j = 0; j < expected.Width; j++)
                {
                    if(i == 0)
                        expected.SetPixel(i, j, Color.FromArgb(0, 255, 0));
                    else
                        expected.SetPixel(i, j, Color.FromArgb(255, 0, 0));
                }


            Assert.IsTrue(expected.Size == actual.Size, "Wrong Size");

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    Color expectedColor = expected.GetPixel(i, j);
                    Color actualColor = actual.GetPixel(i, j);
                    Assert.IsTrue(expectedColor == actualColor, "Wrong Pixel j = " + j.ToString() + "; i = " + i.ToString());
                }

        }


        
    }
}

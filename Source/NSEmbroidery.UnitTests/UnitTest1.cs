using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections;
using NSEmbroidery.Core.Decorators;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1_ColorDecorator()
        {
            Bitmap inputImage = new Bitmap(7, 5);

            #region Filling inputImage
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 7; x++)
                {
                    if ((x == 2 && y == 0) ||
                        (x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 5 && y == 1)
                        )
                        inputImage.SetPixel(x, y, Color.Green);
                    else if ((x == 0 && y == 2) ||
                            (x == 1 && y == 2) ||
                            (x == 2 && y == 2) ||
                            (x == 3 && y == 2) ||
                            (x == 2 && y == 3)
                            )
                        inputImage.SetPixel(x, y, Color.Blue);
                    else
                        inputImage.SetPixel(x, y, Color.Red);
                }
            #endregion


            Bitmap expectedImage = new Bitmap(6, 4);

            #region Filling expectedImage
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 4 && y == 1) ||
                        (x == 5 && y == 1)
                        )
                        expectedImage.SetPixel(x, y, Color.Green);
                    else if ((x == 2 && y == 2) ||
                            (x == 3 && y == 2) ||
                            (x == 2 && y == 3) ||
                            (x == 3 && y == 3)
                            )
                        expectedImage.SetPixel(x, y, Color.Blue);
                    else
                        expectedImage.SetPixel(x, y, Color.Red);
                }
            #endregion


            var patternCreator = new PatternCreator()
            {
                PatternMapGenerator = new PatternMapGenerator(),
                CanvasConverter = new CanvasConverter(),
                DecoratorsComposition = new DecoratorsComposition()
            };


            Bitmap actual = patternCreator.GetEmbroidery(inputImage, new Settings()
            {
                CellsCount = 3,
                Coefficient = 2,
                Palette = new Palette(new Color[]{Color.Red, Color.Blue, Color.Green}),
            });


            Assert.IsTrue(actual.Height == expectedImage.Height && actual.Width == expectedImage.Width);

            for (int y = 0; y < expectedImage.Height; y++)
                for (int x = 0; x < expectedImage.Width; x++)
                    Assert.IsTrue(actual.GetPixel(x, y) == expectedImage.GetPixel(x, y));

        }

        [TestMethod]
        public void TestMethod1_ColorGridDecorator()
        {
            Bitmap inputImage = new Bitmap(7, 5);

            #region Filling inputImage
            for (int y = 0; y < 5; y++)
                for (int x = 0; x < 7; x++)
                {
                    if ((x == 2 && y == 0) ||
                        (x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 5 && y == 1)
                        )
                        inputImage.SetPixel(x, y, Color.Green);
                    else if ((x == 0 && y == 2) ||
                            (x == 1 && y == 2) ||
                            (x == 2 && y == 2) ||
                            (x == 3 && y == 2) ||
                            (x == 2 && y == 3)
                            )
                        inputImage.SetPixel(x, y, Color.Blue);
                    else
                        inputImage.SetPixel(x, y, Color.Red);
                }
            #endregion


            Bitmap expectedImage = new Bitmap(6, 4);

            #region Filling expectedImage
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 0 && y == 2) ||
                        (x == 4 && y == 2) ||
                        (x == 2 && y == 0)
                        )
                        expectedImage.SetPixel(x, y, Color.Red);
                    else if ((x == 2 && y == 2))
                        expectedImage.SetPixel(x, y, Color.Blue);
                    else if ((x == 4 && y == 0))
                        expectedImage.SetPixel(x, y, Color.Green);
                    else
                        expectedImage.SetPixel(x, y, Color.Black);
                }
            #endregion




            var patternCreator = new PatternCreator()
            {
                PatternMapGenerator = new PatternMapGenerator(),
                CanvasConverter = new CanvasConverter(),
                DecoratorsComposition = new DecoratorsComposition()
            };


            Bitmap actual = patternCreator.GetEmbroidery(inputImage, new Settings()
            {
                CellsCount = 3,
                Coefficient = 2,
                Palette = new Palette(new Color[] { Color.Red, Color.Blue, Color.Green }),
                GridType = GridType.SolidLine
            });

            Assert.IsTrue(actual.Height == expectedImage.Height && actual.Width == expectedImage.Width);

            for (int y = 0; y < expectedImage.Height; y++)
                for (int x = 0; x < expectedImage.Width; x++)
                {
                    Color actualColor = actual.GetPixel(x, y);
                    Color expectedColor = expectedImage.GetPixel(x, y);
                    Assert.IsTrue(actualColor == expectedColor);
                }

        }


    }
}

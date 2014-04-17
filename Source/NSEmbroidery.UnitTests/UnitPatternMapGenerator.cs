using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitPatternMapGenerator
    {
        [TestMethod]
        public void Test_PatternMapGenerator_Generate()
        {
            Canvas expectedPattern = new Canvas(new Resolution(3, 2));

#region FillingPattern
            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (x == 2 && y == 0)
                        expectedPattern.SetColor(x, y, Color.Green);
                    else if (x == 1 && y == 1)
                        expectedPattern.SetColor(x, y, Color.Blue);
                    else
                        expectedPattern.SetColor(x, y, Color.Red);
                }
#endregion


            Canvas inputCanvas = new Canvas(new Resolution(6, 4));

#region FillingFillingExpectedCanvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 2 && y == 0) ||
                        (x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 5 && y == 1)
                        )
                        inputCanvas.SetColor(x, y, Color.Green);
                    else if ((x == 0 && y == 2) ||
                             (x == 1 && y == 2) ||
                             (x == 2 && y == 2) ||
                             (x == 3 && y == 2) ||
                             (x == 2 && y == 3)
                            )
                        inputCanvas.SetColor(x, y, Color.Blue);
                    else
                        inputCanvas.SetColor(x, y, Color.Red);
                }
#endregion


            Settings settings = new Settings();
            settings.CellsCount = 3;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[]{Color.Red, Color.Green, Color.Blue});

            PatternMapGenerator mapGenerator = new PatternMapGenerator();

            Canvas actual = mapGenerator.Generate(inputCanvas, settings);


            Assert.IsTrue(actual.Height == expectedPattern.Height && actual.Width == expectedPattern.Width);

            for (int y = 0; y < expectedPattern.Height; y++)
                for (int x = 0; x < expectedPattern.Width; x++)
                    Assert.IsTrue(actual.GetColor(x, y) == expectedPattern.GetColor(x, y));


        }
    }



}

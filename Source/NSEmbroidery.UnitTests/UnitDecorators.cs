using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using System.Drawing;
using System.Collections;
using NSEmbroidery.Core.Decorators;
using Moq;
using Ninject;
using Ninject.Modules;

namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitDecorators
    {

        [TestMethod]
        public void TestMethod_CellsDecorator()
        {

            Settings settings = new Settings();
            settings.CellsCount = 3;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Green, Color.Blue }); 

            Canvas pattern = new Canvas(new Resolution(3, 2));

#region FillingPattern
             for (int y = 0; y < 2; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (x == 2 && y == 0)
                        pattern.SetColor(x, y, Color.Green);
                    else if (x == 1 && y == 1)
                       pattern.SetColor(x, y, Color.Blue);
                    else
                        pattern.SetColor(x, y, Color.Red);
                }
#endregion


            Canvas expectedCanvas = new Canvas(new Resolution(6, 4));

#region FillingFillingExpectedCanvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 4 && y == 0) ||
                        (x == 5 && y == 0) ||
                        (x == 4 && y == 1) ||
                        (x == 5 && y == 1)
                        )
                        expectedCanvas.SetColor(x, y, Color.Green);
                    else if ((x == 2 && y == 2) ||
                            (x == 3 && y == 2) ||
                            (x == 2 && y == 3) ||
                            (x == 3 && y == 3)
                            )
                        expectedCanvas.SetColor(x, y, Color.Blue);
                    else
                        expectedCanvas.SetColor(x, y, Color.Red);
                }
#endregion

            Canvas actual = new Canvas(new Resolution(6, 4));

            CellsDecorator decorator = new CellsDecorator();
            decorator.Decorate(actual, pattern, settings);

            Assert.IsTrue(actual.Height == expectedCanvas.Height && actual.Width == expectedCanvas.Width);

            for (int y = 0; y < expectedCanvas.Height; y++)
                for (int x = 0; x < expectedCanvas.Width; x++)
                    Assert.IsTrue(actual.GetColor(x, y) == expectedCanvas.GetColor(x, y));

        }



        [TestMethod]
        public void TestMethod_GridDecorator()
        {
            Settings settings = new Settings();
            settings.CellsCount = 3;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[] { Color.Red, Color.Green, Color.Blue });
            settings.GridType = GridType.SolidLine;

            Canvas pattern = new Canvas(new Resolution(3, 2));

#region FillingPattern
            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (x == 2 && y == 0)
                        pattern.SetColor(x, y, Color.Green);
                    else if (x == 1 && y == 1)
                        pattern.SetColor(x, y, Color.Blue);
                    else
                        pattern.SetColor(x, y, Color.Red);
                }
#endregion


            Canvas expectedCanvas = new Canvas(new Resolution(6, 4));

#region FillingExpectedCanvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                {
                    if ((x == 0 && y == 0) ||
                        (x == 0 && y == 2) ||
                        (x == 4 && y == 2) ||
                        (x == 2 && y == 0) ||
                        (x == 2 && y == 2) ||
                        (x == 4 && y == 0)     )
                        expectedCanvas.SetColor(x, y, Color.Green);
                    else
                        expectedCanvas.SetColor(x, y, Color.Black);
                }
#endregion

            Canvas actual = new Canvas(new Resolution(6, 4));

#region FillingExpectedCanvas
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 6; x++)
                        actual.SetColor(x, y, Color.Green);
#endregion

             GridDecorator decoratorGrid = new GridDecorator();
             decoratorGrid.Decorate(actual, pattern, settings);

            Assert.IsTrue(actual.Height == expectedCanvas.Height && actual.Width == expectedCanvas.Width);

            for (int y = 0; y < expectedCanvas.Height; y++)
                for (int x = 0; x < expectedCanvas.Width; x++)
                    Assert.IsTrue(actual.GetColor(x, y) == expectedCanvas.GetColor(x, y));

        }

    }
}

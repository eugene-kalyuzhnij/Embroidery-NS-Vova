using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSEmbroidery.Core;
using NSEmbroidery.Core.Interfaces;
using Moq;
using System.Drawing;


namespace NSEmbroidery.UnitTests
{
    [TestClass]
    public class UnitPatternCreator
    {
        [TestMethod]
        public void Test_PatternCreator_GetEmbroidery()
        {
            var mockCanvasConverter = new Mock<ICanvasConverter>();
            var mockPatternMapGenerator = new Mock<IPatternMapGenerator>();
            var mockDecoratorCompositions = new Mock<IDecoratorsComposition>();

            mockPatternMapGenerator.Setup(map => map.Generate(It.IsAny<Canvas>(), It.IsAny<Settings>())).Returns(new Canvas(3, 3));
            mockCanvasConverter.Setup(conv => conv.ConvertBitmapToCanvas(It.IsAny<Bitmap>())).Returns(new Canvas(4, 3));
            mockDecoratorCompositions.Setup(decor => decor.Decorate(It.IsAny<Canvas>(), It.IsAny<Canvas>(), It.IsAny<Settings>()));

            Settings settings = new Settings();
            settings.CellsCount = 3;
            settings.Coefficient = 2;
            settings.Palette = new Palette(new Color[]{Color.Red, Color.Blue});
            settings.DecoratorsComposition = mockDecoratorCompositions.Object;

            EmbroideryCreator creator = new EmbroideryCreator();
            creator.CanvasConverter = mockCanvasConverter.Object;
            creator.PatternMapGenerator = mockPatternMapGenerator.Object;

            Bitmap image = new Bitmap(4, 3);

            creator.GetEmbroidery(image, settings);

            mockCanvasConverter.Verify(conv => conv.ConvertBitmapToCanvas(image));
            mockPatternMapGenerator.Verify(pat => pat.Generate(It.IsAny<Canvas>(), settings));
            mockDecoratorCompositions.Verify(decor => decor.Decorate(It.IsAny<Canvas>(), It.IsAny<Canvas>(), settings));
            mockCanvasConverter.Verify(conv => conv.ConvertCanvasToBitmap(It.IsAny<Canvas>()));

        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_PatternCreator_GetEmbroideryException1()
        {
            Settings settings = new Settings();

            EmbroideryCreator creator = new EmbroideryCreator();

            Bitmap image = new Bitmap(4, 2);
            creator.GetEmbroidery(image, settings);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_PatternCretor_GetEmbroideryException2()
        {
            Settings settings = new Settings()
            {
                CellsCount = 1,
                Coefficient = 0
            };

            EmbroideryCreator creator = new EmbroideryCreator();

            Bitmap image = new Bitmap(5, 3);
            creator.GetEmbroidery(image, settings);
        }


        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_PatternCretor_GetEmbroideryException3()
        {
            Settings settings = new Settings()
            {
                CellsCount = 1,
                Coefficient = 1,
            };

            EmbroideryCreator creator = new EmbroideryCreator();

            Bitmap image = new Bitmap(5, 3);
            creator.GetEmbroidery(image, settings);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongInitializedException))]
        public void Test_PatternCretor_GetEmbroideryException4()
        {
            Settings settings = new Settings()
            {
                CellsCount = 1,
                Coefficient = 1,
                Palette = new Palette()
            };

            EmbroideryCreator creator = new EmbroideryCreator();

            Bitmap image = new Bitmap(5, 3);
            creator.GetEmbroidery(image, settings);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Decorators;
using NSEmbroidery.Core.Interfaces;
using Ninject;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Web;

namespace NSEmbroidery.Core
{
    public class EmbroideryCreator : IEmbroideryCreatorService
    {
        EventLog log  = new EventLog("EmbroideryServiceLog");

        public EmbroideryCreator()
        {
            log.Source = "EmbroiderySource";
        }

        public IPatternMapGenerator PatternMapGenerator { get; set; }

        public ICanvasConverter CanvasConverter { get; set; }



        public Bitmap GetEmbroidery(Bitmap image, Settings settings)
        {
            if (settings.CellsCount <= 0)
                throw new WrongInitializedException("cellsCount has to be > 0");
            if (settings.Coefficient <= 0)
                throw new WrongInitializedException("coefficient has to be > 0");
            if (settings.Palette == null || settings.Palette.Count == 0)
                throw new WrongInitializedException("palette has to be initialized");

            Canvas imageCanvas = CanvasConverter.ConvertBitmapToCanvas(image);

            log.WriteEntry("---------Time spent----------");
            log.WriteEntry(@"-----Convert Bitmap To Canvas: " + Environment.NewLine +
                            "---------resol: " + image.Width.ToString() + "x" + image.Height.ToString());

            Canvas pattern = PatternMapGenerator.Generate(imageCanvas, settings);
            log.WriteEntry("-------------------");
            log.WriteEntry(@"-----Generate pattern: "+ Environment.NewLine +
                            "--------resol: " + imageCanvas.Width.ToString() + "x" + imageCanvas.Height.ToString() + Environment.NewLine +
                            "--------cells: " + settings.CellsCount.ToString());

            Resolution resolution = new Resolution(pattern.Width * settings.Coefficient, pattern.Height * settings.Coefficient);


            Canvas result = new Canvas(resolution);


            settings.Decorate(result, pattern);

            Bitmap resultImage = CanvasConverter.ConvertCanvasToBitmap(result);

            log.WriteEntry("-------------------");
            log.WriteEntry(@"-----Convert Canvas To Bitmap: " + Environment.NewLine +
                            "---------resol: " + result.Width.ToString() + "x" + result.Height.ToString());
            log.WriteEntry("---------End time spent-------");

            return resultImage;
        }





        public Bitmap GetEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, GridType type)
        {

            log.WriteEntry(@"Come in the GetEmbroidery(...)" + Environment.NewLine +
                             "    cells count = " + cellsCount + Environment.NewLine +
                             "    resolution coefficient = " + resolutionCoefficient);
            Bitmap result = null;
            try
            {
                result = CreateEmbroidery(image, resolutionCoefficient, cellsCount, palette, symbols, symbolColor, type);
            }
            catch (Exception ex)
            {
                log.WriteEntry(@"Exception in GetEmbroidery(...) occurred
                                    Message: " + ex.Message);
            }


            log.WriteEntry(@"GetEmbroidery was executed" +
                            "    Result image = " + result.ToString());

            return result;
        }



        public static Bitmap CreateEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, GridType type)
        {

            IKernel kernel = new StandardKernel(new PropertiesModel());

            var patternMapGenerator = kernel.Get<IPatternMapGenerator>();
            var canvasConverter = kernel.Get<ICanvasConverter>();

            var patternCreator = new EmbroideryCreator()
            {
                PatternMapGenerator = patternMapGenerator,
                CanvasConverter = canvasConverter,
            };

            Settings settings = new Settings()
            {
                CellsCount = cellsCount,
                Coefficient = resolutionCoefficient,
                Palette = new Palette(palette),
                Symbols = symbols,
                SymbolColor = symbolColor,
                GridType = type,
                DecoratorsComposition = new DecoratorsComposition()
            };

            if (settings.Palette != null)
                settings.DecoratorsComposition.AddDecorator(new CellsDecorator());
            if (settings.Symbols != null)
                settings.DecoratorsComposition.AddDecorator(new SymbolsDecorator());
            if (settings.GridType != Core.GridType.None)
                settings.DecoratorsComposition.AddDecorator(new GridDecorator());

            Bitmap result = patternCreator.GetEmbroidery(image, settings);

            
            return result;
        }


        public Dictionary<string, int> PossibleResolutions(Bitmap image, int cellsCount, int countResolutions)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            if (cellsCount <= 0)
                throw new WrongInitializedException("Square count has to be initialized and inherent");

            if (countResolutions < 0)
                throw new WrongInitializedException("Count of resolution has to be more than zero");

            if (image.Width < cellsCount)
                throw new WrongResolutionException("Image's width must be higher or input less cells");

            int cellWidth = image.Width / cellsCount;

            if (image.Height < cellWidth)
                throw new WrongResolutionException("Image's height must be higher or input more cells");

            int newHeight = image.Height / cellWidth;
            int newWidth = cellsCount;

            for (int i = 2; i < countResolutions + 2; i++)
                result.Add(
                    new Resolution(newWidth * i, newHeight * i).ToString(),
                    i
                );

            return result;
        }


        public Dictionary<string, int> PossibleResolutions(Bitmap image, int cellsCount, int minCoefficient, int maxCoefficient)
        {

            Dictionary<string, int> result = new Dictionary<string, int>();

            if (cellsCount <= 0)
                throw new WrongInitializedException("Square count has to be initialized and inherent");

            if (image.Width < cellsCount)
                throw new WrongResolutionException("Image's width must be higher or input less cells");

            int cellWidth = image.Width / cellsCount;

            if (image.Height < cellWidth)
                throw new WrongResolutionException("Image's height must be higher or input more cells");

            if (minCoefficient < 2 || minCoefficient >= maxCoefficient)
                throw new Exception("minCoefficient has to be less than maxCoefficient and more than 2");


            int newHeight = image.Height / cellWidth;
            int newWidth = cellsCount;


            for (int i = minCoefficient; i <= maxCoefficient; i++)
                result.Add(
                    new Resolution(newWidth * i, newHeight * i).ToString(),
                    i
                );

            return result;
        }



    }
}

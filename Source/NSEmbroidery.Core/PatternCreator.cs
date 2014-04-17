using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Decorators;
using NSEmbroidery.Core.Interfaces;
using Ninject;

namespace NSEmbroidery.Core
{
    public class PatternCreator
    {


        public PatternCreator()
        {
        }

        [Inject]
        public IPatternMapGenerator PatternMapGenerator { get; set; }

        [Inject]
        public ICanvasConverter CanvasConverter { get; set; }

        [Inject]
        public IDecoratorsComposition DecoratorsComposition{ get; set; }




        public Bitmap GetEmbroidery(Bitmap image, Settings settings)
        {
            if (settings.CellsCount <= 0)
                throw new NotInitializedException("cellsCount has to be > 0");
            if (settings.Coefficient <= 0)
                throw new NotInitializedException("coefficient has to be > 0");
            if (settings.Palette == null)
                throw new NotInitializedException("palette doesn't have to be null");

            Canvas pattern = PatternMapGenerator.Generate(CanvasConverter.ConvertBitmapToCanvas(image), settings);
            Resolution resolution = new Resolution(pattern.Width * settings.Coefficient, pattern.Height * settings.Coefficient);

            if(settings.Palette != null)
                DecoratorsComposition.AddDecorator(new CellsDecorator());
            if(settings.Symbols != null)
                DecoratorsComposition.AddDecorator(new SymbolsDecorator());
            if(settings.GridType != Core.GridType.None)
                DecoratorsComposition.AddDecorator(new GridDecorator());


            Canvas result = new Canvas(resolution);
            DecoratorsComposition.Decorate(result, pattern, settings);

            return CanvasConverter.ConvertCanvasToBitmap(result);
        }




        public static Bitmap CreateEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, GridType type)
        {

            IKernel kernel = new StandardKernel(new PropertiesModel());

            var patternMapGenerator = kernel.Get<IPatternMapGenerator>();
            var decoratoComposition = kernel.Get<IDecoratorsComposition>();
            var canvasConverter = kernel.Get<ICanvasConverter>();

            var patternCreator = new PatternCreator()
            {
                PatternMapGenerator = patternMapGenerator,
                CanvasConverter = canvasConverter,
                DecoratorsComposition = decoratoComposition
            };


            Bitmap result = patternCreator.GetEmbroidery(image, new Settings()
            {
                CellsCount = cellsCount,
                Coefficient = resolutionCoefficient,
                Palette = new Palette(palette),
                Symbols = symbols,
                SymbolColor = symbolColor,
                GridType = type
            });

            return result;
        }



    }
}

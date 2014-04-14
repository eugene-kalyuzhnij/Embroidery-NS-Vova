using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Decorators;

namespace NSEmbroidery.Core
{
    public class PatternCreator
    {

        public PatternCreator()
        {
        }

        public PatternMapGenerator PatternMapGenerator { get; set; }

        public CanvasConverter CanvasConverter { get; set; }

        public DecoratorsComposition DecoratorsComposition{ get; set; }


        public Bitmap GetEmbroidery(Bitmap image, Settings settings)
        {
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

            var patternCreator = new PatternCreator()
            {
                PatternMapGenerator = new PatternMapGenerator(),
                CanvasConverter = new CanvasConverter(),
                DecoratorsComposition = new DecoratorsComposition()
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

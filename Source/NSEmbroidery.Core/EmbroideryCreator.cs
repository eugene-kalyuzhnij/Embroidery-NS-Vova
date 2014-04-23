﻿using System;
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
    public class EmbroideryCreator : IEmbroideryCreatorService
    {


        public EmbroideryCreator()
        {
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

            Canvas pattern = PatternMapGenerator.Generate(CanvasConverter.ConvertBitmapToCanvas(image), settings);
            Resolution resolution = new Resolution(pattern.Width * settings.Coefficient, pattern.Height * settings.Coefficient);


            Canvas result = new Canvas(resolution);
            settings.Decorate(result, pattern);

            return CanvasConverter.ConvertCanvasToBitmap(result);
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



    }
}
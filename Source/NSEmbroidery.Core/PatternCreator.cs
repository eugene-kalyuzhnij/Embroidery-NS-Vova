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
        Settings Settings{get; set;}
        private bool SymbolsFlag{get; set;}
        private bool gridFlag { get; set; }
        private bool ColorFlag { get; set; }

        private Canvas pattern;

        public PatternCreator()
        {
            Settings = new Core.Settings();

            ColorFlag = true;
            SymbolsFlag = false;
            gridFlag = false;
        }

        public GridType GridType
        {
            get { return Settings.GridType; }
            set { Settings.GridType = value; }
        }


        public bool GridFlag { 
            get { return gridFlag; }
            set { gridFlag = value; }
        }


        public int CellsCount { 
            get { return Settings.CellsCount; }
            set { Settings.CellsCount = value; }
        }

        public Color[] Palette
        {
            get { return Settings.Palette.GetAllColors(); }
            set { Settings.Palette = new Palette(value); }
        }

        public Char[] Symbols
        {
            get { return Settings.Symbols; }
            set { Settings.Symbols = value; }
        }

        public Color SymbolColor
        {
            get { return Settings.SymbolColor; }
            set { Settings.SymbolColor = value; }
        }


        public Bitmap GetEmbroidery(Bitmap image, int horisontalCellCount, int ratio)
        {
            CanvasConverter converter = new CanvasConverter();
            DecoratorsCompositors decorator = new DecoratorsCompositors();
            PatternMapGenerator mapGenerator = new PatternMapGenerator();

            Settings.CellsCount = horisontalCellCount;

            pattern = mapGenerator.Generate(converter.ConvertBitmapToCanvas(image), Settings);
            Resolution resolution = new Resolution(pattern.Width * ratio, pattern.Height * ratio);

            if(ColorFlag)
                decorator.AddDecorator(new CellsDecorator());
            if(SymbolsFlag)
                decorator.AddDecorator(new SymbolsDecorator());
            if(gridFlag)
                decorator.AddDecorator(new GridDecorator());


            Canvas result = new Canvas(resolution);
            decorator.Decorate(result, pattern, Settings);

            return converter.ConvertCanvasToBitmap(result);
        }

        public static Bitmap CreateEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette)
        {
            PatternCreator creator = new PatternCreator();
            creator.CellsCount = cellsCount;
            creator.Palette = palette;

            Bitmap result = creator.GetEmbroidery(image, cellsCount, resolutionCoefficient);

            return result;

        }

        public static Bitmap CreateEmbroidery(Bitmap image, int resolutionCoefficient, int cellsCount, Color[] palette, char[] symbols, Color symbolColor, bool grid, GridType type)
        {
            PatternCreator creator = new PatternCreator();
            creator.CellsCount = cellsCount;
            if(symbols != null)
            {
                creator.SymbolsFlag = true;
                creator.Symbols = symbols;
                creator.SymbolColor = symbolColor;
            }
            creator.Palette = palette;
            creator.gridFlag = grid;
            creator.GridType = type;

            Bitmap result = creator.GetEmbroidery(image, cellsCount, resolutionCoefficient);

            return result;
        }


    }
}

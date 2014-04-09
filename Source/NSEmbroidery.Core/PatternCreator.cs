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

        private Bitmap CurrentImage;
        Settings Settings{get; set;}
        public bool SymbolsFlag{get; set;}
        public bool GridFlag { get; set; }
        public bool ColorFlag { get; set; }

        private Canvas pattern;

        public PatternCreator(Bitmap image)
        {
            CurrentImage = image;
            Settings = new Core.Settings();

            ColorFlag = true;
            SymbolsFlag = false;
            GridFlag = false;
        }


        public int SquareCount { 
            get { return Settings.SquareCount; }
            set { Settings.SquareCount = value; }
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

            PatternMapGenerator mapGenerator = new PatternMapGenerator();

            mapGenerator.Settings = Settings;
            pattern = mapGenerator.Generate(CanvasConverter.ConvertBitmapToCanvas(image));

            DecoratorsCompositors decorator = new DecoratorsCompositors();
            decorator.Settings = Settings;

            if(ColorFlag)
                decorator.AddDecorator(new SquaresDecorator());
            if(SymbolsFlag)
                decorator.AddDecorator(new SymbolsDecorator());
            if(GridFlag)
                decorator.AddDecorator(new GridDecorator());


            Resolution resolution;
            resolution = new Resolution(pattern.Width * ratio, pattern.Height * ratio);

            Canvas result = new Canvas(resolution);
            decorator.Decorate(result, pattern);

            return CanvasConverter.ConvertCanvasToBitmap(result);
        }




    }
}

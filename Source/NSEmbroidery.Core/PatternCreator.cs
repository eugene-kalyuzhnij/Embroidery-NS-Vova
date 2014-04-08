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

        public Resolution Resolution
        {
            get{return Settings.Resolution;}
            set { Settings.Resolution = value; }
        }


        public Bitmap GetImage()
        {

            PatternMapGenerator mapGenerator = new PatternMapGenerator();

            mapGenerator.Settings = Settings;
            Canvas pattern = mapGenerator.Generate(CanvasConverter.ConvertBitmapToCanvas(CurrentImage));

            DecoratorsCompositors decorator = new DecoratorsCompositors();
            decorator.Settings = Settings;

            if(ColorFlag)
                decorator.AddDecorator(new SquaresDecorator());
            if(SymbolsFlag)
                decorator.AddDecorator(new SymbolsDecorator());
            if(GridFlag)
                decorator.AddDecorator(new GridDecorator());

            if (Settings.Resolution == null) throw new NullReferenceException("Resolution is null");

            Canvas result = new Canvas(Settings.Resolution);
            decorator.Decorate(result, pattern);

            return CanvasConverter.ConvertCanvasToBitmap(result);
        }


        public List<int> GetPossibleSquareCounts()
        {
            int top = CurrentImage.Width;
            List<int> result = new List<int>();

            for (int i = top - 1; i > 1; i--)
                if (top % i == 0) result.Add(i);

            return result;
        }


        public List<Resolution> GetPossibleResolutions(int count)//Don't work correctly
        {
            List<Resolution> result = new List<Resolution>();
            Resolution current = new Resolution(CurrentImage.Width, CurrentImage.Height);  
            for (int i = 1; i <= count; i++)
            {
                result.Add(new Resolution(current.Width * i, current.Height * i));
            }

            return result;
        }

    }
}

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
        public Settings Settings{get; set;}
        public bool Symbols{get; set;}
        public bool Grid { get; set; }

        public PatternCreator(Bitmap image)
        {
            CurrentImage = image;

            Symbols = false;
            Grid = false;
        }

        public Bitmap GetImage()
        {
            PatternMapGenerator map = new PatternMapGenerator();

            map.Settings = Settings;
            Canvas pattern = map.Generate(CanvasConverter.ConvertBitmapToCanvas(CurrentImage));

            Canvas result = new Canvas(Settings.Resolution);

            DecoratorsCompositors decorator = new DecoratorsCompositors();
            decorator.Settings = Settings;
            decorator.AddDecorator(new SquaresDecorator());
            if(Symbols)
                decorator.AddDecorator(new SymbolsDecorator());
            if(Grid)
                decorator.AddDecorator(new GridDecorator());

            decorator.Decporate(result, pattern);

            CurrentImage = CanvasConverter.ConvertCanvasToBitmap(result);

            return CurrentImage;
        }


        public List<int> GetPossibleSquareCounts()
        {
            int top = CurrentImage.Width;
            List<int> result = new List<int>();

            for (int i = top - 1; i > 1; i--)
                if (top % i == 0) result.Add(i);

            return result;
        }


        public List<Resolution> GetPossibleResolutions(int count)
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

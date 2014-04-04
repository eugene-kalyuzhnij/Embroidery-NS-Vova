using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class PutternCreator
    {

        private Bitmap CurrentImage;

        public PutternCreator(Color[] colors, char[] symbols, Bitmap image, int squareCount)
        {
            CurrentImage = image;
            Settings.Resolution = new Resolution(image.Width, image.Height);
            Settings.Palette = new Palette(colors);
            Settings.Symbols = symbols;
            Settings.SquareCount = squareCount;

            Settings.CreateColorSymbolRelation();
        }

        public Bitmap GetImage()
        {

            PutternMapGenerator map = new PutternMapGenerator();
            Canvas puttern = map.Generate(CanvasConverter.ConvertBitmapToCanvas(CurrentImage));

            Canvas result = CanvasConverter.ConvertBitmapToCanvas(CurrentImage);

            IDecorator decorator = new SquaresDecorator();
            decorator.Decorate(result, puttern);
            IDecorator symbols = new SymbolsDecorator();
            symbols.Decorate(result, puttern);

            CurrentImage = CanvasConverter.ConvertCanvasToBitmap(result);

            return CurrentImage;

        }
    }
}

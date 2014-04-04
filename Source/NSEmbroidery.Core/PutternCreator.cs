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
        public Settings Settings{get; set;}

        public PutternCreator(Bitmap image)
        {
            CurrentImage = image;
        }

        public Bitmap GetImage()
        {

            PutternMapGenerator map = new PutternMapGenerator();

            map.Settings = Settings;
            Canvas puttern = map.Generate(CanvasConverter.ConvertBitmapToCanvas(CurrentImage));

            Canvas result = CanvasConverter.ConvertBitmapToCanvas(CurrentImage);

            IDecorator decorator = new SquaresDecorator();
            decorator.Decorate(result, puttern);
            SymbolsDecorator symbols = new SymbolsDecorator();
            symbols.Settings = Settings;
            symbols.Decorate(result, puttern);

            CurrentImage = CanvasConverter.ConvertCanvasToBitmap(result);

            return CurrentImage;

        }
    }
}

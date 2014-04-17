using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Interfaces;

namespace NSEmbroidery.Core
{
    public class CanvasConverter : ICanvasConverter
    {
        public Canvas ConvertBitmapToCanvas(Bitmap image)
        {
            int width = image.Width;
            int height = image.Height;

            Canvas canvas = new Canvas(new Resolution(width, height));

            for(int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    canvas.SetColor(x, y, image.GetPixel(x, y));
                }
            return canvas;
        }

        public Bitmap ConvertCanvasToBitmap(Canvas canvas)
        {
            int width = canvas.Width;
            int height = canvas.Height;

            Bitmap image = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    image.SetPixel(x, y, canvas.GetColor(x, y));
            return image;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class Canvas : IEnumerable<Color>
    {
        Color[,] Color;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int Count { get{return Color.Length;} }

        public void SetColor(int x, int y, Color color)
        {
            Color[y, x] = color;
        }


        public Resolution GetResolution()
        {
            return new Resolution(Width, Height);
        }

        public Color GetColor(int x, int y)
        {
            return Color[y, x];
        }

        public Canvas(Resolution resolution)
        {
            Width = resolution.Width;
            Height = resolution.Height;
            Color = new Color[Height, Width];
        }


        public IEnumerator<Color> GetEnumerator()
        {
            if (Color != null)
                for (int i = 0; i <= Color.GetUpperBound(1); i++)
                    for (int j = 0; j <= Color.GetUpperBound(0); j++)
                    {
                        yield return Color[i, j];
                    }

            else throw new Exception("Not implement Color field");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (Color != null)
                for (int i = 0; i < Color.GetUpperBound(1); i++)
                    for (int j = 0; j < Color.GetUpperBound(0); j++)
                    {
                        yield return Color[i, j];
                    }

            else throw new Exception("Not implement Color field");
        }


        public static Canvas DrawSymbol(Canvas canvas,int x, int y,int size, char symbol)
        {
            Bitmap tempBitmap = CanvasConverter.ConvertCanvasToBitmap(canvas);

            Graphics g = Graphics.FromImage(tempBitmap);
            Font font = new Font(FontFamily.GenericSansSerif, size, GraphicsUnit.Pixel);
            FontFamily fFamily = FontFamily.GenericSansSerif;

            g.DrawString(symbol.ToString(), font, Brushes.Black, new PointF(x, y));

            Canvas resultCanvas = CanvasConverter.ConvertBitmapToCanvas(tempBitmap);

            return resultCanvas;
        }
    }
}

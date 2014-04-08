using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Decorators.BorderAlgorithms
{
    class LineBorder : IBorderAlgorithm
    {
        public void SetBorder(Canvas canvas, int x, int y, int width, int height, Color color, Aligns align)
        {
            Canvas inner = null;

            switch (align)
            {
                case Aligns.Left:
                    {
                        inner = canvas.GetInnerCanvas(x, y, new Resolution(width, height));
                        for (int _y = 0; _y < height; _y++)
                            inner.SetColor(0, _y, color);
                        break;
                    }
                case Aligns.Right:
                    {
                        inner = canvas.GetInnerCanvas(x, y, new Resolution(width, height));
                        for (int _y = 0; _y < height; _y++)
                            inner.SetColor(width - 1, _y, color);
                        break;
                    }
                case Aligns.Top:
                    {
                        inner = canvas.GetInnerCanvas(x, y, new Resolution(width, height));
                        for (int _x = 0; _x < width; _x++)
                            inner.SetColor(_x, 0, color);
                        break;
                    }
                case Aligns.Buttom:
                    {
                        inner = canvas.GetInnerCanvas(x, y, new Resolution(width, height));
                        for (int _x = 0; _x < width; _x++)
                            inner.SetColor(_x, height - 1, color);
                        break;
                    }
            }

            canvas.SetCanvas(x, y, inner);
        }
    }
}

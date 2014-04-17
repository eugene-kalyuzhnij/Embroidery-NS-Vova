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
            if (resolution == null)
                throw new NotInitializedException("Resolution is not initialized");

            Width = resolution.Width;
            Height = resolution.Height;
            Color = new Color[Height, Width];
        }

        public void SetColor(int x, int y, Color color)
        {
            Color[y, x] = color;
        }

        public IEnumerator<Color> GetEnumerator()
        {
            if (Color != null)
            {
                int width = Color.GetUpperBound(1);
                int height = Color.GetUpperBound(0);

                for (int y = 0; y <= height; y++)
                    for (int x = 0; x <= width; x++)
                    {
                        yield return Color[y, x];
                    }
            }
            else throw new Exception("Not implement Color field");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            if (Color != null)
            {
                int width = Color.GetUpperBound(1);
                int height = Color.GetUpperBound(0);

                for (int y = 0; y <= height; y++)
                    for (int x = 0; x <= width; x++)
                    {
                        yield return Color[y, x];
                    }
            }
            else throw new Exception("Not implement Color field");
        }

        public void SetSymbol(char symbol, int x, int y, int squareWidth, Color color)
        {
            CanvasConverter converter = new CanvasConverter();
            Bitmap smallPart = new Bitmap(squareWidth, squareWidth);

            for (int _y = y, i = 0; _y < y + squareWidth; _y++, i++)
                for (int _x = x, j = 0; _x < x + squareWidth; _x++, j++)
                    smallPart.SetPixel(j, i, this.GetColor(_x, _y));

            Graphics g = Graphics.FromImage(smallPart);


            Font font = new Font("Arial", squareWidth - 1, GraphicsUnit.Pixel);
            string sym = symbol.ToString();
            sym = sym.PadLeft(1, symbol);

            SolidBrush brush = new SolidBrush(color);

            g.DrawString(sym, font, brush, new PointF(0f, 0f));


            Canvas innerCanvas = converter.ConvertBitmapToCanvas(smallPart);

            this.SetCanvas(x, y, innerCanvas);

        }

        private Color AverageColor(Color[] colors)
        {
            int averageA = 0;
            int averageR = 0;
            int averageG = 0;
            int averageB = 0;

            foreach (var color in colors)
            {
                averageA += color.A;
                averageR += color.R;
                averageG += color.G;
                averageB += color.B;
            }


            averageA = averageA / colors.Length;
            averageR = averageR / colors.Length;
            averageG = averageG / colors.Length;
            averageB = averageB / colors.Length;

            
            return System.Drawing.Color.FromArgb(averageA, averageR, averageG, averageB);
        }

        public Canvas ReduceResolution(int newWidth, int newHeight, int cellWidth)
        {

            Canvas smallCanvas = new Canvas(new Resolution(newWidth, newHeight));

            for (int smallY = 0, bigY = 0; smallY < newHeight; smallY++, bigY += cellWidth)
                for (int smallX = 0, bigX = 0; smallX < newWidth; smallX++, bigX += cellWidth)
                {
                    Canvas partCanvas = this.GetInnerCanvas(bigX, bigY, new Resolution(cellWidth, cellWidth));
                    Color[] colors = new Color[cellWidth * cellWidth];

                    int i = 0;
                    foreach (var color in partCanvas)
                    {
                        colors[i++] = color;
                    }

                    Color resultColor = AverageColor(colors);
                    smallCanvas.SetColor(smallX, smallY, resultColor);

                }

            return smallCanvas;

        }

        public void SetBorder(int x, int y, int width, int height, Color color, Aligns align, GridType type)
        {

            Canvas inner = null;

            if (type == GridType.Points)
            {
                #region PointsGridAlgorithm
                switch (align)
                {
                    case Aligns.Left:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _y = 0; _y < height; _y++)
                            {
                                if (_y % 3 == 0)
                                    inner.SetColor(0, _y, color);
                            }
                            break;
                        }
                    case Aligns.Right:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _y = 0; _y < height; _y++)
                            {
                                if (_y % 3 == 0)
                                    inner.SetColor(width - 1, _y, color);
                            }
                            break;
                        }
                    case Aligns.Top:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _x = 0; _x < width; _x++)
                            {
                                if (_x % 3 == 0)
                                    inner.SetColor(_x, 0, color);
                            }
                            break;
                        }
                    case Aligns.Buttom:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _x = 0; _x < width; _x++)
                            {
                                if (_x % 3 == 0)
                                    inner.SetColor(_x, height - 1, color);
                            }
                            break;
                        }
                }
                #endregion
            }
            else
            {
                #region LineGridAlgorithm
                switch (align)
                {
                    case Aligns.Left:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _y = 0; _y < height; _y++)
                                    inner.SetColor(0, _y, color);
                            break;
                        }
                    case Aligns.Right:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _y = 0; _y < height; _y++)
                                    inner.SetColor(width - 1, _y, color);
                            break;
                        }
                    case Aligns.Top:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _x = 0; _x < width; _x++)
                                    inner.SetColor(_x, 0, color);
                            break;
                        }
                    case Aligns.Buttom:
                        {
                            inner = this.GetInnerCanvas(x, y, new Resolution(width, height));
                            for (int _x = 0; _x < width; _x++)
                                    inner.SetColor(_x, height - 1, color);
                            break;
                        }
                }
                #endregion
            }

            this.SetCanvas(x, y, inner);
        }

        public void SetCanvas(int x, int y, Canvas innerCanvas)
        {
            if (x + innerCanvas.Width > this.Width || y + innerCanvas.Height > this.Height)
                throw new Exception("Can't set inner canvas");
            

            for (int _y = y, i = 0; _y < y + innerCanvas.Height; _y++, i++)
                for (int _x = x, j = 0; _x < x + innerCanvas.Width; _x++, j++)
                {
                    this.SetColor(_x, _y, innerCanvas.GetColor(j, i));
                }

        }

        public Canvas GetInnerCanvas(int x, int y, Resolution resol)
        {
            if (x + resol.Width > this.Width || y + resol.Height > this.Height)
                throw new Exception("Can't get inner canvas");
            
            Canvas result = new Canvas(resol);

            for (int _y = y, i = 0; _y < y + resol.Height; _y++, i++)
                for (int _x = x, j = 0; _x < x + resol.Width; _x++, j++)
                {
                    result.SetColor(j, i, this.GetColor(_x, _y));
                }

            return result;

        }

    }

    public enum Aligns
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Buttom = 3
    }

}

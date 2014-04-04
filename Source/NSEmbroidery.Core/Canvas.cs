﻿using System;
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


        public void SetSymbol(char symbol, int x, int y, int squareWidth)
        {

            Bitmap smallPart = new Bitmap(squareWidth, squareWidth);

            for (int _y = y, i = 0; _y < y + squareWidth; _y++, i++)
                for (int _x = x, j = 0; _x < x + squareWidth; _x++, j++)
                    smallPart.SetPixel(j, i, this.GetColor(_x, _y));

            Graphics g = Graphics.FromImage(smallPart);
            Font font = new Font(FontFamily.GenericSansSerif, squareWidth, GraphicsUnit.Pixel);

            g.DrawString(symbol.ToString(), font, Brushes.Green, new PointF(0, 0));


            Canvas innerCanvas = CanvasConverter.ConvertBitmapToCanvas(smallPart);

            this.SetCanvas(x, y, innerCanvas);

            /*for (int _y = y, i = 0; _y < y + squareWidth; _y++, i++)
                for (int _x = x, j = 0; _x < x + squareWidth; _x++, j++)
                {
                    Color partColor = smallPart.GetPixel(j, i);
                    this.SetColor(_x, _y, partColor);
                }*/
        }

        public void SetCanvas(int x, int y, Canvas innerCanvas)
        {
            if (x + innerCanvas.Width > this.Width || y + innerCanvas.Height > this.Height)
                throw new Exception();
            

            for (int _y = y, i = 0; _y < y + innerCanvas.Height; _y++, i++)
                for (int _x = x, j = 0; _x < x + innerCanvas.Width; _x++, j++)
                {
                    this.SetColor(_x, _y, innerCanvas.GetColor(j, i));
                }

        }

        public Canvas GetInnerCanvas(int x, int y, Resolution resol)
        {
            if (x + resol.Width > this.Width || y + resol.Height > this.Height)
                throw new Exception();
            
            Canvas result = new Canvas(resol);

            for (int _y = y, i = 0; _y < y + resol.Height; _y++, i++)
                for (int _x = x, j = 0; _x < x + resol.Width; _x++, j++)
                {
                    result.SetColor(j, i, this.GetColor(x, y));
                }

            return result;

        }

    }
}
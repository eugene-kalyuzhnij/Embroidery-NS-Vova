﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class PutternMapGenerator
    {

        public PutternMapGenerator()
        {
        }

        public PutternMapGenerator(Palette palette)
        {
            Settings.Palette = palette;
        }


        public Canvas Generate(Canvas canvas)
        {
            if (Settings.Palette == null || Settings.Palette.Count == 0)
                throw new NullReferenceException("Check that Palette isn't null or it has any colors");
            if (Settings.SquareCount <= 0)
                throw new WrongFieldException("SquareCount field is wrong");


            Canvas tempCanvas = ReduceResolution(canvas);

            List<Color> colors = Settings.Palette.GetAllColors();
            int width = tempCanvas.Width;
            int height = tempCanvas.Height;

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                {
                    Color oldColor = tempCanvas.GetColor(x, y);
                    Color colorAmoung = ChooseColorAmoung(oldColor, colors);
                    tempCanvas.SetColor(x, y, colorAmoung);
                }

            return tempCanvas;
        }


        private Color ChooseColorAmoung(Color oldColor, List<Color> amoungColors)
        {
            Color resultColor;
            int min = GetDifference(oldColor, amoungColors[0]);

            resultColor = amoungColors[0];

            for (int i = 1; i < amoungColors.Count; i++)
            {
                int tempMin;
                tempMin = GetDifference(oldColor, amoungColors[i]);
                if (tempMin < min)
                {
                    min = tempMin;
                    resultColor = amoungColors[i];
                }
            }

            return resultColor;
        }


        private int GetDifference(Color color1, Color color2)
        {
            int dif = 0;
            dif += Math.Abs(color1.R - color2.R);
            dif += Math.Abs(color1.G - color2.G);
            dif += Math.Abs(color1.B - color2.B);

            return dif;
        }


        private Canvas ReduceResolution(Canvas canvas)
        {
                Bitmap image = CanvasConverter.ConvertCanvasToBitmap(canvas);

                int sourceHeight = image.Height;
                int sourceWidth = image.Width;

                int newHeight = sourceHeight - Math.Abs(sourceHeight - Settings.SquareCount);
                int newWidth = sourceWidth - Math.Abs(sourceWidth - Settings.SquareCount);

                Bitmap tempImage = null;
                if (newHeight > 0 && newWidth > 0)
                {
                    tempImage = new Bitmap(image, new Size(newWidth, newHeight));
                }
                else throw new WrongFieldException();

                canvas = CanvasConverter.ConvertBitmapToCanvas(tempImage);

                return canvas;
        }

    }
}

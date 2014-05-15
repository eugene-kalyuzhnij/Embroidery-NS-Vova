using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Interfaces;

namespace NSEmbroidery.Core
{
    public class PatternMapGenerator : IPatternMapGenerator
    {

        public Canvas Generate(Canvas canvas, Settings settings)
        {

            if (settings.Palette == null || settings.Palette.Count == 0)
                throw new NullReferenceException("Check that Palette isn't null or it has any colors");
            if (settings.CellsCount <= 0)
                throw new WrongInitializedException("Square count has to be initialized and more than zero");

            if (canvas.Width < settings.CellsCount)
                throw new WrongResolutionException("Image's width must be higher or input less cells");

            int cellWidth = canvas.Width / settings.CellsCount;

            if (canvas.Height < cellWidth)
                throw new WrongResolutionException("Image's height must be higher");

            int newHeight = canvas.Height / cellWidth;
            int newWidth = settings.CellsCount;

            Canvas tempCanvas = canvas.ReduceResolution(newWidth, newHeight, cellWidth);

            List<Color> colors = settings.Palette.GetAllColorsList();

            /*for (int y = 0; y < tempCanvas.Height; y++)
                for (int x = 0; x < tempCanvas.Width; x++)
                {
                    Color oldColor = tempCanvas.GetColor(x, y);
                    Color colorAmoung = ChooseColorAmoung(oldColor, colors);
                    tempCanvas.SetColor(x, y, colorAmoung);
                }*/


            Parallel.For(0, tempCanvas.Height, y =>
                {
                    Parallel.For(0, tempCanvas.Width, x => 
                        {
                            Color oldColor = tempCanvas.GetColor(x, y);
                            Color colorAmoung = ChooseColorAmoung(oldColor, colors);
                            tempCanvas.SetColor(x, y, colorAmoung);
                        });
                    
                });

            return tempCanvas;
        }


        public Color ChooseColorAmoung(Color oldColor, List<Color> amoungColors)
        {
            if (amoungColors == null || amoungColors.Count == 0)
                throw new WrongInitializedException("Wrong 'amoungColors' parametr");

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


        public int GetDifference(Color color1, Color color2)
        {
            int dif = 0;
            dif += Math.Abs(color1.A - color2.A);
            dif += Math.Abs(color1.R - color2.R);
            dif += Math.Abs(color1.G - color2.G);
            dif += Math.Abs(color1.B - color2.B);

            return dif;
        }

    }
}

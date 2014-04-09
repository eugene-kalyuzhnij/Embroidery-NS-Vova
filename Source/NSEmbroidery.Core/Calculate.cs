using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public static class Calculate
    {

        public static List<int> PossibleCellsCount(Bitmap image)
        {
            int top = image.Width;
            List<int> result = new List<int>();

            for (int i = top - 1; i > 1; i--)
                if (top % i == 0) result.Add(i);

            return result;
        }


        public static Dictionary<Resolution, int> PossibleResolutions(Bitmap image, int cellsCount, Color[] colors, int countResolutions)
        {
            Dictionary<Resolution, int> result = new Dictionary<Resolution, int>();

            PatternMapGenerator mapGenerator = new PatternMapGenerator();
            mapGenerator.Settings.SquareCount = cellsCount;
            mapGenerator.Settings.Palette = new Palette(colors);
            Canvas pattern = mapGenerator.Generate(CanvasConverter.ConvertBitmapToCanvas(image));

            for (int i = 2; i < countResolutions + 2; i++)
                result.Add(new Resolution(pattern.Width * i, pattern.Height * i), i);

            return result;
        }

    }
}

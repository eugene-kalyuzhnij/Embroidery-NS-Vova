using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public static class Settings
    {


        public static void SetResolution(Resolution resol)
        {
            Resolution = resol;
        }



        public static void SetSquareCont(int count)
        {
            SquareCount = count;
        }

        public static void SetSymbols(char[] symbols)
        {
            Symbols = symbols;
        }

        public static void SetProperties(int squareCount, Resolution resolution, char[] symbols)
        {
            SetResolution(resolution);
            SetSymbols(symbols);
            SetSquareCont(squareCount);
        }

        public static int SquareCount { get; set; }
        public static Resolution Resolution { get; set; }
        public static char[] Symbols { get; set; }
        public static Palette Palette { get; set; }
        public static Dictionary<Color, Char> ColorSymbolRelation;

        public static int GetResolutionWidth()
        {
            return Resolution.Width;
        }

        public static int GetResolutionHight()
        {
            return Resolution.Height;
        }


        public static void CreateColorSymbolRelation()
        {
            if (Palette == null)
                throw new NullReferenceException("Object Palette is null");
            if (Symbols == null)
                throw new NullReferenceException("Object Symbols is null");
            if (Symbols.Length < Palette.Count)
                throw new WrongFieldException("symbols have to be more than colors in palette");

            ColorSymbolRelation = new Dictionary<Color,char>();
            List<Color> colors = Palette.GetAllColors();

            for (int i = 0; i < Palette.Count; i++)
                ColorSymbolRelation.Add(colors[i], Symbols[i]);

        }
    }


    public class Resolution
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Resolution(int width, int height)
        {
            Height = height;
            Width = width;
        }


        public void SetResolution(int width, int height)
        {
            Height = height;
            Width = width;
        }
    }
}

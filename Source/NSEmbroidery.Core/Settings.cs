using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class Settings
    {

        public void SetSquareCont(int count)
        {
            CellsCount = count;
        }

        public void SetSymbols(char[] symbols)
        {
            Symbols = symbols;
        }

        public void SetProperties(int squareCount, char[] symbols)
        {
            SetSymbols(symbols);
            SetSquareCont(squareCount);
        }

        public int CellsCount { get; set; }
        public char[] Symbols { get; set; }
        public Palette Palette { get; set; }
        public Dictionary<Color, Char> ColorSymbolRelation;
        public Color SymbolColor { get; set; }
        public GridType GridType { get; set; }



        public void CreateColorSymbolRelation()
        {
            if (Palette == null)
                throw new NullReferenceException("Object Palette is null");
            if (Symbols == null)
                throw new NullReferenceException("Object Symbols is null");
            if (Symbols.Length < Palette.Count)
                throw new WrongSymbolsRealisationException("Symbols have to be more than colors");

            ColorSymbolRelation = new Dictionary<Color,char>();
            List<Color> colors = Palette.GetAllColorsList();

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

        public override string ToString()
        {
            return Width.ToString() + "x" + Height.ToString();
        }
    }



    public enum GridType
    {
        SolidLine,
        Points
    }
}

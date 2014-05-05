using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Decorators;
using NSEmbroidery.Core.Interfaces;
using System.Runtime.Serialization;

namespace NSEmbroidery.Core
{
    public class Settings
    {
        public int CellsCount { get; set; }
        public char[] Symbols { get; set; }
        public Palette Palette { get; set; }

        public Dictionary<Color, Char> ColorSymbolRelation;
        public Color SymbolColor { get; set; }
        public GridType GridType { get; set; }
        public int Coefficient { get; set; }

        public IDecoratorsComposition DecoratorsComposition { get; set; }


        public void CreateColorSymbolRelation()
        {
            if (Palette == null || Palette.Count == 0)
                throw new NullReferenceException("Palette is null");
            if (Symbols == null)
                throw new NullReferenceException("Symbols is null");
            if (Symbols.Length < Palette.Count)
                throw new WrongSymbolsRealisationException("Symbols have to be more than colors");

            ColorSymbolRelation = new Dictionary<Color,char>();
            List<Color> colors = Palette.GetAllColorsList();

            for (int i = 0; i < Palette.Count; i++)
                ColorSymbolRelation.Add(colors[i], Symbols[i]);
        }


        public void Decorate(Canvas embroidery, Canvas pattern)
        {
            DecoratorsComposition.Decorate(embroidery, pattern, this);
        }
    }

    public enum GridType
    {
        None,
        SolidLine,
        Points
    }
}

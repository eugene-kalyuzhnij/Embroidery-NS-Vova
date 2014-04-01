using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class CrissCross
    {
        public Color Color { get; private set; }
        public Char Symbol { get; private set; }


        public void SetColor(Color color)
        {
            Color = color;
        }

        public void SetSymbol(char symbol)
        {
            Symbol = symbol;
        }
    }
}

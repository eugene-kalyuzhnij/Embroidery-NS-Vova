using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class SymbolsDecorator : IDecorator
    {
        public void Decorate(Canvas embroidery, Canvas puttern)
        {

            int squareWidth = embroidery.Width / puttern.Width;


            for(int squareY = 0, puttrenY = 0; squareY <= embroidery.Height - squareWidth; squareY += squareWidth, puttrenY++)
                for (int squareX = 0, putternX = 0; squareX <= embroidery.Width - squareWidth; squareX += squareWidth, putternX++)
                {
                    char symbol = GetSymbol(puttern.GetColor(putternX, puttrenY));
                    embroidery.SetSymbol(symbol, squareX, squareY, squareWidth);
                }

        }


        private void ChangeConvas(Canvas sourceCanvas, Canvas otheCanvas)
        {
            for(int y = 0; y < sourceCanvas.Height; y++)
                for(int x = 0; x < sourceCanvas.Width; x++)
                    sourceCanvas.SetColor(x, y, otheCanvas.GetColor(x, y));
        }


        private char GetSymbol(Color color)
        {
            char symbol;
            if (!Settings.ColorSymbolRelation.TryGetValue(color, out symbol))
                throw new Exception("There are no value for this key, key = " + color.ToString());

            return symbol;
        }


    }
}

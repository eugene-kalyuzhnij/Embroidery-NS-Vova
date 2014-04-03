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

            Bitmap[,] bitmapSymbols = new Bitmap[puttern.Height, puttern.Width];

            int newX = 0;
            int newY = 0;

            int x = 0;
            int y = 0;

            Canvas tempCanvas = embroidery;

            for (int i = 0; i < puttern.Height; i++)
            {
                for (int j = 0; j < puttern.Width; j++)
                {
                    Char symbol;
                    Settings.ColorSymbolRelation.TryGetValue(puttern.GetColor(j, i), out symbol);
                    for (y = newY; y < newY + squareWidth; y++)
                        for (x = newX; x < newX + squareWidth; x++)
                        {
                        }

                    newX += squareWidth;

                    tempCanvas = Canvas.DrawSymbol(tempCanvas, x, y, squareWidth, symbol);
                }

                newY += squareWidth;
                newX = 0;
            }


            ChangeConvas(embroidery, tempCanvas);

        }


        private void ChangeConvas(Canvas sourceCanvas,  Canvas otheCanvas)
        {
            for(int y = 0; y < sourceCanvas.Height; y++)
                for(int x = 0; x < sourceCanvas.Width; x++)
                    sourceCanvas.SetColor(x, y, otheCanvas.GetColor(x, y));
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core
{
    public class SquaresDecorator : IDecorator
    {
        public void Decorate(Canvas embroidery, Canvas puttern)
        {
            Resolution putternResolution = puttern.GetResolution();
            Resolution embroideryResolution = embroidery.GetResolution();

            int squareWidth = embroideryResolution.Width / putternResolution.Width;

            int newX = 0;
            int newY = 0;

            for (int i = 0; i < putternResolution.Height; i++)
            {
                for (int j = 0; j < putternResolution.Width; j++)
                {
                    for (int y = newY; y < newY + squareWidth; y++)
                        for (int x = newX; x < newX + squareWidth; x++)
                        {
                            embroidery.SetColor(x, y, puttern.GetColor(j, i));
                        }
                    newX += squareWidth;
                }

                newY += squareWidth;
                newX = 0;
            }
            
        }
    }
}

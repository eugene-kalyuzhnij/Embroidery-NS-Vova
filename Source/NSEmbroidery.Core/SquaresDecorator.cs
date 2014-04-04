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


             int squareWidth = checked(embroidery.Width / puttern.Width);


            int newX = 0;
            int newY = 0;

            for (int i = 0; i < puttern.Height; i++)
            {
                for (int j = 0; j < puttern.Width; j++)
                {
                    for (int y = newY; y < newY + squareWidth; y++)
                        for (int x = newX; x < newX + squareWidth; x++)
                            embroidery.SetColor(x, y, puttern.GetColor(j, i));

                    newX += squareWidth;
                }

                newY += squareWidth;
                newX = 0;
            }
            
        }
    }
}

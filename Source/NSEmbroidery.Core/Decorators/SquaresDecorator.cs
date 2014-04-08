using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core.Decorators
{
    public class SquaresDecorator : IDecorator
    {
        public Settings Settings { get; set; }

        public void Decorate(Canvas embroidery, Canvas pattern)
        {
            int squareWidth = embroidery.Width / Settings.SquareCount;

            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            int newX = 0;
            int newY = 0;

            for (int i = 0; i < pattern.Height; i++)
            {
                for (int j = 0; j < pattern.Width; j++)
                {
                    for (int y = newY; y < newY + squareWidth; y++) 
                        for (int x = newX; x < newX + squareWidth; x++)
                            embroidery.SetColor(x, y, pattern.GetColor(j, i));

                    newX += squareWidth;
                }

                newY += squareWidth;
                newX = 0;
            }
  
        }


    }
}

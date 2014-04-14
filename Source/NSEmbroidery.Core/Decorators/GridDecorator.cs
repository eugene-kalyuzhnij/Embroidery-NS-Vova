using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Decorators
{
    class GridDecorator : IDecorator
    {

        public void Decorate(Canvas embroidery, Canvas pattern, Settings settings)
        {

            int squareWidth = embroidery.Width / settings.CellsCount;

            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            for (int squareY = 0; squareY <= embroidery.Height - squareWidth; squareY += squareWidth)
                for (int squareX = 0; squareX <= embroidery.Width - squareWidth; squareX += squareWidth)
                {
                    embroidery.SetBorder(squareX, squareY, squareWidth, squareWidth, Color.Black, Aligns.Right, settings.GridType);
                    embroidery.SetBorder(squareX, squareY, squareWidth, squareWidth, Color.Black, Aligns.Buttom, settings.GridType);
                }


        }

    }
}

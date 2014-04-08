using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using NSEmbroidery.Core.Decorators.BorderAlgorithms;

namespace NSEmbroidery.Core.Decorators
{
    class GridDecorator : IDecorator
    {
        public Settings Settings { get; set; }

        public void Decorate(Canvas embroidery, Canvas pattern)
        {

            ContextBorderAlgorithm borderAlgorithm = new ContextBorderAlgorithm(Settings.GridType);

            int squareWidth = embroidery.Width / Settings.SquareCount;

            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            for (int squareY = 0; squareY <= embroidery.Height - squareWidth; squareY += squareWidth)
                for (int squareX = 0; squareX <= embroidery.Width - squareWidth; squareX += squareWidth)
                {
                    borderAlgorithm.ExecuteAlgorithm(embroidery, squareX, squareY, squareWidth, squareWidth, Color.Black, Aligns.Left);
                    borderAlgorithm.ExecuteAlgorithm(embroidery, squareX, squareY, squareWidth, squareWidth, Color.Black, Aligns.Buttom);
                }
        }


    }
}

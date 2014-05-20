using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace NSEmbroidery.Core.Decorators
{

    public class CellsDecorator : IDecorator
    {

        private object locker = new object();

        public void Decorate(Canvas embroidery, Canvas pattern, Settings settings)
        {
            int squareWidth = embroidery.Width / settings.CellsCount;

            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            /*Parallel.For with Partitioner*/
           Parallel.ForEach(Partitioner.Create(0, pattern.Height), (rangeHeight) =>
            {
                for (int i = rangeHeight.Item1; i < rangeHeight.Item2; i++)
                    Parallel.ForEach(Partitioner.Create(0, pattern.Width), (rangeWidth) =>
                        {
                            for (int j = rangeWidth.Item1; j < rangeWidth.Item2; j++)
                            {
                                int startX = j * squareWidth;
                                int endX = startX + squareWidth;

                                int startY = i * squareWidth;
                                int endY = startY + squareWidth;

                                for (int y = startY; y < endY; y++)
                                    for (int x = startX; x < endX; x++)
                                        embroidery.SetColor(x, y, pattern.GetColor(j, i));
                            }
                        });
            });
            

            #region Just parallel.For
            /*
            Parallel.For(0, pattern.Height, i =>
                {

                    Parallel.For(0, pattern.Width, j =>
                    {
                        int startX = j * squareWidth;
                        int endX = startX + squareWidth;

                        int startY = i * squareWidth;
                        int endY = startY + squareWidth;

                        for (int y = startY; y < endY; y++)
                            for (int x = startX; x < endX; x++)
                                embroidery.SetColor(x, y, pattern.GetColor(j, i));
                    });
                });
            */
            #endregion

            #region Obsolete
            /*
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
             */
            #endregion

        }

    }

}

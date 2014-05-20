using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Concurrent;

namespace NSEmbroidery.Core.Decorators
{

    public class SymbolsDecorator : IDecorator
    {

        public void Decorate(Canvas embroidery, Canvas pattern, Settings settings)
        {
            int squareWidth = embroidery.Width / settings.CellsCount;
            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            try
            {
                settings.CreateColorSymbolRelation();
            }
            catch (WrongSymbolsRealisationException e)
            {
                throw new WrongSymbolsRealisationException(e.Message);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException(e.Message);
            }

            Color symbolColor;
            if(settings.SymbolColor == Color.Empty) symbolColor = Color.Black;
            else symbolColor = settings.SymbolColor;



            Parallel.ForEach(Partitioner.Create(0, pattern.Height), rangeHeight =>
                {
                    for (int patternY = rangeHeight.Item1; patternY < rangeHeight.Item2; patternY++)
                    {
                        Parallel.ForEach(Partitioner.Create(0, pattern.Width), rangeWidth =>
                            {
                                for (int patternX = rangeWidth.Item1; patternX < rangeWidth.Item2; patternX++)
                                {
                                    int startX = patternX * squareWidth;
                                    int startY = patternY * squareWidth;

                                    char symbol = GetSymbol(pattern.GetColor(patternX, patternY), settings);
                                    embroidery.SetSymbol(symbol, startX, startY, squareWidth, symbolColor);
                                }
                            });
                    }
                });


            #region Just Parallel.For
            /*
            Parallel.For(0, pattern.Height, patternY =>
                {
                    Parallel.For(0, pattern.Width, patternX =>

                    {
                        int startX = patternX * squareWidth;
                        int startY = patternY * squareWidth;

                        char symbol = GetSymbol(pattern.GetColor(patternX, patternY), settings);
                        embroidery.SetSymbol(symbol, startX, startY, squareWidth, symbolColor);
                    });
                });
            */
            #endregion

            #region Obsolete
            /*for(int squareY = 0, patternY = 0; squareY <= embroidery.Height - squareWidth; squareY += squareWidth, patternY++)
                for (int squareX = 0, patternX = 0; squareX <= embroidery.Width - squareWidth; squareX += squareWidth, patternX++)
                {
                    char symbol = GetSymbol(pattern.GetColor(patternX, patternY), settings);
                    embroidery.SetSymbol(symbol, squareX, squareY, squareWidth, symbolColor);
                }*/
            #endregion

        }


        private char GetSymbol(Color color, Settings settings)
        {
            char symbol;
            if (!settings.ColorSymbolRelation.TryGetValue(color, out symbol))
                throw new Exception("There are no value for this key, key = " + color.ToString());

            return symbol;
        }


    }
}

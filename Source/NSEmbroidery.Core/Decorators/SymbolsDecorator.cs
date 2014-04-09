using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Decorators
{
    public class SymbolsDecorator : IDecorator
    {

        public Settings Settings { get; set; }

        public void Decorate(Canvas embroidery, Canvas pattern)
        {
            int squareWidth = embroidery.Width / Settings.CellsCount;
            if (embroidery.Height < pattern.Height * squareWidth)
                throw new WrongResolutionException("Resolution.Height has to be higher");

            try
            {
                Settings.CreateColorSymbolRelation();
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
            if(Settings.SymbolColor == Color.Empty) symbolColor = Color.Black;
            else symbolColor = Settings.SymbolColor;
                
            for(int squareY = 0, patternY = 0; squareY <= embroidery.Height - squareWidth; squareY += squareWidth, patternY++)
                for (int squareX = 0, patternX = 0; squareX <= embroidery.Width - squareWidth; squareX += squareWidth, patternX++)
                {
                    char symbol = GetSymbol(pattern.GetColor(patternX, patternY));
                    embroidery.SetSymbol(symbol, squareX, squareY, squareWidth, symbolColor);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core
{
    public class Settings
    {

        public Settings(int crissCrossXCount, Resolution resol, char[] symbols)
        {
            CrissCrossXCount = crissCrossXCount;
            Resolution = resol;
            Symbols = symbols;
        }

        public int CrissCrossXCount { get; set; }
        public Resolution Resolution { get; set; }
        public char[] Symbols { get; set; }

        public int GetResolutionWidth()
        {
            return Resolution.Width;
        }

        public int GetResolutionHight()
        {
            return Resolution.Height;
        }

    }


    public class Resolution
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public Resolution(int width, int height)
        {
            Height = height;
            Width = width;
        }


        public void SetResolution(int width, int height)
        {
            Height = height;
            Width = width;
        }
    }
}

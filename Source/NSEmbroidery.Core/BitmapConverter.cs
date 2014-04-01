using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class BitmapConverter
    {

//||||||||||||||||||Properties||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public Bitmap Image { get; set; }
        public Color[] Colors { get; private set; }
        public Settings Settings { get; private set; }

        private Object lockObj = new object();


        public int ImageWidth
        {
            get { return Image.Width; }
        }

        public int ImageHeight
        {
            get { return Image.Height; }
        }

        public Color GetPixel(int x, int y)
        {
            return Image.GetPixel(x, y);
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||Constructors||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public BitmapConverter(string path, Settings settings)
        {
            Image = new Bitmap(path);
            Settings = settings;
        }

        public BitmapConverter(string path, Color[] convertColors, Settings settings)
            : this(path, settings)
        {
            Colors = convertColors;
        }


        public BitmapConverter(Bitmap image, Settings settings)
        {
            Image = new Bitmap(image);
            Settings = settings;
        }

        public BitmapConverter(Bitmap image, Color[] convertColors, Settings settings):this(image, settings)
        {
            Colors = convertColors;
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||Seters||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public void SetColors(Color[] colors)
        {
            Colors = colors;
        }

        public void SetResolution(Resolution resol)
        {
            Settings.Resolution = resol;
        }

        public void SetCrissCrossesXcount(int count)
        {
            Settings.CrissCrossXCount = count;
        }

        public void SetSymbols(char[] symbols)
        {
            Settings.Symbols = symbols;
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||Methods|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public Bitmap ChangeResolution(int width)
        {
            lock (lockObj)
            {
                int newHeight;
                if (width < Image.Width)
                    newHeight = Image.Height - Math.Abs(Image.Width - width);
                else newHeight = Image.Height + Math.Abs(Image.Width - width);


                Bitmap tempImage = new Bitmap(Image,
                                              new Size(width, newHeight));

                this.Image = tempImage;
                return tempImage;
            }
        }



        public Bitmap ChangeColor()
        {
            lock (lockObj)
            {
                Bitmap tempImage = this.Image;

                for (int x = 0; x < tempImage.Width; x++)
                    for (int y = 0; y < tempImage.Height; y++)
                    {
                        Color oldColor = tempImage.GetPixel(x, y);
                        Color colorAmoung = ChooseColorAmoung(oldColor, Colors);
                        tempImage.SetPixel(x, y, colorAmoung);
                    }

                return tempImage;
            }
        }

        private Color ChooseColorAmoung(Color changeTo, Color[] amoungColors)
        {
            Color resultColor;
            int min = GetDifference(changeTo, amoungColors[0]);

            resultColor = amoungColors[0];

            for(int i = 1; i < amoungColors.Length; i++)
            {
                int tempMin;
                tempMin = GetDifference(changeTo, amoungColors[i]);
                if(tempMin < min)
                {
                    min = tempMin;
                    resultColor = amoungColors[i];
                }
            }

            return resultColor;  
        }


        private int GetDifference(Color color1, Color color2)
        {
            int dif = 0;
            dif += Math.Abs(color1.R - color2.R);
            dif += Math.Abs(color1.G - color2.G);
            dif += Math.Abs(color1.B - color2.B);

            return dif;
        }


        private bool IsTheSame(Color color1, Color color2)
        {
            if (color1.R == color2.R && color1.G == color2.G && color1.B == color2.B)
                return true;
            return false;
        }



       public Dictionary<Color, char> CreateSymbols(char[] symbols)
        {
            if (symbols.Length >= Colors.Length)
            {
                Dictionary<Color, Char> result = new Dictionary<Color,char>();

                int i = 0;
                foreach(var color in Colors)
                {
                    result.Add(color, symbols[i++]);
                }

                return result;
            }
            else
            {
                throw new Exception("Symbols has to be larger then count of colors");
            }
        }


       public CrissCross[,] GetScheme(Dictionary<Color, char> color_symbol)
       {
           CrissCross[,] result = new CrissCross[Image.Width, Image.Height];

           for (int x = 0; x < Image.Width; x++)
               for (int y = 0; y < Image.Height; y++)
               {
                   char symbol;
                   Color pixelColor = Image.GetPixel(x, y);

                   if (color_symbol.TryGetValue(pixelColor, out symbol))
                   {
                       result[x, y].SetColor(pixelColor);
                       result[x, y].SetSymbol(symbol);
                   }
                   else
                       throw new Exception("Wrong color_symbol parametr");

               }

           return result;
       }



       public Bitmap GetScheme()
       {
           Bitmap scheme = new Bitmap(Settings.GetResolutionWidth(), Settings.GetResolutionHight());

           CrissCross[,] cCrosses = GetScheme(CreateSymbols(new char[]{'$','@','#'}));

           
           int OneCrossDimention = Settings.GetResolutionWidth() / Settings.CrissCrossXCount;

           int newX = 0;
           int newY = 0;

           for (int i = 0; i <= cCrosses.GetUpperBound(1); i++)
           {
               for (int j = 0; j <= cCrosses.GetUpperBound(0); j++)
               {
                   for (int y = newY; y < OneCrossDimention; y++)
                       for (int x = newX; x < OneCrossDimention; x++)
                       {
                           scheme.SetPixel(x, y, cCrosses[i, j].Color);
                       }
                   newX += OneCrossDimention;
               }

               newY += OneCrossDimention;
           }


           return scheme;
       }

    }

}

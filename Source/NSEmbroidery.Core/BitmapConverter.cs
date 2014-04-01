using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    class BitmapConverter
    {
        public Bitmap Image { get; set; }
        public Color[] Colors { get; private set; }
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

        public BitmapConverter(string path, Color[] convertColors = null)
        {
            Image = new Bitmap(path);
        }

        public BitmapConverter(Bitmap image, Color[] convertColors = null)
        {
            Image = new Bitmap(image);
        }

        public void SetColors(Color[] colors)
        {
            Colors = colors;
        }

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



        public Bitmap ChangeColor(Color[] colors)
        {
            lock (lockObj)
            {
                Bitmap tempImage = this.Image;

                for (int x = 0; x < tempImage.Width; x++)
                    for (int y = 0; y < tempImage.Height; y++)
                    {
                        Color oldColor = tempImage.GetPixel(x, y);
                        Color colorAmoung = ChooseColorAmoung(oldColor, colors);
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

    }




    public class CrissCross
    {
        public Color Color { get; private set; }
        public Char Symbol { get; private set; }


        public void SetColor(Color color)
        {
            Color = color;
        }

        public void SetSymbol(char symbol)
        {
            Symbol = symbol;
        }
    }
}

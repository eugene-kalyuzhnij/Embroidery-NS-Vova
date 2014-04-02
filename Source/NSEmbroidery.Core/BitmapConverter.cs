using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{


    //this class doesn't work!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    public class BitmapConverter
    {

//||||||||||||||||||Properties||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
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

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||Constructors||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        public BitmapConverter(string path)
        {
            Image = new Bitmap(path);
        }


        public BitmapConverter(Bitmap image)
        {
            Image = new Bitmap(image);
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||public Methods||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public Bitmap ReduceResolutionTo(int width)
        {
            lock (lockObj)
            {
                int sourceHeight = Image.Height;
                int sourceWidth = Image.Width;

                int newHeight = sourceHeight - width;
                int newWidth = sourceWidth - width;

                Bitmap tempImage = null;
                if (newHeight > 0 && newWidth > 0)
                {
                    tempImage = new Bitmap(Image, new Size(newWidth, newHeight));
                }
                else throw new NotImplementResolutionException("Maybe parameter /'width = " + width.ToString() + "/' is too higher");

                this.Image = tempImage;
                return tempImage;
            }
        }



        public Bitmap ChangeColorsTo(Color[] colors)
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

                Colors = colors;
                return tempImage;
            }
        }

        public Dictionary<Color, char> CreateSymbols(char[] symbols)
        {
            if (Colors != null)
            {
                if (symbols.Length >= Colors.Length)
                {
                    Dictionary<Color, Char> result = new Dictionary<Color, char>();

                    int i = 0;
                    foreach (var color in Colors)
                    {
                        result.Add(color, symbols[i++]);
                    }

                    return result;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
            throw new NotImplementedException();
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
                        result[x, y] = new CrissCross();
                        result[x, y].SetColor(pixelColor);
                        result[x, y].SetSymbol(symbol);
                    }
                    else
                        throw new Exception("Wrong color_symbol parametr");

                }

            return result;
        }



        public Bitmap GetScheme(Resolution resolution)
        {

            int countX = Image.Width;
            Bitmap scheme = new Bitmap(resolution.Width, resolution.Height);

            CrissCross[,] cCrosses = GetScheme(CreateSymbols(new char[] { '$', '@', '#' }));


            int OneCrossDimention = resolution.Width / countX;

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


        public int TestMethod(int a)
        {
            if(a < 0)
            {
                throw new Exception("a < 0");
            }
            else
            {
                return a + 2;
            }
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||||||||||||||||Static Methods||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        public static Resolution[] GetResolutions(System.Drawing.Image image)//!!!!!!!!This method might be wrong
        {

            Resolution[] result = new Resolution[5];
            for (int i = 1; i <= 5; i++)
            {
                result[i - 1] = new Resolution(image.Width * i, image.Height * i);
            }

            return result;
        }

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
//||||||||||||||||||||||||||||||||Private Methods|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||

        private Color ChooseColorAmoung(Color oldColor, Color[] amoungColors)
        {
            Color resultColor;
            int min = GetDifference(oldColor, amoungColors[0]);

            resultColor = amoungColors[0];

            for(int i = 1; i < amoungColors.Length; i++)
            {
                int tempMin;
                tempMin = GetDifference(oldColor, amoungColors[i]);
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

//||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    }

}

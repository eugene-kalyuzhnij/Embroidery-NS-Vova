using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using NSEmbroidery.Core.Interfaces;
using System.Collections.Concurrent;

namespace NSEmbroidery.Core
{
    public class CanvasConverter : ICanvasConverter
    {

        public Canvas ConvertBitmapToCanvas(Bitmap image)
        {
            if (image == null) throw new NullReferenceException("'image' can't be null");

            int width = image.Width;
            int height = image.Height;

            Canvas canvas = new Canvas(new Resolution(width, height));
            
            LockBitmap lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();

            
            if (height * width > 6000)
            {
                Parallel.ForEach(Partitioner.Create(0, height), rangeHeight =>
                    {
                        for (int y = rangeHeight.Item1; y < rangeHeight.Item2; y++)
                            for (int x = 0; x < width; x++)
                                canvas.SetColor(x, y, lockBitmap.GetPixel(x, y));
                    });
            }
            else
            {
                for (int y = 0; y < height; y++)
                    for (int x = 0; x < width; x++)
                        canvas.SetColor(x, y, lockBitmap.GetPixel(x, y));
            }
            
            
            /*
            Parallel.ForEach(Partitioner.Create(0, height), rangeHeight =>
                {
                    for (int y = rangeHeight.Item1; y < rangeHeight.Item2; y++)
                        Parallel.ForEach(Partitioner.Create(0, width), rangeWidth =>
                            {
                                for (int x = 0; x < width; x++)
                                    canvas.SetColor(x, y, lockBitmap.GetPixel(x, y));
                            }
                });
            */
            lockBitmap.UnlockBits();
             
            #region Obsolete
            /*
             for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    canvas.SetColor(x, y, image.GetPixel(x, y));
             */
            #endregion

            return canvas;
        }


        public Bitmap ConvertCanvasToBitmap(Canvas canvas)
        {
            if (canvas == null) throw new NullReferenceException("'canvas' can't be null");

            int width = canvas.Width;
            int height = canvas.Height;

            Bitmap image = new Bitmap(width, height);
            
            LockBitmap lockBitmap = new LockBitmap(image);
            lockBitmap.LockBits();

            Parallel.ForEach(Partitioner.Create(0, height), rangeHeight =>
                {
                    for(int y = rangeHeight.Item1; y < rangeHeight.Item2; y++)
                         for (int x = 0; x < width; x++)
                             lockBitmap.SetPixel(x, y, canvas.GetColor(x, y));
                });

            lockBitmap.UnlockBits();
            
            #region Obsolete
            /*
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    image.SetPixel(x, y, canvas.GetColor(x, y));
            }
            */
            #endregion

            return image;
        }

    }

}

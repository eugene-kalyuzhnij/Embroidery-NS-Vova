using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Data.Models;
using System.Windows.Media.Imaging;

namespace NSEmbroidery.WPFClient
{
    static class MethodExtentions
    {
        public static int? GetNextId(this List<Embroidery> smallImages, int currentId)
        {
            int index = smallImages.IndexOf(smallImages.Where(emb => emb.Id == currentId).First());
            if (index < smallImages.Count - 1)
                return smallImages.ElementAt(index + 1).Id;
            return null;
        }

        public static int? GetPrevId(this List<Embroidery> smallImages, int currentId)
        {
            int index = smallImages.IndexOf(smallImages.Where(emb => emb.Id == currentId).First());
            if (index >= 1)
                return smallImages.ElementAt(index - 1).Id;
            return null;
        }

        public static BitmapSource GetBitmapSource(this System.Drawing.Bitmap image)
        {
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(),
                                                      IntPtr.Zero,
                                                      System.Windows.Int32Rect.Empty,
                                                      BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));


            return bitmapSource;
        }
        
    }
}

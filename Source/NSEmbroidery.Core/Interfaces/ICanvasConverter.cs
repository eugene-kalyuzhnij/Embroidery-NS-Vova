using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Interfaces
{
    public interface ICanvasConverter
    {
        Canvas ConvertBitmapToCanvas(Bitmap image);
        Bitmap ConvertCanvasToBitmap(Canvas canvas);
    }
}

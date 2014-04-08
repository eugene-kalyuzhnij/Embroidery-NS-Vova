using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core.Decorators.BorderAlgorithms
{
    public interface IBorderAlgorithm
    {
        void SetBorder(Canvas canvas, int x, int y, int width, int height, Color color, Aligns align);
    }
}

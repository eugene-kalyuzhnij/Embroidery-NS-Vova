using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core
{
    public interface IDecorator
    {
        void Decorate(Canvas embroidery, Canvas puttern);
    }
}

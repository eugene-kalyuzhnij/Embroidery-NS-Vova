using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core.Decorators
{
    public interface IDecorator
    {
        void Decorate(Canvas embroidery, Canvas pattern);
        Settings Settings { get; set; }
    }
}

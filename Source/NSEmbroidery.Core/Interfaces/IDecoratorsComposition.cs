using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSEmbroidery.Core.Decorators;
using System.Runtime.Serialization;

namespace NSEmbroidery.Core.Interfaces
{
    public interface IDecoratorsComposition
    {
        void Decorate(Canvas embroidery, Canvas pattern, Settings settings);
        void AddDecorator(IDecorator decorator);
    }
}

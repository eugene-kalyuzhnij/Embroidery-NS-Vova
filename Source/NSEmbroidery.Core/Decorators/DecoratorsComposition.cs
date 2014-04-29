using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NSEmbroidery.Core.Interfaces;
using System.Runtime.Serialization;

namespace NSEmbroidery.Core.Decorators
{

    public class DecoratorsComposition : IDecoratorsComposition
    {
        public List<IDecorator> Decorators { get; set; }

        public DecoratorsComposition()
        {
            Decorators = new List<IDecorator>();
        }

        public void AddDecorator(IDecorator decorator)
        {
            Decorators.Add(decorator);
        }

        public void Decorate(Canvas embroidery, Canvas pattern, Settings settings)
        {
            foreach (var item in Decorators)
            {
                item.Decorate(embroidery, pattern, settings);
            }
        }


    }
}

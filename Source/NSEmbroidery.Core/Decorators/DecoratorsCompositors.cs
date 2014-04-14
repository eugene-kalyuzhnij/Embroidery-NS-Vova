using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core.Decorators
{
    class DecoratorsCompositors
    {
        List<IDecorator> Decorators;
        public Settings Settings { get; set; }

        public DecoratorsCompositors()
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

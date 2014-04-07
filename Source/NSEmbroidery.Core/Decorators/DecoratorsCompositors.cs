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

        public void Decporate(Canvas embroidery, Canvas pattern)
        {
            foreach (var item in Decorators)
            {
                var decor = item as SymbolsDecorator;
                if (decor != null) decor.Settings = Settings;
                item.Decorate(embroidery, pattern);
            }
        }


    }
}

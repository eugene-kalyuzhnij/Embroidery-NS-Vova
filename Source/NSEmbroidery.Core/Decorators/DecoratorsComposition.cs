using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NSEmbroidery.Core.Interfaces;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace NSEmbroidery.Core.Decorators
{

    public class DecoratorsComposition : IDecoratorsComposition
    {
        EventLog log  = new EventLog("EmbroideryServiceLog");

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

            log.Source = "EmbroiderySource";

            foreach (var item in Decorators)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                item.Decorate(embroidery, pattern, settings);
                watch.Stop();
                log.WriteEntry("-------------------");
                log.WriteEntry("-----" + item.ToString() + ": " + watch.ElapsedMilliseconds.ToString() + Environment.NewLine +
                                "---------resol: " + embroidery.Width + "x" + embroidery.Height);
            }
        }


    }
}

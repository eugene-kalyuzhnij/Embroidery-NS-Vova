using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Injection;
using Ninject.Modules;
using NSEmbroidery.Core.Interfaces;
using NSEmbroidery.Core.Decorators;

namespace NSEmbroidery.Core
{
    class PropertiesModel : NinjectModule
    {

        public override void Load()
        {
            this.Bind<IPatternMapGenerator>().To<PatternMapGenerator>();
            this.Bind<ICanvasConverter>().To<CanvasConverter>();
        }
    }
}

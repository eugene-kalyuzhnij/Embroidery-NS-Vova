using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Ninject.Modules;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.EF;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.Data.DI.EF
{
    public class DataModelCreator : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRepository<User>>().To<UsersRepository>();
            this.Bind<IRepository<Embroidery>>().To<EmbroideriesRepository>();
        }
    }
}

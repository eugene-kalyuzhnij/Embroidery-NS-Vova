using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;

namespace NSEmbroidery.ASP.Helper
{
    public class EmbroideryIdentity
    {
        public static string EmbroideryName(int id)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            string name = kernel.Get<IRepository<Embroidery>>().GetById(id).Name;

            return name;
        }
    }
}
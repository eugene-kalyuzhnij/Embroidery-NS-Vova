using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.Interfaces;
using Ninject;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Helper
{
    public static partial class UserIdentity
    {
        public static string FirstName
        {
            get
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());

                return kernel.Get<IRepository<User>>().GetById(WebSecurity.CurrentUserId).FirstName;
            }

        }

        public static string LastName
        {
            get
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());

                return kernel.Get<IRepository<User>>().GetById(WebSecurity.CurrentUserId).LastName;
            }
        }


        public static int EmbroideriesCount(int userId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == userId);

            return embroideries.Count();
        }


        public static string GetOthersUserName(int userId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var user = kernel.Get<IRepository<User>>().GetById(userId);

            return user.FirstName + " " + user.LastName;
        }
    }
}
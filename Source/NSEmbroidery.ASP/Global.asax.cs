using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebMatrix.WebData;
using Ninject;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.DI.EF;

namespace NSEmbroidery.ASP
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
         /*   
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var users = kernel.Get<IRepository<User>>();

            users.Add(new User()
            {
                Email = "new@mail.com",
                FirstName = "first_name",
                LastName = "last_name",
                Year = DateTime.Now
            });
            
            */
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

           
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Email", autoCreateTables: true);
        }
    }
}
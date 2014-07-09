using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using Ninject;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data.SqlClient;
using WebMatrix.WebData;
using System.Diagnostics;

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            EventLog log = new EventLog("EmbroideryServiceLog");
            log.Source = "EmbroiderySource";

            if (!WebSecurity.Initialized)
            {
                try
                {
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "Id", "Email", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    log.WriteEntry(@"Could not connect to database
                                    Message:" + ex.Message +
                                                  @"/nInner Exception:" + ((ex.InnerException != null) ? ex.InnerException.Message : "null"));

                    throw new Exception(ex.Message);
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}

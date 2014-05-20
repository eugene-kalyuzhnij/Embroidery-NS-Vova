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
using Ninject;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NSEmbroidery.ASP.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [Authorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Gallery(int userId)
        {
            ViewBag.Title = "Gallery";

            IKernel kernel = new StandardKernel(new DataModelCreator());

            IEnumerable<Embroidery> embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(embr => embr.UserId == userId);

            return View(embroideries);
        }
    }
}

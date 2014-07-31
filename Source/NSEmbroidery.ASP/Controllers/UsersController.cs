using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.DI.EF;
using System.IO;
using System.Drawing;
using NSEmbroidery.ASP.Filters;
using WebMatrix.WebData;
using Ninject;


namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            IEnumerable<User> users = kernel.Get<IRepository<User>>().GetAll().Where(u => u.Id != WebSecurity.CurrentUserId);

            return View(users);
        }

        [HttpPost]
        public ActionResult OtherUser(int userId)
        {
            if (userId == WebSecurity.CurrentUserId)
                return RedirectToAction("Index", "Gallery");

            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == userId && e.PublicEmbroidery == true);

            ViewBag.UserName = kernel.Get<IRepository<User>>().GetById(userId).FirstName;

            ViewBag.UserId = userId;

            return View(embroideries);
        }

    }
}
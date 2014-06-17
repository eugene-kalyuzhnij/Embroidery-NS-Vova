using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize(Users="admin@mail.com")]
    public class ManagementController : Controller
    {
        //
        // GET: /Management/

        public ActionResult Index()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var users = kernel.Get<IRepository<User>>().GetAll().Where(u => u.Id != WebSecurity.CurrentUserId);

            return View(users);
        }


        [HttpPost]
        public ActionResult UserContent(int userId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == userId);

            var comments = kernel.Get<IRepository<Comment>>().GetAll().Where(c => c.UserId == userId);

            ViewBag.Comments = comments;
            ViewBag.Embroideries = embroideries;

            return View();
        }


        [HttpPost]
        public void DeleteEmbroidery(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(embroideryId);

            kernel.Get<IRepository<Embroidery>>().Remove(embroidery);

        }

    }
}

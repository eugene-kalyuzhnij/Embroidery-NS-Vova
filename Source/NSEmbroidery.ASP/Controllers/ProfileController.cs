using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.DI.EF;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var comments = kernel.Get<IRepository<Comment>>().GetAll();
            var lastComments = comments.Where(com => com.UserId == WebSecurity.CurrentUserId).Skip((int)Math.Max(0, comments.Count - 5));

            ViewBag.LastComments = lastComments;

            return View();
        }


        [HttpPost]
        public ActionResult GetLastComments()
        {
           

            
        }

    }
}

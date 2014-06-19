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
    [Authorize(Roles="Admin")]
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


        [HttpGet]
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
        public ActionResult DeleteEmbroidery(int embroideryId)
        {        
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(embroideryId);

                kernel.Get<IRepository<Embroidery>>().Remove(embroidery);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false });
            }
            
            return Json(new { Result = true });
        }



        [HttpPost]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var comment = kernel.Get<IRepository<Comment>>().GetById(commentId);

                kernel.Get<IRepository<Comment>>().Remove(comment);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false });
            }

            return Json(new { Result = true });
        }


        [HttpPost]
        public ActionResult DeleteUser(int userId)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var user = kernel.Get<IRepository<User>>().GetById(userId);

                kernel.Get<IRepository<User>>().Remove(user);
            }
            catch (Exception ex)
            {
                return Json(new { Result = false });
            }

            return Json(new { Result = true });
        }


    }
}

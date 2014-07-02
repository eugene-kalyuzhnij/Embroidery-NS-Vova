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
using System.IO;
using System.Drawing;

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            var lastComments = GetLastComments();
            var lastLikes = GetLastLikes();

            ViewBag.LastLikes = lastLikes;
            ViewBag.LastComments = lastComments;

            return View();
        }

        public IEnumerable<Comment> GetLastComments()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == WebSecurity.CurrentUserId);

            var comments = kernel.Get<IRepository<Comment>>().GetAll().Where(c =>
            {
                if (WebSecurity.CurrentUserId != c.UserId)
                {
                    foreach (var item in embroideries)
                        if (item.Id == c.EmbroideryId) return true;
                }
                return false;
            }).OrderBy(c => c.DateCreated).Reverse();


            return comments.Take(5);
        }


        public IEnumerable<Like> GetLastLikes()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == WebSecurity.CurrentUserId);

            var likes = kernel.Get<IRepository<Like>>().GetAll().Where(l =>
            {
                if (WebSecurity.CurrentUserId != l.UserId)
                {
                    foreach (var item in embroideries)
                        if (item.Id == l.EmbroideryId) return true;
                }
                return false;
            });

            return likes.Skip((int)Math.Max(0, likes.Count() - 5));
        }

    }

}

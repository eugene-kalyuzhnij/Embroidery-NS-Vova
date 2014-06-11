using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.DI.EF;
using System.IO;
using System.Drawing;
using NSEmbroidery.ASP.Filters;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/

        public ActionResult Index()
        {
            ViewBag.Title = "Gallery";

            IKernel kernel = new StandardKernel(new DataModelCreator());
            IEnumerable<Embroidery> embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(embr => embr.UserId == WebSecurity.CurrentUserId);

            return View(embroideries);
        }

        public FileContentResult ShowImage(int id)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(id);

            byte[] array = null;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap image = embroidery.SmallImage;
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                array = stream.ToArray();
            }

            string contentType = "image/jpeg";

            return File(array, contentType);
        }



        [HttpPost]
        public ActionResult GetEmbroidery(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideryFromData = kernel.Get<IRepository<Embroidery>>().GetById(embroideryId);

            var embroidery = embroideryFromData.Image;

            byte[] imageBytes = null;

            using (MemoryStream resultStream = new MemoryStream())
            {
                embroidery.Save(resultStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = resultStream.ToArray();
            }
            string base64 = Convert.ToBase64String(imageBytes);

            bool alloweChangePublic = false;
            if (embroideryFromData.UserId == WebSecurity.CurrentUserId)
                alloweChangePublic = true;

            var jsonResult = Json(new { imageString = "data:image/jpeg;base64," + base64, allowePublic = embroideryFromData.PublicEmbroidery, alloweChangePublic = alloweChangePublic}, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }


        [HttpPost]
        public ActionResult GetComments(int EmbroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            List<Comment> comments = kernel.Get<IRepository<Comment>>().GetAll().Where(comment => comment.EmbroideryId == EmbroideryId).ToList();

            List<Object> result = new List<Object>();

            foreach (var item in comments)
            {
                User currentUser = kernel.Get<IRepository<User>>().GetById(item.UserId);
                string userName = currentUser.FirstName + " " + currentUser.LastName;
                result.Add(new { UserId = item.UserId, Comment = item.Comment_msg, UserName = userName });
            }

            return Json(result);
        }


        [HttpPost]
        public void AddComment(int EmbroideryId, string comment)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var comments = kernel.Get<IRepository<Comment>>();

            comments.Add(new Comment() { Comment_msg = comment, EmbroideryId = EmbroideryId, UserId = WebSecurity.CurrentUserId });
        }



        [HttpPost]
        public int GetLikesCount(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var likes = kernel.Get<IRepository<Like>>().GetAll().Where(l => l.EmbroideryId == embroideryId);

            int likesCount = likes.Count();

            return likesCount;
        }

        [HttpPost]
        public bool CanAddLike(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var likes = kernel.Get<IRepository<Like>>().GetAll().Where(l => l.EmbroideryId == embroideryId);

            bool canAddLike = true;

            if (likes.Where(l => l.UserId == WebSecurity.CurrentUserId).Count() > 0) canAddLike = false;

            return canAddLike;
        }


        [HttpPost]
        public void AddLike(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            kernel.Get<IRepository<Like>>().Add(new Like() { UserId = WebSecurity.CurrentUserId, EmbroideryId = embroideryId });
        }


        [HttpPost]
        public void RemoveLike(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var like = kernel.Get<IRepository<Like>>().GetAll().Where(l => l.EmbroideryId == embroideryId && l.UserId == WebSecurity.CurrentUserId).First();

            kernel.Get<IRepository<Like>>().Remove(like);
        }

        [HttpPost]
        public ActionResult GetLikesUsers(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var likes = kernel.Get<IRepository<Like>>().GetAll().Where(l => l.EmbroideryId == embroideryId);

            List<Object> result = new List<object>();

            foreach (var item in likes)
            {
                User user = kernel.TryGet<IRepository<User>>().GetById(item.UserId);
                result.Add(new { UserName = user.FirstName + " " + user.LastName, UserId = item.UserId });
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteEmbroidery(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroideries = kernel.Get<IRepository<Embroidery>>();
            Embroidery current = embroideries.GetById(embroideryId);

            if (current.UserId == WebSecurity.CurrentUserId)
                embroideries.Remove(current);
            else throw new Exception("You can't delete this embroidery :(");


            return RedirectToAction("Index", "Gallery");
        }


        [HttpPost]
        public ActionResult ChangeEmbroideryAllow(int embroideryId, bool newAllow)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideryContext = kernel.Get<IRepository<Embroidery>>();
            var embroidery = embroideryContext.GetById(embroideryId);

            if (embroidery.UserId == WebSecurity.CurrentUserId)
            {
                embroidery.PublicEmbroidery = newAllow;
                embroideryContext.SaveChanges(embroidery);
                return Json(new { result = true });
            }

            return Json(new { result = false });
        }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using System.Drawing;
using System.IO;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.DI.EF;
using WebMatrix.WebData;
using NSEmbroidery.ASP.Filters;
using NSEmbroidery.ASP;


namespace NSEmbroidery.ASP.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            

            return View();
        }


        [HttpGet]
        public ActionResult Gallery()
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
                    Bitmap image = embroidery.Image;
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    array = stream.ToArray();
                }

                string contentType = "image/jpeg";

                return File(array, contentType);
        }



        

        [HttpGet]
        public ActionResult AddEmbroidery()
        {
            List<SelectListItem> objects = new List<SelectListItem>();
            ViewBag.Items = objects;

            return View();
        }


        [HttpPost]
        public ActionResult AddEmbroidery(HttpPostedFileBase file)
        {
            Bitmap imageFromFile = new Bitmap(file.InputStream);

            byte[] array = null;

            using (MemoryStream stream = new MemoryStream())
            {
                Bitmap image = imageFromFile;
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                array = stream.ToArray();
            }

            string contentType = "image/jpeg";

            return File(array, contentType);

        }

        [HttpGet]
        public ActionResult DeleteEmbroidery(int embroideryId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var embroideries = kernel.Get<IRepository<Embroidery>>();
            Embroidery current = embroideries.GetById(embroideryId);

            if(current.UserId == WebSecurity.CurrentUserId)
                embroideries.Remove(current);
            
            return RedirectToAction("Gallery");
        }


        public ActionResult Users()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            IEnumerable<User> users = kernel.Get<IRepository<User>>().GetAll().Where(u => u.Id != WebSecurity.CurrentUserId);

            return View(users);
        }


        public ActionResult OtherUser(int userId)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll().Where(e => e.UserId == userId);

            return View(embroideries);
        }

        [HttpPost]
        public ActionResult GetResolutions(int cells, string img)
        {
            string imageDataParsed = img.Substring(img.IndexOf(',') + 1);
            byte[] imageBytes = Convert.FromBase64String(imageDataParsed);

            string contentType = "image/jpeg";

            return File(imageBytes, contentType);
        }

    }
}

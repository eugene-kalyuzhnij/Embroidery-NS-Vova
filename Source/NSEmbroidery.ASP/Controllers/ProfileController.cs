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
using NSEmbroidery.ASP.EmbroideryCreatorService;


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
        public ActionResult AddEmbroidery(string img)
        {
            string imageDataParsed = img.Substring(img.IndexOf(',') + 1);
            byte[] imageBytes = Convert.FromBase64String(imageDataParsed);

            Bitmap image = null;

            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                Bitmap _img = new Bitmap(stream);

                image = new Bitmap(_img.Width, _img.Height);

                for (int y = 0; y < _img.Height; y++)
                    for (int x = 0; x < _img.Width; x++)
                        image.SetPixel(x, y, _img.GetPixel(x, y));
            }

            IKernel kernel = new StandardKernel(new DataModelCreator());

            var embroideries = kernel.Get<IRepository<Embroidery>>();

            embroideries.Add(new Embroidery(image) { Name = "new Image", UserId = WebSecurity.CurrentUserId });

            return RedirectToAction("Gallery", "Profile");
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


            Dictionary<string, int> resolList = new Dictionary<string, int>();

            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                Bitmap image = new Bitmap(stream);

                EmbroideryCreatorServiceClient client = new EmbroideryCreatorServiceClient();
                resolList = client.PossibleResolutionsCount(image, cells, 10);
            }


            
            List<SelectListItem> objects = new List<SelectListItem>();
            foreach (var item in resolList)
                objects.Add(new SelectListItem() { Text = item.Key, Value = item.Value.ToString() });

            
            return Json(new SelectList(objects, "Value", "Text"));
        }


        [HttpPost]
        public JsonResult CreateEmbroidery(string img, int coefficient, int cellsCount, Color[] colors, char[] symbols, Color symbolColor, bool grid)
        {
            string imageDataParsed = img.Substring(img.IndexOf(',') + 1);
            byte[] imageBytes = Convert.FromBase64String(imageDataParsed);

            Bitmap image = null;

            using (MemoryStream stream = new MemoryStream(imageBytes))
            {

                Bitmap _img = new Bitmap(stream);

                image = new Bitmap(_img.Width, _img.Height);

                for (int y = 0; y < _img.Height; y++)
                    for (int x = 0; x < _img.Width; x++)
                        image.SetPixel(x, y, _img.GetPixel(x, y));

            }

            EmbroideryCreatorServiceClient proxy = new EmbroideryCreatorServiceClient();

            Bitmap result = proxy.GetEmbroidery(image, coefficient, cellsCount, colors, null, Color.Black, GridType.SolidLine);

            using (MemoryStream resultStream = new MemoryStream())
            {
                result.Save(resultStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = resultStream.ToArray();
            }

            string base64 = Convert.ToBase64String(imageBytes);

            return Json(new { imageString = base64 });
        }





    }

}


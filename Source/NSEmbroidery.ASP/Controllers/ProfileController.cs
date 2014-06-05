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

        Bitmap _uploadedImage;

        public ActionResult Index()
        {
            return View();
        }

        
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


            return RedirectToAction("Gallery");
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

            if (userId == WebSecurity.CurrentUserId)
            {
                return View("Gallery", embroideries);
            }

            ViewBag.UserName = kernel.Get<IRepository<User>>().GetById(userId).FirstName;

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
        public ActionResult CreateEmbroidery(string img, int coefficient, int cellsCount, string colors, string symbols, string symbolColor, bool grid)
        {
            var inputColors = ParseFromString(colors);
            //var inputSymbolColor = Color.FromArgb(Convert.ToInt32(symbolColor));

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

            GridType type = GridType.None;
            if (grid) type = GridType.SolidLine;

            Bitmap result = proxy.GetEmbroidery(image, coefficient, cellsCount, inputColors, null, Color.Black, type);

            using (MemoryStream resultStream = new MemoryStream())
            {
                result.Save(resultStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = resultStream.ToArray();
            }

            string base64 = Convert.ToBase64String(imageBytes);

            var jsonResult = Json(new { imageString = base64 }, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = Int32.MaxValue;

            return jsonResult;
        }


        private Color[] ParseFromString(string colors)
        {
            colors = colors.Replace("#", "");
            string[] splitColors = colors.Split(',');


            Color[] resultColors = new Color[splitColors.Length];

            int i = 0;
            foreach (var color in splitColors)
            {
                int colorInt = int.Parse(color, System.Globalization.NumberStyles.HexNumber);
                resultColors[i++] = Color.FromArgb(colorInt);

            }

            return resultColors;
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
                result.Add(new { UserId = item.UserId, Comment = item.Comment_msg, UserName = userName});
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

            foreach(var item in likes)
            {
                User user = kernel.TryGet<IRepository<User>>().GetById(item.UserId);
                result.Add(new { UserName = user.FirstName + " " + user.LastName, UserId = item.UserId });
            }

            return Json(result);
        }


        [HttpPost]
        public FileContentResult UploadImage(HttpPostedFileBase file)
        {
            Bitmap image = null;

            using (file.InputStream)
            {
                image = (Bitmap)Image.FromStream(file.InputStream);
            }

            _uploadedImage = image;

            byte[] imageBytes = null;

            using (MemoryStream stream = new MemoryStream())
            {
                _uploadedImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                imageBytes = stream.ToArray();
            }

            return File(imageBytes, "image/jpeg");
            
        }

    }

}


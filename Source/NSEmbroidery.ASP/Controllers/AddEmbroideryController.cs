using Ninject;
using Ninject.Modules;
using Ninject.Parameters;
using NSEmbroidery.ASP.EmbroideryCreatorService;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Controllers
{

        [Authorize]
        public class AddEmbroideryController : Controller
        {
            [HttpPost]
            public ActionResult AddEmbroidery(string img, bool allowePublic, string name)
            {
                byte[] buffer = Convert.FromBase64String(img.Substring(img.IndexOf(',') + 1));
                Bitmap image = null;
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    Bitmap bitmap2 = new Bitmap(stream);
                    image = new Bitmap(bitmap2.Width, bitmap2.Height);
                    for (int i = 0; i < bitmap2.Height; i++)
                    {
                        for (int j = 0; j < bitmap2.Width; j++)
                        {
                            image.SetPixel(j, i, bitmap2.GetPixel(j, i));
                        }
                    }
                }
                IKernel root = new StandardKernel(new INinjectModule[] { new DataModelCreator() });
                IRepository<Embroidery> repository = root.Get<IRepository<Embroidery>>(new IParameter[0]);
                Size newSize = this.GetNewSize(image, 150);
                Embroidery item = new Embroidery(image, newSize)
                {
                    Name = name,
                    UserId = WebSecurity.CurrentUserId,
                    DateCreated = DateTime.Now,
                    PublicEmbroidery = allowePublic
                };
                repository.Add(item);
                return base.RedirectToAction("Index", "Gallery");
            }

            [HttpPost]
            public ActionResult CreateEmbroidery(string img, int coefficient, int cellsCount, string colors, string symbols, string symbolColor, bool grid)
            {
                Color[] palette = this.ParseFromStringColors(colors);
                Color color = this.ParseFromStringColors(symbolColor).First<Color>();
                char[] chArray = null;
                if (symbols != "")
                {
                    chArray = this.ParseFromStringSymbols(symbols);
                }
                byte[] buffer = Convert.FromBase64String(img.Substring(img.IndexOf(',') + 1));
                Bitmap image = null;
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    Bitmap bitmap2 = new Bitmap(stream);
                    image = new Bitmap(bitmap2.Width, bitmap2.Height);
                    for (int i = 0; i < bitmap2.Height; i++)
                    {
                        for (int j = 0; j < bitmap2.Width; j++)
                        {
                            image.SetPixel(j, i, bitmap2.GetPixel(j, i));
                        }
                    }
                }
                EmbroideryCreatorServiceClient client = new EmbroideryCreatorServiceClient();
                GridType none = GridType.None;
                if (grid)
                {
                    none = GridType.SolidLine;
                }
                Bitmap bitmap3 = client.GetEmbroidery(image, coefficient, cellsCount, palette, chArray, color, none);
                using (MemoryStream stream2 = new MemoryStream())
                {
                    bitmap3.Save(stream2, ImageFormat.Jpeg);
                    buffer = stream2.ToArray();
                }
                string str2 = Convert.ToBase64String(buffer);
                JsonResult result = base.Json(new { imageString = str2 }, JsonRequestBehavior.AllowGet);
                result.MaxJsonLength = 0x7fffffff;
                return result;
            }

            private Size GetNewSize(Image image, int width)
            {
                int num = (width * 100) / image.Width;
                return new Size(width, (num * image.Height) / 100);
            }

            [HttpPost]
            public ActionResult GetResolutions(int cells, string img)
            {
                byte[] buffer = Convert.FromBase64String(img.Substring(img.IndexOf(',') + 1));
                Dictionary<string, int> dictionary = new Dictionary<string, int>();
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    Bitmap image = new Bitmap(stream);
                    dictionary = new EmbroideryCreatorServiceClient().PossibleResolutions(image, cells, 3, 12);
                }
                List<SelectListItem> items = new List<SelectListItem>();
                foreach (KeyValuePair<string, int> pair in dictionary)
                {
                    SelectListItem item = new SelectListItem
                    {
                        Text = pair.Key,
                        Value = pair.Value.ToString()
                    };
                    items.Add(item);
                }
                return base.Json(new SelectList(items, "Value", "Text"));
            }

            [HttpGet]
            public ActionResult Index()
            {
                List<SelectListItem> list = new List<SelectListItem>();
                ((dynamic)base.ViewBag).Items = list;
                return base.View();
            }

            private Color[] ParseFromStringColors(string colors)
            {
                colors = colors.Replace("#", "ff");
                string[] strArray = colors.Split(new char[] { ',' });
                Color[] colorArray = new Color[strArray.Length];
                int num = 0;
                foreach (string str in strArray)
                {
                    int argb = int.Parse(str, NumberStyles.HexNumber);
                    colorArray[num++] = Color.FromArgb(argb);
                }
                return colorArray;
            }

            private char[] ParseFromStringSymbols(string symbols)
            {
                string[] strArray = symbols.Split(new char[] { ',' });
                char[] chArray = new char[strArray.Length];
                for (int i = 0; i < chArray.Length; i++)
                {
                    chArray[i] = strArray[i].Single<char>();
                }
                return chArray;
            }
        }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.DI.EF;
using Ninject;
using System.Diagnostics;
using System.Drawing;
using Newtonsoft.Json;

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class EmbroideriesController : ApiController
    {
        [HttpGet]
        public List<Embroidery> GetAllEmbroideries()
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll();

                return embroideries.ToList();
            }
            catch(Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        public List<Embroidery> GetAllEmbroidery(bool small)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                if (small)
                {
                    IKernel kernel = new StandardKernel(new DataModelCreator());
                    var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll();
                    List<Embroidery> result = new List<Embroidery>();

                    foreach (var item in embroideries)
                        result.Add(new Embroidery()
                        {
                            SmallImageData = item.SmallImageData,
                            UserId = item.UserId,
                            Id = item.Id
                        });

                    return result;
                }
            }
            catch(Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            

            return null;
        }

        [HttpGet]
        public Embroidery GetEmbroidery(int id)
        {
            EventLog log = null;
            Embroidery embroidery = null;

            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                embroidery = kernel.Get<IRepository<Embroidery>>().GetById(id);

            }
            catch (Exception ex)
            {
                log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            if (embroidery == null)
              throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return embroidery;
        }

        [HttpPost]
        public HttpResponseMessage PostEmbroidery([FromBody]Embroidery embroidery)
        {

            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
              
                    #region Creating Small Image
                    Bitmap image = embroidery.GetImage();
                    if (image != null)
                    {
                        int smallImageWidht = 150;

                        int num = (smallImageWidht * 100) / image.Width;
                        Size size = new Size(smallImageWidht, (num * image.Height) / 100);

                        if(!embroidery.CreateSmallImage(size))
                            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotModified));
                    }
                    else
                        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
                    #endregion

                embroidery.DateCreated = DateTime.Now;

                var repository = kernel.Get<IRepository<Embroidery>>();
                repository.Add(embroidery);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                if (log != null)
                {
                    string str_exception = "";
                    str_exception += "Exception: " + ex.Message + Environment.NewLine;
                    if (ex.InnerException.InnerException != null)
                        str_exception += @"  Inner exception: " + ex.InnerException.InnerException.Message;

               

                    log.WriteEntry(str_exception);
                }
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEmbroidery(int id)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(id);
                kernel.Get<IRepository<Embroidery>>().Remove(embroidery);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

    }
}

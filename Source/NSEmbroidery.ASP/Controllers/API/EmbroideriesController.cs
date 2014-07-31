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

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class EmbroideriesController : ApiController
    {
        [HttpGet]
        public List<Embroidery> GetAllEmbroideries()
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll();

                return embroideries.ToList();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        public List<Embroidery> GetAllEmbroidery(bool small)
        {
           
            if (small)
            {
                try
                {
                    IKernel kernel = new StandardKernel(new DataModelCreator());
                    var embroideries = kernel.Get<IRepository<Embroidery>>().GetAll();
                    List<Embroidery> result = new List<Embroidery>();

                    foreach (var item in embroideries)
                        result.Add(new Embroidery()
                        {
                            SmallImageData = item.SmallImageData
                        });

                    return result;
                }
                catch
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
                }
            }

            return null;
        }


        [HttpGet]
        public Embroidery GetEmbroidery(int id)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(id);

                return embroidery;
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddEmbroidery([FromBody]Embroidery embroidery)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                kernel.Get<IRepository<Embroidery>>().Add(embroidery);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
        


        [HttpDelete]
        public HttpResponseMessage DeleteEmbroidery(int id)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var embroidery = kernel.Get<IRepository<Embroidery>>().GetById(id);
                kernel.Get<IRepository<Embroidery>>().Remove(embroidery);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

    }
}

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
using Newtonsoft.Json;

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class LikesController : ApiController
    {
        [HttpGet]
        public List<Like> GetAllLikes()
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var likes = kernel.Get<IRepository<Like>>().GetAll().ToList();

                return likes;
            }
            catch (Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddLike(int userId, int embroideryId)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                kernel.Get<IRepository<Like>>().Add(new Like()
                {
                    EmbroideryId = embroideryId,
                    UserId = userId
                });
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                if(log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }



        [HttpDelete]
        public HttpResponseMessage DeleteLike(int userId, int embroideryId)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                Like like = kernel.Get<IRepository<Like>>().GetAll().Where(l => (l.UserId == userId && l.EmbroideryId == embroideryId)).First();
                kernel.Get<IRepository<Like>>().Remove(like);

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

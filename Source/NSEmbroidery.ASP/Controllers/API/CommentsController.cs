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

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class CommentsController : ApiController
    {
        [HttpGet]
        public List<Comment> GetAllComments()
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var comments = kernel.Get<IRepository<Comment>>().GetAll();

                return comments.ToList();
            }
            catch (Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        public Comment GetComment(int id)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var comment = kernel.Get<IRepository<Comment>>().GetById(id);

                if (comment == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

                return comment;
            }
            catch (Exception ex)
            {
                if(log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddComment(string msg, int userId, int embroideryId)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                kernel.Get<IRepository<Comment>>().Add(new Comment()
                {
                    Comment_msg = msg,
                    DateCreated = DateTime.Now,
                    EmbroideryId = embroideryId,
                    UserId = userId
                });
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch(Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteComment(int id)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var comment = kernel.Get<IRepository<Comment>>().GetById(id);
                kernel.Get<IRepository<Comment>>().Remove(comment);

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

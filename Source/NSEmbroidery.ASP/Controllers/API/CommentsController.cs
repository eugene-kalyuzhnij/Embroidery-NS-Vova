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
    public class CommentsController : ApiController
    {
        [HttpGet]
        public List<Comment> GetAllComments()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var comments = kernel.Get<IRepository<Comment>>().GetAll();

            return comments.ToList();
        }

        [HttpGet]
        public Comment GetComment(int id)
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var comment = kernel.Get<IRepository<Comment>>().GetById(id);

            return comment;
        }

        [HttpPost]
        public HttpResponseMessage AddComment(string msg, int userId, int embroideryId)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                kernel.Get<IRepository<Comment>>().Add(new Comment()
                {
                    Comment_msg = msg,
                    DateCreated = DateTime.Now,
                    EmbroideryId = embroideryId,
                    UserId = userId
                });
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteComment(int id)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var comment = kernel.Get<IRepository<Comment>>().GetById(id);
                kernel.Get<IRepository<Comment>>().Remove(comment);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}

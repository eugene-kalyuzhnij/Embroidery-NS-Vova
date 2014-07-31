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
    public class LikesController : ApiController
    {
        [HttpGet]
        public List<Like> GetAllLikes()
        {
            IKernel kernel = new StandardKernel(new DataModelCreator());
            var likes = kernel.Get<IRepository<Like>>().GetAll();

            return likes.ToList();
        }

        [HttpPost]
        public HttpResponseMessage AddLike(int userId, int embroideryId)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                kernel.Get<IRepository<Like>>().Add(new Like()
                {
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
        public HttpResponseMessage DeleteLike(int userId, int embroideryId)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                Like like = kernel.Get<IRepository<Like>>().GetAll().Where(l => (l.UserId == userId && l.EmbroideryId == embroideryId)).First();
                kernel.Get<IRepository<Like>>().Remove(like);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using NSEmbroidery.Data.Models;
using Ninject;

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class UsersController : ApiController
    {
        [HttpGet]
        public List<User> GetAllUser()
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var users = kernel.Get<IRepository<User>>().GetAll();
                return users.ToList();
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        public User GetUser(int id)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var user = kernel.Get<IRepository<User>>().GetById(id);

                return user;
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                var user = kernel.Get<IRepository<User>>().GetById(id);
                kernel.Get<IRepository<User>>().Remove(user);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

    }
}

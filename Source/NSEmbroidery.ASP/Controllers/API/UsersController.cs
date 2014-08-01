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
using System.Diagnostics;

namespace NSEmbroidery.ASP.Controllers.API
{
    [Authorize]
    public class UsersController : ApiController
    {
        [HttpGet]
        public List<User> GetAllUser()
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var users = kernel.Get<IRepository<User>>().GetAll();
                return users.ToList();
            }
            catch (Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        public User GetUser(int id)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var user = kernel.Get<IRepository<User>>().GetById(id);

                if (user == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

                return user;
            }
            catch (Exception ex)
            {
                if (log != null) log.WriteEntry("Exception: " + ex.Message);
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }


        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                IKernel kernel = new StandardKernel(new DataModelCreator());
                var user = kernel.Get<IRepository<User>>().GetById(id);

                if (user == null)
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

                kernel.Get<IRepository<User>>().Remove(user);

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

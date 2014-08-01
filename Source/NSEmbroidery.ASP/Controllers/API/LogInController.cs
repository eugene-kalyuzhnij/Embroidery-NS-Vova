using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.Models;
using WebMatrix.WebData;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.Interfaces;
using Ninject;
using System.Diagnostics;

namespace NSEmbroidery.ASP.Controllers.API
{
    public class LogInController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {

                if(ModelState.IsValid)
                    if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK);;
                    }

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        [HttpGet]
        public User GetCurrentUser()
        {
            User user = null;
            try
            {
                IKernel kernel = new StandardKernel(new DataModelCreator());
                user = kernel.Get<IRepository<User>>().GetById(WebSecurity.CurrentUserId);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            if (user == null)
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return user;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.Models;
using WebMatrix.WebData;

namespace NSEmbroidery.ASP.Controllers.API
{
    public class LogInController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid && WebSecurity.Login(model.Email, model.Password, persistCookie: model.RememberMe))
                    return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch(Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.BadRequest));
        }
    }
}

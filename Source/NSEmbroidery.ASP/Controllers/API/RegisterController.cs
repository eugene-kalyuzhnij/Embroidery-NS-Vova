using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.Models;
using WebMatrix.WebData;
using System.Web.Security;
using System.Diagnostics;
using NSEmbroidery.Data.Models;
using NSEmbroidery.Data.DI.EF;
using NSEmbroidery.Data.Interfaces;
using Ninject;

namespace NSEmbroidery.ASP.Controllers.API
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public User Register([FromBody] RegisterModel model)
        {
            EventLog log = null;
                try
                {
                    log = new EventLog("NS.Server");
                    log.Source = "NS.Server.Source";

                    WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                        new { FirstName = model.FirstName, LastName = model.LastName });
                    WebSecurity.Login(model.Email, model.Password);

                    IKernel kernel = new StandardKernel(new DataModelCreator());
                    User user = kernel.Get<IRepository<User>>().GetById(WebSecurity.CurrentUserId);

                    return user;
                }
                catch (MembershipCreateUserException ex)
                {
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict));
                }
                catch(Exception ex)
                {
                    if(log != null) log.WriteEntry("Exception: " + ex.Message);
                    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
                }
                
        }
    }
}

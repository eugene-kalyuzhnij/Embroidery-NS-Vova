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
    public class RegisterController : ApiController
    {
        [HttpPost]
        public bool Register([FromBody] RegisterModel model)
        {
            //if (ModelState.IsValid)
            //{
                try
                {
                    WebSecurity.CreateUserAndAccount(model.Email, model.Password,
                        new { FirstName = model.FirstName, LastName = model.LastName });
                    WebSecurity.Login(model.Email, model.Password);
                    return true;

                }
                catch
                {
                    return false;
                }
            //}
        }
    }
}

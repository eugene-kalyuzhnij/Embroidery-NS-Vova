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
    public class LogOffController : ApiController
    {
        [HttpPost]
        public bool LogOff()
        {
            try
            {
                WebSecurity.Logout();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

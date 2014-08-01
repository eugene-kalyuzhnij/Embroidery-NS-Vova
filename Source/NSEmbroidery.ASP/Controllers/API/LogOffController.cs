using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NSEmbroidery.Data.Models;
using WebMatrix.WebData;
using System.Diagnostics;

namespace NSEmbroidery.ASP.Controllers.API
{
    public class LogOffController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage LogOff()
        {
            EventLog log = null;
            try
            {
                log = new EventLog("NS.Server");
                log.Source = "NS.Server.Source";

                WebSecurity.Logout();
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

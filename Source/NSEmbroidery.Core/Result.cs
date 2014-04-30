using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;

namespace NSEmbroidery.Core
{
    [MessageContract]
    public class Result
    {
        [MessageBodyMember(Order=1)]
        public Stream ImageStream { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core
{

    public class WrongSymbolsRealisationException : ApplicationException
    {
        public WrongSymbolsRealisationException()
            : base()
        {
        }

        public WrongSymbolsRealisationException(string msg)
            : base(msg)
        {
        }
    }


    public class NotInitializedException : ApplicationException
    {
        public NotInitializedException()
            : base()
        {
        }

        public NotInitializedException(string msg)
            : base(msg)
        {
        }
    }


    public class WrongResolutionException : ApplicationException
    {
        public WrongResolutionException()
            : base()
        {
        }

        public WrongResolutionException(string msg)
            : base(msg)
        {
        }
    }
    

}

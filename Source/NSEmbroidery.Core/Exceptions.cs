using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core
{
    public class NotImplementResolutionException : ApplicationException
    {
        public NotImplementResolutionException()
            : base()
        {
        }

        public NotImplementResolutionException(string msg)
            : base(msg)
        {
        }
    }

    public class NotImplementPaletteException : ApplicationException
    {
        public NotImplementPaletteException()
            : base()
        {
        }

        public NotImplementPaletteException(string msg)
            : base(msg)
        {
        }
    }


    public class NotImplementSquaresException : ApplicationException
    {
        public NotImplementSquaresException()
            : base()
        {
        }

        public NotImplementSquaresException(string msg)
            : base(msg)
        {
        }
    }

}

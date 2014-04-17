using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSEmbroidery.Core.Interfaces
{
    public interface IPatternMapGenerator
    {
        Canvas Generate(Canvas canvas, Settings settings);
    }
}

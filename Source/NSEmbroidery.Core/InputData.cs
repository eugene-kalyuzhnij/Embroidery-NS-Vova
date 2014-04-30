using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.IO;
using System.Drawing;

namespace NSEmbroidery.Core
{
    [MessageContract]
    public class InputData : IDisposable
    {
        [MessageBodyMember(Order=1)]
        public Stream InputImageStream { get; set; }

        [MessageHeader]
        public int ResolutionCoefficient { get; set; }

        [MessageHeader]
        public int CellsCount { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public Color[] Palette { get; set; }

        [MessageHeaderArray]
        public char[] Symbols { get; set; }

        [MessageHeader(Namespace="System.Drawing")]
        public Color SymbolColor { get; set; }

        [MessageHeader(MustUnderstand = true)]
        public GridType GridType { get; set; }



        public void Dispose()
        {
            if (InputImageStream != null)
            {
                InputImageStream.Close();
                InputImageStream = null;
            }

        }
    }
}

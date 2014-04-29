using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NSEmbroidery.Core
{
    [Serializable]
    [DataContract]
    public class Resolution
    {
        [DataMember]
        public int Height { get; set; }
        [DataMember]
        public int Width { get; set; }

        public Resolution(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public void SetResolution(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return Width.ToString() + "x" + Height.ToString();
        }
    }
}

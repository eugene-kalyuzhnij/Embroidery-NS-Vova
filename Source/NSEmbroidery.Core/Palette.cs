using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.Serialization;

namespace NSEmbroidery.Core
{
    [Serializable]
    [DataContract]
    public class Palette
    {
        Color[] colors;

        [DataMember]
        public int Count {
            get{
                    if (colors != null)
                        return colors.Length;
                    else return 0;
               }
        }

        [DataMember]
        public Color[] Colors{
            private get { return colors; }
            set { colors = value; }
        }

        public Palette() { }

        public List<Color> GetAllColorsList()
        {
            return colors.ToList();
        }
        
        public Palette(Color[] colors)
        {
            this.colors = colors;
        }

        public Palette(List<Color> colors)
        {
            this.colors = new Color[colors.Count];
            for (int i = 0; i < colors.Count; i++)
                this.colors[i] = colors[i];
        }

        public Color[] GetAllColors()
        {
            if (this.colors == null || this.Count == 0) throw new WrongInitializedException("Add colors to palette first");

            Color[] result = new Color[Count];
            for (int i = 0; i < Count; i++)
                result[i] = colors[i];

            return result;
        }

        
    }
}

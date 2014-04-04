﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public class Palette
    {

        List<Color> colors;

        public int Count {
            get{return colors.Count;}
        }

        public List<Color> GetAllColors()
        {
            return colors;
        }

        public Palette()
        {
            colors = new List<Color>();
        }

        public Palette(Color[] colors):this()
        {
            foreach (var color in colors)
            {
                this.colors.Add(color);
            }
        }

        public Palette(List<Color> colors)
        {
            this.colors = colors;
        }

        

    }
}
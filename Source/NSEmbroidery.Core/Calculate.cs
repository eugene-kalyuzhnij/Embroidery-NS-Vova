﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace NSEmbroidery.Core
{
    public static class Calculate
    {
        public static Dictionary<Resolution, int> PossibleResolutions(Bitmap image, int cellsCount, int countResolutions)
        {
            Dictionary<Resolution, int> result = new Dictionary<Resolution, int>();

            if (cellsCount <= 0)
                throw new NotInitializedException("Square count has to be initialized and inherent");

            if (image.Width < cellsCount)
                throw new WrongResolutionException("Image's width must be higher or input less cells");

            int cellWidth = image.Width / cellsCount;

            if (image.Height < cellWidth)
                throw new WrongResolutionException("Image's height must be higher");

            int newHeight = image.Height / cellWidth;
            int newWidth = cellsCount;

            for (int i = 2; i < countResolutions + 2; i++)
                result.Add(new Resolution(newWidth * i, newHeight * i), i);

            return result;
        }

    }
}

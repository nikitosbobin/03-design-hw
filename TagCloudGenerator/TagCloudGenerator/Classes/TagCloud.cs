﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class TagCloud : ICloudImageGenerator
    {

        private Bitmap image;
        public Bitmap GetBitmap()
        {
            return image;
        }
    }
}

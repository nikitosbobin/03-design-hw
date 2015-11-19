using System;
using System.Drawing;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        public PngEncoder(ICloudImageGenerator cloud)
        {
            this.cloud = cloud;
        }

        private readonly ICloudImageGenerator cloud;
        public Bitmap Origin {
            get { return cloud.Image; }
            private set { }
        }

        public bool SaveImage(String name)
        {
            try
            {
                Origin.Save(name + ".png", ImageFormat.Png);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

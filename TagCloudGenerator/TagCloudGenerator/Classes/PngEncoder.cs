using System;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        private ICloudImageGenerator cloud;

        public PngEncoder(ICloudImageGenerator cloud)
        {
            this.cloud = cloud;
        }

        public bool SaveImage(String name)
        {
            try
            {
                cloud.CreateImage();
                cloud.Image.Save(name + ".png", ImageFormat.Png);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

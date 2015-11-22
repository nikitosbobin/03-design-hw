using System;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        public bool SaveImage(String name, ICloudImageGenerator cloud)
        {
            try
            {
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

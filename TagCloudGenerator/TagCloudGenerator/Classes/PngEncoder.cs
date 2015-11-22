using System;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        
        public void SaveImage(String name, ICloudImageGenerator cloud)
        {
            cloud.Image.Save(name + ".png", ImageFormat.Png);
        }
    }
}

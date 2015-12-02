using System;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        private ICloudImageGenerator cloudImage;

        public PngEncoder(ICloudImageGenerator cloudImage)
        {
            this.cloudImage = cloudImage;
        }

        public void SaveImage(String name)
        {
            cloudImage.CreateImage();
            cloudImage.Image.Save(name + ".png", ImageFormat.Png); 
        }
    }
}

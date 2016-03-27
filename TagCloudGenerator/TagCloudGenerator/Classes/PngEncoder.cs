using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        private readonly ICloudImageGenerator cloudImage;

        public PngEncoder(ICloudImageGenerator cloudImage)
        {
            this.cloudImage = cloudImage;
        }

        public void SaveImage(string name)
        {
            cloudImage.CreateImage();
            cloudImage.Image.Save(name + ".png", ImageFormat.Png); 
        }
    }
}

using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PngEncoder : IImageEncoder
    {
        private readonly ICloudImageGenerator cloudImage;
        private readonly ICloudGenerator generator;
        public PngEncoder(ICloudImageGenerator cloudImage, ICloudGenerator generator)
        {
            this.cloudImage = cloudImage;
            this.generator = generator;
        }

        public void SaveImage(string name)
        {
            cloudImage.CreateImage(generator);
            cloudImage.Image.Save(name + ".png", ImageFormat.Png); 
        }
    }
}

using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        Bitmap Image { get; set; }
        ICloudGenerator Cloud { get; }
        void CreateImage();
    }
}

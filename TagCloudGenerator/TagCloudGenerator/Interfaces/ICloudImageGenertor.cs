using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        void CreateImage(ITextDecoder decoder, ITextHandler parsedText, IImageEncoder imageEncoder);
        bool SaveImage(string path);
        Bitmap Image { get; }
    }
}

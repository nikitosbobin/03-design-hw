using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        void CreateImage(ITextDecoder decoder, ITextHandler parsedText);
        Bitmap Image { get; }
    }
}

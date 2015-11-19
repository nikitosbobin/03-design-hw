using System;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface IImageEncoder
    {
        Bitmap Origin { get; }
        bool SaveImage(String name);
    }
}

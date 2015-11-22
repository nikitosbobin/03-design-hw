using System;

namespace TagCloudGenerator.Interfaces
{
    interface IImageEncoder
    {
        bool SaveImage(String name, ICloudImageGenerator cloud);
    }
}

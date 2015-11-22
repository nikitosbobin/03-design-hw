using System;

namespace TagCloudGenerator.Interfaces
{
    interface IImageEncoder
    {
        void SaveImage(String name, ICloudImageGenerator cloud);
    }
}

using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudGenerator
    {
        void CreateCloud();
        ITextHandler TextHandler { get; set; }
        IWordBlock[] Words { get; set; }
        ICloudImageGenerator ImageGenerator { get; }
        float WordScale { get; set; }
        string FontFamily { get; set; }
        bool MoreDensity { get; set; }
    }
}

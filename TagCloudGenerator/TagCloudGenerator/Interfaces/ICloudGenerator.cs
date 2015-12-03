using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudGenerator
    {
        void CreateCloud();
        Size Size { get; set; }
        ITextHandler TextHandler { get; set; }
        IWordBlock[] Words { get; set; }
        ICloudImageGenerator Generator { get; }
        float WordScale { get; set; }
        string FontFamily { get; set; }
        bool MoreDensity { get; set; }
    }
}

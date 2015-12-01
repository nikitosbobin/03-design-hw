using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        void CreateImage();
        Bitmap Image { get; }
        Size Size { get; set; }
        List<SolidBrush> WordsBrushes { get; set; }
        ITextHandler TextHandler { get; set; }
        float WordScale { get; set; }
        string FontFamily { get; set; }
        bool MoreDensity { get; set; }
    }
}

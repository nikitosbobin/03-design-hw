using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudGenerator
    {
        void CreateCloud();
        Size Size { get; set; }
        List<SolidBrush> WordsBrushes { get; set; }
        ITextHandler TextHandler { get; set; }
        IWord[] Words { get; set; }
        float WordScale { get; set; }
        string FontFamily { get; set; }
        bool MoreDensity { get; set; }
    }
}

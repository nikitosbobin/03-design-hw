using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    interface ICloudImageGenerator
    {
        Bitmap Image { get; set; }
        ICloudGenerator Cloud { get; }
        List<SolidBrush> WordsBrushes { get; set; }
        void CreateImage();
    }
}

using System.Collections.Generic;
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    public interface IWordBlock
    {
        string Source { get; }
        int Frequency { get; set; }
        Point Location { get; set; }
        Rectangle GetWordRectangle(Graphics graphics);
        Size GetWordSize(Graphics graphics);
        Font Font { get; set; }
        float FontSize { get; set; }
        void Draw(Graphics graphics, Brush brush);
        bool IsVertical { get; set; }
        void SaveLocation();
        bool RestoreLocation();
        bool IntersectsWith(IEnumerable<Rectangle> frames, Graphics graphics);
    }
}
using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    public interface IWordBlock
    {
        string Source { get; }
        int Frequency { get; set; }
        Point Location { get; set; }
        Rectangle GetWordRectangle(Graphics graphics);
        Font Font { get; set; }
        void Draw(Graphics graphics, Brush brush);
        bool IsVertical { get; set; }
    }
}
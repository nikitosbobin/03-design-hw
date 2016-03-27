using System.Drawing;

namespace TagCloudGenerator.Interfaces
{
    public interface IWordBlock
    {
        string Source { get; }
        int Frequency { get; set; }
        Point Location { get; set; }
        Rectangle WordRectangle { get; }
        Font Font { get; set; }
        void Draw(Graphics graphics, Brush brush, Point imageCenter);
        bool Vertical { get; set; }
    }
}
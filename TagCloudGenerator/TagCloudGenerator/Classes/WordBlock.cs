using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class WordBlock : IWordBlock
    {
        public WordBlock(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Frequency = frequency;
            Location = Point.Empty;
            Vertical = false;
        }

        public string Source { get; }
        public int Frequency { get; set; }
        public Point Location { get; set; }

        public Rectangle WordRectangle
        {
            get
            {
                var wordWidth = (int)(Font.Size * 0.7) * Source.Length;
                var wordHeight = Font.Height;
                var location = new Point(Location.X, Location.Y);
                if (Vertical)
                    location.Y -= wordWidth;
                return new Rectangle(location, new Size(Vertical ? wordHeight : wordWidth, Vertical ? wordWidth : wordHeight));
            }
        }
        private Font font;
        public Font Font
        {
            get { return font ?? (font = new Font("Times New Roman", 12f)); }
            set { font = value; }
        }

        public bool Vertical { get; set; }

        public void Draw(Graphics graphics, Brush brush, Point imageCenter)
        {
            Font = new Font(Font.FontFamily.ToString(), 30f);
            graphics.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, imageCenter.X + Location.X, imageCenter.Y - Location.Y);
            var angle = Vertical ? 270f : 0f;
            graphics.RotateTransform(angle);
            graphics.DrawString(Source, Font, brush, 0, 0);
            graphics.ResetTransform();
            var v = WordRectangle;
            v.Offset(imageCenter.X, imageCenter.Y);
            graphics.DrawRectangle(new Pen(Color.Crimson), v);
        }
    }
}

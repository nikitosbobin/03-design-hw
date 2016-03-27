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
            IsVertical = false;
        }

        public string Source { get; }
        public int Frequency { get; set; }
        public Point Location { get; set; }

        public Rectangle GetWordRectangle(Graphics graphics)
        {
            var currentWordSize = graphics.MeasureString(Source, Font);
            var wordWidth = (int) currentWordSize.Width;
            var wordHeight = (int) currentWordSize.Height;
            var location = new Point(Location.X, Location.Y);
            if (IsVertical) location.Y -= wordWidth;
            return new Rectangle(location, new Size(IsVertical ? wordHeight : wordWidth, IsVertical ? wordWidth : wordHeight));
        }
        private Font font;
        public Font Font
        {
            get { return font ?? (font = new Font("Times New Roman", 12f)); }
            set { font = value; }
        }

        public float FontSize { get { return Font.Size; }
            set { Font = new Font(Font.FontFamily.ToString(), value); }
        }

        public bool IsVertical { get; set; }

        public void Draw(Graphics graphics, Brush brush)
        {
            graphics.DrawRectangle(new Pen(Color.Blue), Location.X, Location.Y, 2, 2);
            var grState = graphics.Save();
            graphics.TranslateTransform(Location.X, Location.Y);
            var angle = IsVertical ? 270f : 0f;
            graphics.RotateTransform(angle);
            graphics.DrawString(Source, Font, brush, 0, 0);
            graphics.Restore(grState);
            var v = GetWordRectangle(graphics);
            graphics.DrawRectangle(new Pen(Color.Crimson), v);
        }
    }
}

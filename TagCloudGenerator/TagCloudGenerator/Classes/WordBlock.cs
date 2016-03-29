using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class WordBlock : IWordBlock
    {
        public WordBlock(string source, int frequency = 1)
        {
            Source = source;
            Frequency = frequency;
            Location = Point.Empty;
            var rnd = new Random(DateTime.Now.Millisecond);
            IsVertical = rnd.Next(0, 2) == 1;
            //IsVertical = false;
            savedLocations = new Stack<Point>();
        }

        public string Source { get; }
        public int Frequency { get; set; }
        public Point Location { get; set; }

        public Rectangle GetWordRectangle(Graphics graphics)
        {
            var currentWordSize = GetWordSize(graphics);
            var wordWidth = currentWordSize.Width;
            var wordHeight = currentWordSize.Height;
            var location = new Point(Location.X, Location.Y);
            if (IsVertical) location.Y -= wordWidth;
            return new Rectangle(location, new Size(IsVertical ? wordHeight : wordWidth, IsVertical ? wordWidth : wordHeight));
        }

        public Size GetWordSize(Graphics graphics)
        {
            var tmpSize = graphics.MeasureString(Source, Font);
            return new Size((int) tmpSize.Width, (int) tmpSize.Height);
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
        private Stack<Point> savedLocations;
        public void SaveLocation()
        {
            savedLocations.Push(Location);
        }

        public bool RestoreLocation()
        {
            if (savedLocations.Count == 0) return false;
            Location = savedLocations.Pop();
            return true;
        }

        public bool IntersectsWith(IEnumerable<Rectangle> frames, Graphics graphics)
        {
            return frames?.Any(r => r.IntersectsWith(GetWordRectangle(graphics))) ?? false;
        }

        public bool InsideImage(Graphics graphics)
        {
            return GetWordRectangle(graphics).GetPoints().All(graphics.IsVisible);
        }

        public override string ToString()
        {
            return $"{Source}--{Frequency}";
        }

        public void Draw(Graphics graphics, Brush brush)
        {
            //graphics.DrawRectangle(new Pen(Color.Blue), Location.X, Location.Y, 2, 2);
            var grState = graphics.Save();
            graphics.TranslateTransform(Location.X, Location.Y);
            var angle = IsVertical ? 270f : 0f;
            graphics.RotateTransform(angle);
            graphics.DrawString(Source, Font, brush, 0, 0);
            graphics.Restore(grState);
            //var v = GetWordRectangle(graphics);
            //graphics.DrawRectangle(new Pen(Color.Crimson), v);
        }
    }
}

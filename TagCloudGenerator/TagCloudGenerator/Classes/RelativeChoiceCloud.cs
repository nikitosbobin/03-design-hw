using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class RelativeChoiceCloud : ICloudGenerator
    {
        public RelativeChoiceCloud(ITextDecoder decoder, ITextHandler textHandler, ICloudImageGenerator generator)
        {
            ImageGenerator = generator;
            TextHandler = textHandler;
            this.decoder = decoder;
            frames = new HashSet<Rectangle>();
        }

        private readonly HashSet<Rectangle> frames;
        private readonly ITextDecoder decoder;

        public void CreateCloud()
        {
            Words = TextHandler.GetWords(decoder).OrderByDescending(u => u.Frequency).ToArray();
            foreach (var word in Words)
            {
                foreach (var frame in frames)
                {
                    if (BypassRect(word, frame.GetPoints(), 4, frames, Graphics))
                    {
                        frames.Add(word.GetWordRectangle(Graphics));
                        break;
                    }
                }
                if (frames.Count == 0)
                    frames.Add(word.GetWordRectangle(Graphics));
            }
        }
        
        public ITextHandler TextHandler { get; set; }
        public IWordBlock[] Words { get; set; }
        public ICloudImageGenerator ImageGenerator { get; }
        public Graphics Graphics => ImageGenerator.Graphics;
        public float WordScale { get; set; }
        public string FontFamily { get; set; }
        public bool MoreDensity { get; set; }

        private bool BypassRect(IWordBlock word, Point[] points, int count, IEnumerable<Rectangle> frames, Graphics gr)
        {
            return points.Where((t, i) => MoveOnLine(word, t, points[(i + 1)%points.Length], count, frames, gr)).Any();
        }

        private bool MoveOnLine(IWordBlock word, Point start, Point end, int count, IEnumerable<Rectangle> frames, Graphics gr)
        {
            var dist = start.OffsetTo(end);
            var startLocation = word.Location;
            var lineIsVertical = start.X == end.X;
            for (var i = 0; i < count; ++i)
            {
                word.Location = new Point(start.X + i * dist.X / count, start.Y + i * dist.Y / count);
                Rectangle tmpRect;
                if (lineIsVertical)
                {
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    word.IsVertical = true;
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    word.IsVertical = false;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X - tmpRect.Width, word.Location.Y);
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    word.Location = new Point(word.Location.X + tmpRect.Width, word.Location.Y);
                    word.IsVertical = true;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X - tmpRect.Width, word.Location.Y);
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                }
                else
                {
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    word.SaveLocation();
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y - tmpRect.Height);
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    word.RestoreLocation();
                    word.IsVertical = true;
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y + tmpRect.Height);
                    if (!word.IntersectsWith(frames, gr))
                        return true;
                }
                word.IsVertical = false;
            }
            word.Location = startLocation;
            return false;
        }
    }
}

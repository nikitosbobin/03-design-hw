using System;
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
            frames = new List<Rectangle>();
            WordScale = 0.1f;
            rnd = new Random(DateTime.Now.Millisecond);
        }

        private readonly List<Rectangle> frames;
        private readonly ITextDecoder decoder;
        private Random rnd;

        public void CreateCloud()
        {
            
            Console.WriteLine("Handling words\n");
            Words = TextHandler.GetWords(decoder).OrderByDescending(u => u.Frequency).ToArray();
            var currentFontSize = ImageGenerator.Image.Height * WordScale;
            Words[0].FontSize = currentFontSize;
            var currentFreq = Words[0].Frequency;
            var logger = new ConsoleLogger(Words.Length);
            logger.LogTitle("Start cloud creating");
            foreach (var word in Words)
            {
                if (currentFreq > word.Frequency)
                {
                    currentFontSize *= ((float)word.Frequency / currentFreq);
                    currentFreq = word.Frequency;
                }
                word.FontSize = currentFontSize;
                if (frames.Shuffle().Any(frame => BypassRect(word, frame.GetPoints().OffsetArray(rnd.Next(0, 4)), 4, frames, Graphics)))
                {
                    frames.Add(word.GetWordRectangle(Graphics));
                }
                if (frames.Count == 0)
                {
                    var wordSize = word.GetWordSize(Graphics);
                    word.Location = new Point(-wordSize.Width/2,-wordSize.Height/2);
                    word.IsVertical = false;
                    frames.Add(word.GetWordRectangle(Graphics));
                }
                logger.LogStatus();
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
            var startRotation = word.IsVertical;
            for (var i = 0; i < count; ++i)
            {
                word.Location = new Point(start.X + i * dist.X / count, start.Y + i * dist.Y / count);
                Rectangle tmpRect;
                if (lineIsVertical)
                {
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    word.SaveLocation();
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X + tmpRect.Width * (word.IsVertical ? 1 : -1), word.Location.Y);
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    word.RestoreLocation();
                    word.IsVertical = !word.IsVertical;
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X + tmpRect.Width * (word.IsVertical ? 1 : -1), word.Location.Y);
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                }
                else
                {
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    word.SaveLocation();
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y + tmpRect.Height * (word.IsVertical ? 1 : -1));
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    word.RestoreLocation();
                    word.IsVertical = !word.IsVertical;
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                    tmpRect = word.GetWordRectangle(gr);
                    word.Location = new Point(word.Location.X, word.Location.Y + tmpRect.Height * (word.IsVertical ? 1 : -1));
                    if (!word.IntersectsWith(frames, gr) && word.InsideImage(gr))
                        return true;
                }
                word.IsVertical = startRotation;
            }
            word.Location = startLocation;
            return false;
        }
    }
}

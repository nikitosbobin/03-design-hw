using System;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class TagCloud : ICloudImageGenerator
    {
        public TagCloud(ITextParser parsedText, int height, int width)
        {
            this.parsedText = parsedText;
            image = new Bitmap(height, width);
            graph = Graphics.FromImage(image);
            graph.Clear(Color.Aqua);
            rnd = new Random();
        }

        public void DrawWord(Word word, PointF pos)
        {
            graph.DrawString(word.Source,word.Font,word.Color, image.Width/2+pos.X, image.Height/2-pos.Y);
        }

        private ITextParser parsedText;
        private Bitmap image;
        public Bitmap Image {
            get
            {
                Update();
                return image;
            }
            private set { }
        }
        private Graphics graph;
        private float currentRadius;
        private Random rnd;

        private PointF Func(Word word)
        {
            
            float x = rnd.Next(-1 * (int)currentRadius, (int)currentRadius + 1);
            float y = (float)(Math.Pow(-1, rnd.Next(1, 3))*Math.Sqrt(Math.Pow(currentRadius, 2) - Math.Pow(x, 2)));
            return new PointF(x, y);
        }

        private void Update()
        {
            var words = parsedText.Words.OrderBy(u => u.Frequency).ToArray();
            currentRadius = Math.Min(image.Height, image.Width)/2 - 15;
            graph.DrawEllipse(new Pen(Color.Black), (float)(image.Width / 2 - currentRadius), (float)(image.Height / 2 - currentRadius), 2 * currentRadius, 2 * currentRadius);
            int currentFreq = words[0].Frequency;
            float delta = currentRadius / words.Select(y => y.Frequency).Distinct().Count();
            foreach (var word in words)
            {
                if (word.Frequency > currentFreq)
                {
                    currentFreq = word.Frequency;
                    currentRadius -= delta;
                }
                DrawWord(word, Func(word));
            }
        }
    }
}

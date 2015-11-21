using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class TagCloud : ICloudImageGenerator
    {
        public TagCloud(ITextParser parsedText, int height, int width, List<SolidBrush> wordsColors = null)
        {
            this.parsedText = parsedText;
            image = new Bitmap(height, width);
            rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            graph = Graphics.FromImage(image);
            graph.Clear(Color.CadetBlue);
            this.wordsColors = new List<SolidBrush>();
            if (wordsColors != null)
                this.wordsColors = wordsColors;
            else
                this.wordsColors.Add(new SolidBrush(Color.Black));
        }

        public void DrawNextWord(Word word)
        {
            Font font = new Font("Times New Roman", currentFontSize);

            SolidBrush color = wordsColors[rnd.Next(0, wordsColors.Count)];
            int wordWidth = (int) (font.Size*0.65)*word.Source.Length;
            int wordHeight = font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = Func();
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                currentAngle += delta;
            } while (InterdsectsWithAny(thisWord));
            var t = wordsColors[0];

            graph.DrawString(word.Source, font, color,
                (image.Width / 2 + pos.X), (image.Height / 2 - pos.Y));
            frames.Add(thisWord);
            //graph.DrawRectangle(new Pen(Brushes.Black), (image.Width / 2 + pos.X), (image.Height / 2 - pos.Y),wordWidth,wordHeight);
        }

        private bool InterdsectsWithAny(Rectangle rect)
        {
            foreach (var r in frames)
            {
                if (r.IntersectsWith(rect))
                    return true;
            }
            return false;
        }

        private Random rnd;
        public static float MIN_FONT_SIZE = 12;
        private float currentFontSize;
        private HashSet<Rectangle> frames; 
        private ITextParser parsedText;
        private List<SolidBrush> wordsColors;
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
        private float currentAngle;
        private float delta = (float)Math.PI/100;

        private Point Func()
        {
            var x = (int)(5*currentAngle * Math.Cos(currentAngle));
            var y = (int)(3*currentAngle * Math.Sin(currentAngle));
            //image.SetPixel((image.Width / 2 + x), (image.Height / 2 - y), Color.Black);
            return new Point(x, y);
        }

        private void Update()
        {
            var words = parsedText.Words.OrderByDescending(u => u.Frequency).ToArray();
            currentFontSize = MIN_FONT_SIZE + words.Select(h => h.Frequency).Distinct().Count()/3;
            int currentFreq = words[0].Frequency;
            foreach (var word in words)
            {
                if (currentFreq > word.Frequency)
                {
                    currentFontSize -= 1f;
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                currentAngle = 0;
                image.Save("out.png", ImageFormat.Png);
            }
        }
    }
}

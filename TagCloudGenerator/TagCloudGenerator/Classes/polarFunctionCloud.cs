using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PolarFunctionCloud : ICloudImageGenerator
    {
        public PolarFunctionCloud(ITextParser parsedText, int height, int width, List<SolidBrush> wordsColors = null)
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
            var font = new Font("Times New Roman", currentFontSize);
            var color = wordsColors[rnd.Next(0, wordsColors.Count)];
            var wordWidth = (int) (font.Size*0.65)*word.Source.Length;
            var wordHeight = font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = Func(currentAngle, new Size(image.Width, image.Height));
                //image.SetPixel((image.Width / 2 + pos.X), (image.Height / 2 - pos.Y), Color.Black);
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                currentAngle += delta;
            } while (InterdsectsWithAny(thisWord));
            graph.DrawString(word.Source, font, color,
                (image.Width / 2 + pos.X), (image.Height / 2 - pos.Y));
            frames.Add(thisWord);
            //graph.DrawRectangle(new Pen(Brushes.Black), (image.Width / 2 + pos.X), (image.Height / 2 - pos.Y),
                //wordWidth, wordHeight);
        }

        private bool InterdsectsWithAny(Rectangle rect)
        {
            return frames.Any(rect.IntersectsWith);
        }

        private readonly Random rnd;
        public static float MIN_FONT_SIZE = 12;
        private float currentFontSize;
        private HashSet<Rectangle> frames; 
        private readonly ITextParser parsedText;
        private readonly List<SolidBrush> wordsColors;
        private readonly Bitmap image;
        public Bitmap Image {
            get
            {
                Update();
                return image;
            }
            private set { }
        }
        private readonly Graphics graph;
        private float currentAngle;
        private const float delta = (float) Math.PI/100;

        public Func<float, Size, Point> Func = MainFunc;

        /// <summary>
        /// Задаёт функцию в полярных координатах
        /// </summary>
        /// <param name="angle">Угол</param>
        /// <param name="image">Размер изображения</param>
        /// <returns></returns>
        private static Point MainFunc(float angle, Size image)
        {
            var nod = GetGreatestCommonDivisor(image.Height, image.Width);
            var x = (int)(image.Width / nod * angle * Math.Cos(angle));
            var y = (int)(image.Height / nod * angle * Math.Sin(angle));
            return new Point(x, y);
        }

        private static int GetGreatestCommonDivisor(int firstItem, int secondItem)
        {
            while (firstItem != secondItem)
            {
                if (firstItem > secondItem)
                    firstItem -= secondItem;
                else
                    secondItem -= firstItem;
            }
            return firstItem;
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

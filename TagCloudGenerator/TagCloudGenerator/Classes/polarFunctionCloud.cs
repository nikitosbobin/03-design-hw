using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class PolarFunctionCloud : ICloudImageGenerator
    {
        public PolarFunctionCloud(int height, int width, List<SolidBrush> wordsBrushes = null)
        {
            Image = new Bitmap(height, width);
            rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            graph = Graphics.FromImage(Image);
            graph.Clear(Color.CadetBlue);
            this.wordsBrushes = new List<SolidBrush>();
            if (wordsBrushes != null)
                this.wordsBrushes = wordsBrushes;
            else
                this.wordsBrushes.Add(new SolidBrush(Color.Black));
        }

        public void DrawNextWord(Word word)
        {
            var font = new Font("Times New Roman", currentFontSize);
            var color = wordsBrushes[rnd.Next(0, wordsBrushes.Count)];
            var wordWidth = (int)(font.Size * 0.7) * word.Source.Length;
            var wordHeight = font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = Func(currentAngle, new Size(Image.Width, Image.Height));
                //Image.SetPixel((Image.Width / 2 + pos.X), (Image.Height / 2 - pos.Y), Color.Black);
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                currentAngle += delta;
            } while (InterdsectsWithAny(thisWord));
            graph.DrawString(word.Source, font, color,
                (Image.Width / 2 + pos.X), (Image.Height / 2 - pos.Y));
            frames.Add(thisWord);
            //graph.DrawRectangle(new Pen(Brushes.Black), (Image.Width / 2 + pos.X), (Image.Height / 2 - pos.Y),
            //wordWidth, wordHeight);
        }

        private bool InterdsectsWithAny(Rectangle rect)
        {
            bool a = (Image.Width/2 + rect.X) > 0;
            bool b = (Image.Width/2 + rect.Right) < Image.Width;
            bool c = (Image.Height/2 - rect.Y) > 0;
            bool d = (Image.Height/2 - rect.Bottom) < Image.Height;
            return frames.Any(rect.IntersectsWith) || !a || !b || !c || !d;
        }


        private readonly Random rnd;
        private float currentFontSize;
        private HashSet<Rectangle> frames;
        private readonly List<SolidBrush> wordsBrushes;
        public Bitmap Image { get; private set; }
        private readonly Graphics graph;
        private float currentAngle;
        private const float delta = (float)Math.PI / 100;

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

        public void CreateImage(ITextDecoder decoder, ITextHandler parsedText)
        {
            var words = parsedText.GetWords(decoder).OrderByDescending(u => u.Frequency).ToArray();
            currentFontSize = Image.Height * 0.04f; //облако меняется
            int currentFreq = words[0].Frequency;
            foreach (var word in words)
            {
                if (currentFreq > word.Frequency)
                {
                    currentFontSize *= ((float)word.Frequency/currentFreq);
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                //currentAngle = 0; //облако меняется
            }
        }
    }
}
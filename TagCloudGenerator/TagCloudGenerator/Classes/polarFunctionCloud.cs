using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Ninject.Planning.Directives;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    partial class PolarFunctionCloud : ICloudImageGenerator
    {
        public PolarFunctionCloud(ICommand[] commands, ITextDecoder decoder, ITextHandler textHandler)
        {
            this.decoder = decoder;
            TextHandler = textHandler;
            rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            foreach (var command in commands)
                command.Execute(this);
        }

        public PolarFunctionCloud(int width, int height, ITextDecoder decoder, 
            ITextHandler textHandler,List<SolidBrush> wordsBrushes = null)
        {
            this.decoder = decoder;
            TextHandler = textHandler;
            Image = new Bitmap(width, height);
            rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            graphics = Graphics.FromImage(Image);
            graphics.Clear(Color.CadetBlue);
            MoreDensity = false;
            WordScale = 7;
            this.wordsBrushes = new List<SolidBrush>();
            if (wordsBrushes != null)
                this.wordsBrushes = wordsBrushes;
            else
                this.wordsBrushes.Add(new SolidBrush(Color.Black));
        }

        public void DrawNextWord(Word word)
        {
            var font = new Font(FontFamily, currentFontSize);
            var color = WordsBrushes[rnd.Next(0, WordsBrushes.Count)];
            var wordWidth = (int)(font.Size * 0.7) * word.Source.Length;
            var wordHeight = font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = GetBlockCoords(currentAngle, new Size(Image.Width, Image.Height));
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                currentAngle += delta;
                //graphics.DrawString(word.Source, font, color,
                    //(Image.Width / 2 + pos.X), (Image.Height / 2 - pos.Y));
                //Image.Save("outTest.png", ImageFormat.Png);
            } while (IntersectsWithAny(thisWord));
            graphics.DrawString(word.Source, font, color,
                (Image.Width / 2 + pos.X), (Image.Height / 2 - pos.Y));
            frames.Add(thisWord);
        }

        private bool IntersectsWithAny(Rectangle rect)
        {
            bool insideLeftEdge = (Image.Width/2 + rect.X) > 0;
            bool insideRigthEdge = (Image.Width/2 + rect.Right) < Image.Width;
            bool insideTopEdge = (Image.Height/2 - rect.Y) > 0;
            bool insideBottomEdge = (Image.Height/2 - rect.Bottom) < Image.Height;
            return frames.Any(rect.IntersectsWith) || !insideLeftEdge || !insideRigthEdge || !insideTopEdge || !insideBottomEdge;
        }

        public string FontFamily { get; set; }
        private float wordScale;
        public float WordScale {
            get
            {
                if (wordScale == 0)
                    return 0.07f;
                return wordScale; 
            }
            set
            {
                if (value >= 1 && value <= 9)
                    wordScale = value / 100;
                else
                    wordScale = 0.07f;
            }
        }
        public bool MoreDensity { get; set; }
        private readonly ITextDecoder decoder;
        public ITextHandler TextHandler { get; set; }
        private readonly Random rnd;
        private float currentFontSize;
        private HashSet<Rectangle> frames;
        private List<SolidBrush> wordsBrushes;
        public List<SolidBrush> WordsBrushes {
            get { return wordsBrushes; }
            set
            {
                if (value != null)
                    wordsBrushes = value;
            }
        }
        public Bitmap Image { get; private set; }
        public Size Size {
            get { return new Size(Image.Width, Image.Height); }
            set
            {
                Image = new Bitmap(value.Width, value.Height);
                graphics = Graphics.FromImage(Image);
                graphics.Clear(Color.CadetBlue);
            }
        }
        private Graphics graphics;
        private float currentAngle;
        private const float delta = (float)Math.PI / 100;

        public Func<float, Size, Point> GetBlockCoords = ArchimedSpiralFunc;

        /// <summary>
        /// Задаёт функцию в полярных координатах
        /// </summary>
        /// <param name="angle">Угол</param>
        /// <param name="imageSize">Размер изображения</param>
        /// <returns></returns>
        private static Point ArchimedSpiralFunc(float angle, Size imageSize)
        {
            var nod = GetGreatestCommonDivisor(imageSize.Height, imageSize.Width);
            var x = (int)((double)imageSize.Width / nod * angle * Math.Cos(angle));
            var y = (int)((double)imageSize.Height / nod * angle * Math.Sin(angle));
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

        public void CreateImage()
        {
            var words = TextHandler.GetWords(decoder).OrderByDescending(u => u.Frequency).ToArray();
            currentFontSize = Image.Height * WordScale;
            int currentFreq = words[0].Frequency;
            foreach (var word in words)
            {
                if (currentFreq > word.Frequency)
                {
                    currentFontSize *= ((float)word.Frequency/currentFreq);
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                if (MoreDensity) currentAngle = 0;

            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    abstract partial class PolarFunctionCloud : ICloudGenerator
    {
        public abstract Point GetBlockCoords();

        protected PolarFunctionCloud(ITextDecoder decoder, ITextHandler textHandler)
        {
            Generator = new ImageGenerator(this);
            this.decoder = decoder;
            TextHandler = textHandler;
            frames = new HashSet<Rectangle>();
        }

        protected PolarFunctionCloud(int width, int height, ITextDecoder decoder,
            ITextHandler textHandler)
        {
            this.decoder = decoder;
            Size = new Size(width, height);
            TextHandler = textHandler;
            frames = new HashSet<Rectangle>();
            MoreDensity = false;
            WordScale = 7;
        }

        public void DrawNextWord(IWordBlock word)
        {
            word.Font = new Font(FontFamily, currentFontSize);
            var wordWidth = (int)(word.Font.Size * 0.7) * word.Source.Length;
            var wordHeight = word.Font.Height;
            Rectangle thisWord;
            do
            {
                var pos = GetBlockCoords();
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                CurrentAngle += Delta;
            } while (IntersectsWithAny(thisWord));
            word.Location = thisWord.Location;
            frames.Add(thisWord);
        }

        private bool IntersectsWithAny(Rectangle rect)
        {
            var insideLeftEdge = (Size.Width / 2 + rect.X) > 0;
            var insideRigthEdge = (Size.Width / 2 + rect.Right) < Size.Width;
            var insideTopEdge = (Size.Height / 2 - rect.Y) > 0;
            var insideBottomEdge = (Size.Height / 2 - rect.Bottom) < Size.Height;
            return frames.Any(rect.IntersectsWith) || !insideLeftEdge || !insideRigthEdge || !insideTopEdge || !insideBottomEdge;
        }

        private string fontFamily;

        public string FontFamily
        {
            get
            {
                if (string.IsNullOrEmpty(fontFamily))
                    fontFamily = "Times New Roman";
                return fontFamily;
            }
            set { fontFamily = value; }
        }
        private float wordScale;
        public IWordBlock[] Words { get; set; }

        public ICloudImageGenerator Generator { get; }

        public float WordScale
        {
            get
            {
                if (wordScale == 0f)
                    wordScale = 0.03f;
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

        private float currentFontSize;
        private readonly HashSet<Rectangle> frames;

        public Size Size
        {
            get
            {
                if (size.Width == 0 && size.Height == 0)
                    size = new Size(500, 500);
                return size;
            }
            set { size = value; }
        }
        private Size size;
        protected float CurrentAngle;
        private const float Delta = (float)Math.PI / 100;

        public void CreateCloud()
        {
            Words = TextHandler.GetWords(decoder).OrderByDescending(u => u.Frequency).ToArray();
            currentFontSize = Size.Height * WordScale;
            var currentFreq = Words[0].Frequency;
            foreach (var word in Words)
            {
                if (currentFreq > word.Frequency)
                {
                    currentFontSize *= ((float)word.Frequency / currentFreq);
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                if (MoreDensity) CurrentAngle = 0;
            }
        }
    }
}

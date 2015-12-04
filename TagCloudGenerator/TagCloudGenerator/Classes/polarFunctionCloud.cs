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

        public PolarFunctionCloud(ITextDecoder decoder, ITextHandler textHandler)
        {
            Generator = new ImageGenerator(this);
            _decoder = decoder;
            TextHandler = textHandler;
            frames = new HashSet<Rectangle>();
        }

        public PolarFunctionCloud(int width, int height, ITextDecoder decoder,
            ITextHandler textHandler)
        {
            _decoder = decoder;
            Size = new Size(width, height);
            TextHandler = textHandler;
            frames = new HashSet<Rectangle>();
            MoreDensity = false;
            WordScale = 7;
        }

        public void DrawNextWord(IWordBlock word)
        {
            word.Font = new Font(FontFamily, _currentFontSize);
            var wordWidth = (int)(word.Font.Size * 0.7) * word.Source.Length;
            var wordHeight = word.Font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = GetBlockCoords();
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                CurrentAngle += Delta;
            } while (IntersectsWithAny(thisWord));
            word.WordRectangle = thisWord;
            frames.Add(thisWord);
        }

        private bool IntersectsWithAny(Rectangle rect)
        {
            bool insideLeftEdge = (Size.Width / 2 + rect.X) > 0;
            bool insideRigthEdge = (Size.Width / 2 + rect.Right) < Size.Width;
            bool insideTopEdge = (Size.Height / 2 - rect.Y) > 0;
            bool insideBottomEdge = (Size.Height / 2 - rect.Bottom) < Size.Height;
            return frames.Any(rect.IntersectsWith) || !insideLeftEdge || !insideRigthEdge || !insideTopEdge || !insideBottomEdge;
        }

        private string _fontFamily;

        public string FontFamily
        {
            get
            {
                if (string.IsNullOrEmpty(_fontFamily))
                    _fontFamily = "Times New Roman";
                return _fontFamily;
            }
            set { _fontFamily = value; }
        }
        private float _wordScale;
        public IWordBlock[] Words { get; set; }

        public ICloudImageGenerator Generator { get; }

        public float WordScale
        {
            get
            {
                if (_wordScale == 0)
                    _wordScale = 0.03f;
                return _wordScale;
            }
            set
            {
                if (value >= 1 && value <= 9)
                    _wordScale = value / 100;
                else
                    _wordScale = 0.07f;
            }
        }
        public bool MoreDensity { get; set; }

        private readonly ITextDecoder _decoder;
        public ITextHandler TextHandler { get; set; }

        private float _currentFontSize;
        private HashSet<Rectangle> frames;

        public Size Size
        {
            get
            {
                if (_size.Width == 0 && _size.Height == 0)
                    _size = new Size(500, 500);
                return _size;
            }
            set { _size = value; }
        }
        private Size _size;
        protected float CurrentAngle;
        private const float Delta = (float)Math.PI / 100;

        public void CreateCloud()
        {
            Words = TextHandler.GetWords(_decoder).OrderByDescending(u => u.Frequency).ToArray();
            _currentFontSize = Size.Height * WordScale;
            int currentFreq = Words[0].Frequency;
            foreach (var word in Words)
            {
                if (currentFreq > word.Frequency)
                {
                    _currentFontSize *= ((float)word.Frequency / currentFreq);
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                if (MoreDensity) CurrentAngle = 0;
            }
        }
    }
}

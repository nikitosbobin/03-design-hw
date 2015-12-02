using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    partial class PolarFunctionCloud : ICloudGenerator
    {
        public PolarFunctionCloud(ICommand[] commands, ITextDecoder decoder, ITextHandler textHandler)
        {
            _decoder = decoder;
            TextHandler = textHandler;
            _rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            foreach (var command in commands)
                command.Execute(this);
        }

        public PolarFunctionCloud(int width, int height, ITextDecoder decoder, 
            ITextHandler textHandler,List<SolidBrush> wordsBrushes = null)
        {
            _decoder = decoder;
            Size = new Size(width, height);
            TextHandler = textHandler;
            _rnd = new Random(DateTime.Now.Millisecond);
            frames = new HashSet<Rectangle>();
            MoreDensity = false;
            WordScale = 7;
            _wordsBrushes = new List<SolidBrush>();
            if (wordsBrushes != null)
                _wordsBrushes = wordsBrushes;
            else
                _wordsBrushes.Add(new SolidBrush(Color.Black));
        }

        public void DrawNextWord(IWord word)
        {
            word.Font = new Font(FontFamily, _currentFontSize);
            word.SolidBrush = WordsBrushes[_rnd.Next(0, WordsBrushes.Count)];
            var wordWidth = (int)(word.Font.Size * 0.7) * word.Source.Length;
            var wordHeight = word.Font.Height;
            Point pos;
            Rectangle thisWord;
            do
            {
                pos = GetBlockCoords(_currentAngle, Size);
                thisWord = new Rectangle(pos, new Size(wordWidth, wordHeight));
                _currentAngle += Delta;
            } while (IntersectsWithAny(thisWord));
            word.WordBlock = thisWord;
            frames.Add(thisWord);
        }

        private bool IntersectsWithAny(Rectangle rect)
        {
            bool insideLeftEdge = (Size.Width/2 + rect.X) > 0;
            bool insideRigthEdge = (Size.Width/2 + rect.Right) < Size.Width;
            bool insideTopEdge = (Size.Height/2 - rect.Y) > 0;
            bool insideBottomEdge = (Size.Height/2 - rect.Bottom) < Size.Height;
            return frames.Any(rect.IntersectsWith) || !insideLeftEdge || !insideRigthEdge || !insideTopEdge || !insideBottomEdge;
        }

        public string FontFamily { get; set; }
        private float _wordScale;
        public IWord[] Words { get; set; }

        public float WordScale {
            get
            {
                if (_wordScale == 0)
                    return 0.07f;
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
        private readonly Random _rnd;
        private float _currentFontSize;
        private HashSet<Rectangle> frames;
        private List<SolidBrush> _wordsBrushes;
        public List<SolidBrush> WordsBrushes {
            get { return _wordsBrushes; }
            set
            {
                if (value != null)
                    _wordsBrushes = value;
            }
        }

        public Size Size { get; set; }
        private float _currentAngle;
        private const float Delta = (float)Math.PI / 100;

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

        public void CreateCloud()
        {
            Words = TextHandler.GetWords(_decoder).OrderByDescending(u => u.Frequency).ToArray();
            _currentFontSize = Size.Height * WordScale;
            int currentFreq = Words[0].Frequency;
            foreach (var word in Words)
            {
                if (currentFreq > word.Frequency)
                {
                    _currentFontSize *= ((float)word.Frequency/currentFreq);
                    currentFreq = word.Frequency;
                }
                DrawNextWord(word);
                if (MoreDensity) _currentAngle = 0;

            }
        }
    }
}
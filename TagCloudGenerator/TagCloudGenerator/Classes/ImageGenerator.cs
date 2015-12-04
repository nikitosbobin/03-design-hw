using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class ImageGenerator : ICloudImageGenerator
    {
        public Bitmap Image { get; set; }
        public ICloudGenerator Cloud { get; }
        private IWordBlock[] _words;
        private List<SolidBrush> _wordsBrushes;
        private readonly Random _rnd;
        public List<SolidBrush> WordsBrushes
        {
            get
            {
                if (_wordsBrushes == null)
                {
                    _wordsBrushes = new List<SolidBrush>();
                    _wordsBrushes.Add(new SolidBrush(Color.Black));
                }
                return _wordsBrushes;
            }
            set
            {
                if (value != null)
                    _wordsBrushes = value;
            }
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

        public ImageGenerator(ICloudGenerator cloud)
        {
            Cloud = cloud;
            _rnd = new Random(DateTime.Now.Millisecond);
        }

        public void CreateImage()
        {
            Cloud.CreateCloud();
            _words = Cloud.Words;
            Image = new Bitmap(Cloud.Size.Width, Cloud.Size.Height);
            var graphics = Graphics.FromImage(Image);
            graphics.Clear(Color.CadetBlue);
            foreach (var word in _words)
            {
                graphics.DrawString(word.Source, word.Font, WordsBrushes[_rnd.Next(0, WordsBrushes.Count)],
                    (Image.Width / 2 + word.WordRectangle.X), (Image.Height / 2 - word.WordRectangle.Y));
                Thread.Sleep(1); //рандом не успевает разные цвета выбирать
            }
        }
    }
}

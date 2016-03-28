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
        private IWordBlock[] words;
        private List<SolidBrush> wordsBrushes;
        private readonly Random rnd;
        public List<SolidBrush> WordsBrushes
        {
            get
            {
                if (wordsBrushes != null) return wordsBrushes;
                wordsBrushes = new List<SolidBrush> {new SolidBrush(Color.Black)};
                return wordsBrushes;
            }
            set
            {
                if (value != null)
                    wordsBrushes = value;
            }
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

        public ImageGenerator(ICloudGenerator cloud)
        {
            Cloud = cloud;
            rnd = new Random(DateTime.Now.Millisecond);
        }

        public void CreateImage()
        {
            Cloud.CreateCloud();
            words = Cloud.Words;
            Image = new Bitmap(Cloud.Size.Width, Cloud.Size.Height);
            var graphics = Graphics.FromImage(Image);
            graphics.Transform = new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, Image.Width / 2, Image.Height / 2);
            graphics.Clear(Color.CadetBlue);
            var consoleWidth = Console.WindowWidth;
            for (var i = 0; i < words.Length; ++i)
            {
                Console.Clear();
                var statusLength = consoleWidth - 11;
                var status = (int) (statusLength * ((i + 1) / (double) words.Length));
                words[i].Draw(graphics, Brushes.Black/*, new Point(Image.Width / 2, Image.Height / 2)*/);
                Console.Write("Status: [" + new string('=', status) + ">" + new string(' ', statusLength - status) + "]");
                Thread.Sleep(10);
            }
            graphics.ResetTransform();
        }
    }
}

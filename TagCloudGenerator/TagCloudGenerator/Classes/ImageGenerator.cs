using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class ImageGenerator : ICloudImageGenerator
    {
        public Bitmap Image { get; set; }
        public Graphics Graphics { get; }
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

        public ImageGenerator(int width, int height)
        {
            Image = new Bitmap(width, height);
            Graphics = Graphics.FromImage(Image);
            Graphics.Transform = new Matrix(1, 0, 0, 1, width / 2, height / 2);
            Graphics.Clear(Color.Aquamarine);
            Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            rnd = new Random(DateTime.Now.Millisecond);
        }

        public void CreateImage(ICloudGenerator cloud)
        {
            cloud.CreateCloud();
            words = cloud.Words;
            var consoleWidth = Console.WindowWidth;
            for (var i = 0; i < words.Length; ++i)
            {
                Console.Clear();
                var statusLength = consoleWidth - 11;
                var status = (int) (statusLength * ((i + 1) / (double) words.Length));
                words[i].Draw(Graphics, Brushes.Black/*, new Point(Image.Width / 2, Image.Height / 2)*/);
                Console.Write("Status: [" + new string('=', status) + ">" + new string(' ', statusLength - status) + "]");
                //Thread.Sleep(10);
            }
            Graphics.ResetTransform();
        }
    }
}

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
            Console.WriteLine();
            var logger = new ConsoleLogger(words.Length);
            logger.LogTitle("Cloud drawing");
            foreach (var word in words)
            {
                word.Draw(Graphics, Brushes.Black);
                logger.LogStatus();
            }
            Graphics.ResetTransform();
        }
    }
}

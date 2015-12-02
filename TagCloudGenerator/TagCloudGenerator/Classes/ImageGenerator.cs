using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class ImageGenerator : ICloudImageGenerator
    {
        public Bitmap Image { get; set; }
        public ICloudGenerator Cloud { get; }
        private IWord[] _words;

        public ImageGenerator(ICloudGenerator cloud)
        {
            Cloud = cloud;
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
                graphics.DrawString(word.Source, word.Font, word.SolidBrush,
                    (Image.Width / 2 + word.WordBlock.X), (Image.Height / 2 - word.WordBlock.Y));
            }
        }
    }
}

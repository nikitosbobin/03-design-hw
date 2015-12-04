using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class WordBlock : IWordBlock
    {
        public WordBlock(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Frequency = frequency;
        }

        public string Source { get; set; }
        public int Frequency { get; set; }
        public Rectangle WordRectangle { get; set; }
        private Font _font;
        public Font Font
        {
            get
            {
                if (_font == null)
                    _font = new Font("Times New Roman", 12f);
                return _font;
            }
            set { _font = value; }
        }
    }
}

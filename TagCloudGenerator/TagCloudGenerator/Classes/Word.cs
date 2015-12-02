using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Word : IWord
    {
        public Word(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Frequency = frequency;
        }

        public string Source { get; set; }
        public int Frequency { get; set; }
        public Rectangle WordBlock { get; set; }
        public Font Font { get; set; }
        public SolidBrush SolidBrush { get; set; }
    }
}

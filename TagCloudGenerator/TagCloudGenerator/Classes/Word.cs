using System.Drawing;

namespace TagCloudGenerator.Classes
{
    class Word
    {
        public Word(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Color = new SolidBrush(System.Drawing.Color.Black);
            Frequency = frequency;
            FontFamily = "Times New Roman";
            FontSize = 30;
        }

        public string Source { get; set; }
        public SolidBrush Color { get; set; }
        public int Frequency { get; set; }
        public Font Font {
            get
            {
                return new Font(FontFamily, FontSize);
            }
            private set { }
        }
        public string FontFamily { get; set; }
        public float FontSize { get; set; }
    }
}

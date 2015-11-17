using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloudGenerator.Classes
{
    class Word
    {
        public Word(string source, int frequency = 0)
        {
            Source = source.ToLower();
            Color = Color.Black;
            Frequency = frequency;
            Random rnd = new Random(DateTime.Now.Millisecond);
            Way = (Vector) rnd.Next(0, 4);
        }

        public Word(string source, Color color, int frequency, Vector way)
        {
            Source = source.ToLower();
            Color = color;
            Frequency = frequency;
            Way = way;
        }

        public string Source { get; set; }
        public Color Color { get; set; }
        public int Frequency { get; set; }
        public Vector Way { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SolidBrush> colors = new List<SolidBrush>();
            colors.Add(new SolidBrush(Color.AliceBlue));
            colors.Add(new SolidBrush(Color.Black));
            colors.Add(new SolidBrush(Color.Aqua));
            colors.Add(new SolidBrush(Color.DarkRed));
            colors.Add(new SolidBrush(Color.DarkGreen));
            colors.Add(new SolidBrush(Color.Crimson));
            colors.Add(new SolidBrush(Color.Goldenrod));
            colors.Add(new SolidBrush(Color.BlueViolet));
            colors.Add(new SolidBrush(Color.LawnGreen));
            ITextDecoder inputText = new TxtDecoder(args[0]);
            ITextParser parsedText = new SimpleTextParser(inputText);
            var tagCloud = new PolarFunctionCloud(parsedText, int.Parse(args[1]), int.Parse(args[2]), colors);
            IImageEncoder encoder = new PngEncoder(tagCloud);
            encoder.SaveImage("out");
        }
    }
}

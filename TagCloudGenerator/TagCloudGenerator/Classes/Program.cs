using System;
using System.Collections.Generic;
using System.Drawing;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SolidBrush> solidBrushes = new List<SolidBrush>();
            solidBrushes.Add(new SolidBrush(Color.AliceBlue));
            solidBrushes.Add(new SolidBrush(Color.Black));
            solidBrushes.Add(new SolidBrush(Color.Aqua));
            solidBrushes.Add(new SolidBrush(Color.DarkRed));
            solidBrushes.Add(new SolidBrush(Color.DarkGreen));
            solidBrushes.Add(new SolidBrush(Color.Crimson));
            solidBrushes.Add(new SolidBrush(Color.Goldenrod));
            solidBrushes.Add(new SolidBrush(Color.BlueViolet));
            solidBrushes.Add(new SolidBrush(Color.LawnGreen));
            ITextDecoder inputText = new TxtDecoder(args[0]);
            ITextHandler parsedText = new SimpleTextHandler();
            ICloudImageGenerator tagCloud = new PolarFunctionCloud(int.Parse(args[1]), int.Parse(args[2]), solidBrushes);
            tagCloud.CreateImage(inputText, parsedText);
            IImageEncoder encoder = new PngEncoder();
            if (encoder.SaveImage("out", tagCloud))
                Console.WriteLine("Запись прошла успешно");
            else
                Console.WriteLine("Запись не удалась");
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using CommandLine;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            Console.WriteLine("Parsing options\n");
            Parser.Default.ParseArgumentsStrict(args, options);
            Console.WriteLine("Creating TxtDecoder\n");
            var textDecoder = new TxtDecoder(options.FilePath);
            Console.WriteLine("Creating SimpleTextHandler\n");
            var textHandler = new SimpleTextHandler(options.BoringWords);
            Console.WriteLine("Creating ImageGenerator\n");
            ICloudImageGenerator imageGenerator = new ImageGenerator(options.ImageWidth, options.ImageHeight, options.WordColors);
            Console.WriteLine("Creating CloudGenerator\n");
            ICloudGenerator cloudGenerator = new RelativeChoiceCloud(textDecoder, textHandler, imageGenerator);
            Console.WriteLine("Creating PngEncoder\n");
            var encoder = new PngEncoder(imageGenerator, cloudGenerator);
            Console.WriteLine("Saving image\n");
            var date = DateTime.Now;
            encoder.SaveImage($"cloud[{date.Day}_{date.Month}_{date.Year}][{date.Hour}_{date.Minute}_{date.Second}]");
        }
    }
}

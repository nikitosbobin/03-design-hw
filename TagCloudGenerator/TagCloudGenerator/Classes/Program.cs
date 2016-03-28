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
            Parser.Default.ParseArgumentsStrict(args, options);
            var textDecoder = new TxtDecoder(options.FilePath);
            var textHandler = new SimpleTextHandler(options.BoringWords);
            ICloudImageGenerator imageGenerator = new ImageGenerator(options.ImageWidth, options.ImageHeight);
            ICloudGenerator cloudGenerator = new RelativeChoiceCloud(textDecoder, textHandler, imageGenerator);
            var encoder = new PngEncoder(imageGenerator, cloudGenerator);
            encoder.SaveImage("out");
        }
    }
}

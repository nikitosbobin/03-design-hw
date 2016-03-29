using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CommandLine;

namespace TagCloudGenerator.Classes
{
    class Options
    {
        [Option('w', "width", DefaultValue = 500, HelpText = "Ширина выходного изображения")]
        public int ImageWidth { get; set; }

        [Option('h', "height", DefaultValue = 500, HelpText = "Высота выходного изображения")]
        public int ImageHeight { get; set; }

        public Size ImageSize => new Size(ImageWidth, ImageHeight);

        [Option('f', "font", DefaultValue = "Times New Roman", HelpText = "Задаёт шрифт печати слов")]
        public string FontFamily { get; set; }

        [Option('r', "read", Required = true, HelpText = "Пусть к текстовому файлу к ресурсам для построения облака")]
        public string FilePath { get; set; }

        [Option('b', "boring", HelpText = "Пусть к текстовому файлу со скучными словами")]
        public string BoringFilePath { get; set; }

        public IEnumerable<string> BoringWords => BoringFilePath == null ? new string[0] : File.ReadLines(BoringFilePath);

        [Option('c', "colors", HelpText = "Путь к файлу с цветами для раскраски слов")]
        public string ColorsPath { get; set; }

        public List<SolidBrush> WordColors => GetColors(ColorsPath);

        private static List<SolidBrush> GetColors(string path)
        {
            var wordsBrushes = new List<SolidBrush>();
            var converter = new ColorConverter();
            var colors = File.ReadAllLines(path);
            foreach (var color in colors)
            {
                try
                {
                    var tempColor = (Color) converter.ConvertFromString(color);
                    wordsBrushes.Add(new SolidBrush(tempColor));
                }
                catch (Exception)
                {
                    Console.WriteLine($"Can not convert {color}");
                }
                
            }
            return wordsBrushes;
        }
    }
}

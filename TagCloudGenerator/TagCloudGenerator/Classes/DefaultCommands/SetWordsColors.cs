using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetWordsColors : ICommand
    {
        public void Execute(ICloudImageGenerator cloud)
        {
            cloud.WordsBrushes = wordsBrushes;
        }

        private List<SolidBrush> wordsBrushes;

        public ICommand CreateCommand(string stringCommand)
        {
            wordsBrushes = new List<SolidBrush>();
            var pattern = "colors:<.+>";
            var converter = new ColorConverter();
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Color tempColor;
            foreach (var color in splited)
            {
                try
                {
                    tempColor = (Color)converter.ConvertFromString(color);
                }
                catch (Exception)
                {
                    throw new Exception("Can not convert" + color);
                }
                wordsBrushes.Add(new SolidBrush(tempColor));
            }
            return this;
        }

        public string GetDescription()
        {
            return "Параметр задаёт цвета раскраски слов.\nИспользование:\ncolors:<[color1],[color2],...>";
        }
    }
}

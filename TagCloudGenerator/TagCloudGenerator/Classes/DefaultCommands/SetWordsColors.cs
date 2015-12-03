using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetWordsColors : ICommand
    {
        public SetWordsColors(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            ParentParser.Cloud.Generator.WordsBrushes = _wordsBrushes;
        }

        private List<SolidBrush> _wordsBrushes;

        public ICommand CreateCommand(string stringCommand)
        {
            _wordsBrushes = new List<SolidBrush>();
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
                _wordsBrushes.Add(new SolidBrush(tempColor));
            }
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "colors";
        }

        public string GetDescription()
        {
            return "Параметр задаёт цвета раскраски слов.\nИспользование:\ncolors:<[color1],[color2],...>";
        }
    }
}

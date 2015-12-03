using System;
using System.Drawing;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetSize : ICommand
    {
        public SetSize(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            ParentParser.Cloud.Size = _size;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "size:[0-9]+,[0-9]+";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            var splitted = stringCommand.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
            _size = new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "size";
        }

        private Size _size;

        public string GetDescription()
        {
            return "Параметр задаёт размер выходного изображения.\nИспользование:\nsize:[width],[height]";
        }
    }
}

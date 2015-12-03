using System;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetWordsScale : ICommand
    {
        public SetWordsScale(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            ParentParser.Cloud.WordScale = _wordScale;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "scale:[1-9]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand[6].ToString();
            _wordScale = int.Parse(stringCommand);
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "scale";
        }

        private int _wordScale;

        public string GetDescription()
        {
            return "Параметр задаёт отношение высоты слов к высоте изображения.\nИспользование:\nscale:[1-9]";
        }
    }
}

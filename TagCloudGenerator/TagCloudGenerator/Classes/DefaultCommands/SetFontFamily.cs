using System;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetFontFamily : ICommand
    {
        public SetFontFamily(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            ParentParser.Cloud.FontFamily = _fontFamily;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "font:[a-zA-Z ]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            _fontFamily = stringCommand;
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "font";
        }

        private string _fontFamily;

        public string GetDescription()
        {
            return "Задаёт шрифт печати слов.\nИспользование:\nfont:[Font name]";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetBoringWords : ICommand
    {
        public SetBoringWords(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            ParentParser.Cloud.TextHandler.BoringWords = _boringWords;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "boring:<.+>";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            _boringWords = new HashSet<string>();
            foreach (var word in splited)
                _boringWords.Add(word);
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "boring";
        }

        private HashSet<string> _boringWords;

        public string GetDescription()
        {
            return "Параметр задаёт список скучных слов.\nИспользование:\nboring:<[word1],[word2],...>";
        }
    }
}

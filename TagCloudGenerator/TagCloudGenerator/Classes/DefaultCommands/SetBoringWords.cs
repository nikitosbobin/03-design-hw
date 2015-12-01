using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Ninject.Infrastructure.Language;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetBoringWords : ICommand
    {
        public void Execute(ICloudImageGenerator cloud)
        {
            cloud.TextHandler.BoringWords = boringWords;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "boring:<.+>";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(8, stringCommand.Length - 9);
            var splited = stringCommand.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            boringWords = new HashSet<string>();
            foreach (var word in splited)
                boringWords.Add(word);
            return this;
        }

        private HashSet<string> boringWords;

        public string GetDescription()
        {
            return "Параметр задаёт список скучных слов.\nИспользование:\nboring:<[word1],[word2],...>";
        }
    }
}

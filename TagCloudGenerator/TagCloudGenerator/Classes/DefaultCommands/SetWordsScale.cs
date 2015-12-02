﻿using System;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetWordsScale : ICommand
    {
        public void Execute(ICloudGenerator cloud)
        {
            cloud.WordScale = _wordScale;
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

        private int _wordScale;

        public string GetDescription()
        {
            return "Параметр задаёт отношение высоты слов к высоте изображения.\nИспользование:\nscale:[1-9]";
        }
    }
}

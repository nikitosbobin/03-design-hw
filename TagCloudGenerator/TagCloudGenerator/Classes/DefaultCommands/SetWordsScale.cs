using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetWordsScale : ICommand
    {
        public void Execute(ICloudImageGenerator cloud)
        {
            cloud.WordScale = wordScale;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "scale:[1-9]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand[6].ToString();
            wordScale = int.Parse(stringCommand);
            return this;
        }

        private int wordScale;

        public string GetDescription()
        {
            return "Параметр задаёт отношение высоты слов к высоте изображения.\nИспользование:\nscale:[1-9]";
        }
    }
}

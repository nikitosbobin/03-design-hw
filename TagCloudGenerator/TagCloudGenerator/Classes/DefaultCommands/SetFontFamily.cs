using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetFontFamily : ICommand
    {
        public void Execute(ICloudImageGenerator cloud)
        {
            cloud.FontFamily = fontFamily;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "font:[a-zA-Z ]";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            fontFamily = stringCommand;
            return this;
        }


        private string fontFamily;

        public string GetDescription()
        {
            return "Задаёт шрифт печати слов.\nИспользование:\nfont:[Font name]";
        }
    }
}

using System;
using System.Drawing;
using System.Text.RegularExpressions;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetSize : ICommand
    {
        public void Execute(ICloudImageGenerator cloud)
        {
            cloud.Size = size;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            var pattern = "size:[0-9]+,[0-9]+";
            if (!Regex.IsMatch(stringCommand, pattern))
                throw new Exception();
            stringCommand = stringCommand.Substring(5);
            var splitted = stringCommand.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries);
            size = new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
            return this;
        }

        private Size size;

        public string GetDescription()
        {
            return "Параметр задаёт размер выходного изображения.\nИспользование:\nsize:[width],[height]";
        }
    }
}

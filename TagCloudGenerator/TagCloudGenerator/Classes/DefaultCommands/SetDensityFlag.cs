using System;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class SetDensityFlag : ICommand
    {
        public void Execute(ICloudGenerator cloud)
        {
            cloud.MoreDensity = true;
        }

        public ICommand CreateCommand(string stringCommand)
        {
            if (stringCommand != "moreDensity")
                throw new Exception();
            return this;
        }

        public string GetDescription()
        {
            return "Задаёт флаг повышенной плотности размещения слов в облаке.\nИспользование:\nmoreDensity";
        }
    }
}

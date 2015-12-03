using System;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes.DefaultCommands
{
    class Help : ICommand
    {
        public Help(CommandsParser parser)
        {
            ParentParser = parser;
        }

        public void Execute()
        {
            foreach (var command in ParentParser.RegisteredCommands)
            {
                Console.WriteLine(command.Value.GetDescription());
            }
        }

        public ICommand CreateCommand(string stringCommand)
        {
            if (stringCommand != "help")
                throw new Exception();
            return this;
        }

        public CommandsParser ParentParser { get; }

        public string GetKeyWord()
        {
            return "help";
        }

        public string GetDescription()
        {
            return "Выводит описание всех возможных команд.\nИспользование:\nhelp";
        }
    }
}

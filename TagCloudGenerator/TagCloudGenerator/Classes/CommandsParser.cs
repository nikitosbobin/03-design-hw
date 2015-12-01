using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TagCloudGenerator.Classes.DefaultCommands;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class CommandsParser
    {
        public bool CreateCommands(string[] args)
        {
            if (!File.Exists(args[0]))
               return false;
            var pattern = ".+:|.+$";
            commands = new HashSet<ICommand>();
            string keyWord;
            foreach (var command in args.Skip(1))
            {
                keyWord = Regex.Match(command, pattern).ToString();
                if (registeredCommands.ContainsKey(keyWord))
                    commands.Add(registeredCommands[keyWord].CreateCommand(command));
            }
            return true;
        }

        private HashSet<ICommand> commands;

        public ICommand[] GetCommands()
        {
            return commands.ToArray();
        }

        private Dictionary<string, ICommand> registeredCommands = new Dictionary<string, ICommand>
        {
            { "size:", new SetSize() },
            { "scale:", new SetWordsScale() },
            { "moreDensity", new SetDensityFlag() },
            { "boring:", new SetBoringWords() },
            { "font:", new SetFontFamily() },
            { "colors:", new SetWordsColors()}
        };

        public bool AddForeignCommand(string keyWord, ICommand command)
        {
            try
            {
                registeredCommands.Add(keyWord, command);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

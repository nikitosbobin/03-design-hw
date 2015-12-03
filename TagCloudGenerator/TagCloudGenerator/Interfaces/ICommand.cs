using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ICommand
    {
        void Execute();
        ICommand CreateCommand(string stringCommand);
        CommandsParser ParentParser { get; }
        string GetKeyWord();
        string GetDescription();
    }
}

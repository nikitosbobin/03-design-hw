namespace TagCloudGenerator.Interfaces
{
    interface ICommand
    {
        void Execute(ICloudGenerator cloud);
        ICommand CreateCommand(string stringCommand);
        string GetDescription();
    }
}

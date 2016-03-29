using System;

namespace TagCloudGenerator.Classes
{
    class ConsoleLogger
    {
        public ConsoleLogger(int maxIndex)
        {
            this.maxIndex = maxIndex;
            statusLength = Console.WindowWidth - 11;
            currentIndex = 0;
        }

        private int currentIndex;
        private readonly int maxIndex;
        private int titleConsoleLine;
        private readonly int statusLength;

        public void LogTitle(string title)
        {
            titleConsoleLine = Console.CursorTop;
            Console.WriteLine(title);
        }

        public void LogStatus()
        {
            var status = (int)(statusLength * ((currentIndex + 1) / (double)maxIndex));
            Console.SetCursorPosition(0, titleConsoleLine + 2);
            Console.Write("Status: [" + new string('=', status) + ">" + new string(' ', statusLength - status) + "]");
            currentIndex++;
        }
    }
}

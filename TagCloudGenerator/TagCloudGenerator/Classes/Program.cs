using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagCloudGenerator.Classes;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextDecoder inputText = new TxtDecoder(args[0]);
            ITextParser parsedText = new SimpleTextParser(inputText);
            var readyText = parsedText.GetWords();
        }
    }
}

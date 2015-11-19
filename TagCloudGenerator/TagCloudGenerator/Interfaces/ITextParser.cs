using System.Collections.Generic;
using TagCloudGenerator.Classes;

namespace TagCloudGenerator.Interfaces
{
    interface ITextParser
    {
        HashSet<string> BoringWords { get; set; }
        Word[] Words { get; }
    }
}

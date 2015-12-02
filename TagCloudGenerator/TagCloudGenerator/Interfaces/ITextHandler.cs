using System.Collections.Generic;

namespace TagCloudGenerator.Interfaces
{
    interface ITextHandler
    {
        HashSet<string> BoringWords { get; set; }
        IEnumerable<IWord> GetWords(ITextDecoder decoder);
    }
}

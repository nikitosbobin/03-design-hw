using System;
using System.IO;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class TxtDecoder : ITextDecoder
    {
        public TxtDecoder(string path)
        {
           Path = path;
        }

        public string Path { get; set; }

        public string[] GetDecodedText()
        {
            try
            {
                var tmpText = File.ReadAllLines(Path);
                var tmpTextSplitted = tmpText.Select(s => s.Split(new[] {'[',']','$', '>', '{', '}', '=', ' ', '.', ',',
                    ':', '-', '!', '?', '(',')', '\'', '"', '`', ';', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' },StringSplitOptions.RemoveEmptyEntries));
                return tmpTextSplitted.SelectMany(c => c.Select(t => t.ToUpper())).ToArray();
            }
            catch
            {
                return new string[0];
            }
        }
    }
}

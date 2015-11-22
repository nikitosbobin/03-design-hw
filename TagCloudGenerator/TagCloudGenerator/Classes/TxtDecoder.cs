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
                return tmpText.Select(line => line.ToLower()).ToArray();
            }
            catch
            {
                return new string[0];
            }
        }
    }
}

using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            ITextDecoder inputText = new TxtDecoder(args[0]);
            ITextParser parsedText = new SimpleTextParser(inputText);
            var tagCloud = new TagCloud(parsedText, 1000, 1000);
            IImageEncoder encoder = new PngEncoder(tagCloud);
            encoder.SaveImage("out");
        }
    }
}

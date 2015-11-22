namespace TagCloudGenerator.Classes
{
    class Word
    {
        public Word(string source, int frequency = 1)
        {
            Source = source.ToLower();
            Frequency = frequency;
        }

        public string Source { get; set; }
        public int Frequency { get; set; }
    }
}

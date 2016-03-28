using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class SimpleTextHandler : ITextHandler
    {
        public SimpleTextHandler(IEnumerable<string> boringWords)
        {
            BoringWords = new HashSet<string>(boringWords ?? new string[0]);
        }

        private Dictionary<string, WordBlock> innerWords;
        private string[] decodedLines;
        public HashSet<string> BoringWords { get; set; }
        private WordBlock[] words;

        public IEnumerable<IWordBlock> GetWords(ITextDecoder decoder)
        {
            decodedLines = decoder.GetDecodedText();
            CreateInnerWords();
            return words;
        }

        private bool IsItRightWord(string word)
        {
            return word.Length > 5 && !BoringWords.Contains(word);
        }

        private void CreateInnerWords()
        {
            innerWords = new Dictionary<string, WordBlock>();
            foreach (var word in decodedLines)
            {
                if (IsItRightWord(word))
                {
                    if (innerWords.ContainsKey(word))
                        innerWords[word].Frequency++;
                    else
                        innerWords.Add(word, new WordBlock(word));
                }
                else
                    if (!BoringWords.Contains(word))
                        BoringWords.Add(word);
            }
            words = innerWords.Select(pair => pair.Value).ToArray();
        }
    }
}
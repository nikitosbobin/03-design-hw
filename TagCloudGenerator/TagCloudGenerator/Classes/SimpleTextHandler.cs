using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class SimpleTextHandler : ITextHandler
    {
        public SimpleTextHandler(params string[] boringWords)
        {
            BoringWords = new HashSet<string>(boringWords ?? new string[0]);
        }

        private Dictionary<string, WordBlock> _innerWords;
        private string[] _decodedLines;
        public HashSet<string> BoringWords { get; set; }
        private WordBlock[] _words;

        public IEnumerable<IWordBlock> GetWords(ITextDecoder decoder)
        {
            _decodedLines = decoder.GetDecodedText();
            CreateInnerWords();
            return _words;
        }

        private bool IsItRightWord(string word)
        {
            return word.Length > 5 && !BoringWords.Contains(word);
        }

        private void CreateInnerWords()
        {
            _innerWords = new Dictionary<string, WordBlock>();
            foreach (var word in _decodedLines)
            {
                if (IsItRightWord(word))
                {
                    if (_innerWords.ContainsKey(word))
                        _innerWords[word].Frequency++;
                    else
                        _innerWords.Add(word, new WordBlock(word));
                }
                else
                    if (!BoringWords.Contains(word))
                        BoringWords.Add(word);
            }
            _words = _innerWords.Select(pair => pair.Value).ToArray();
        }
    }
}
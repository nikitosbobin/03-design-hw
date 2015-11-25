using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class SimpleTextHandler : ITextHandler
    {
        public SimpleTextHandler(params string[] boringWords)
        {
            BoringWords = new HashSet<string>();
            if (boringWords != null && boringWords.Length != 0)
                foreach (var boringWord in boringWords)
                {
                    if (!BoringWords.Contains(boringWord.ToLower()))
                        BoringWords.Add(boringWord.ToLower());
                }
        }

        private Dictionary<string, Word> innerWords;
        private string[] decodedLines;
        public HashSet<string> BoringWords { get; set; }
        private Word[] words;

        public IEnumerable<Word> GetWords(ITextDecoder decoder)
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
            innerWords = new Dictionary<string, Word>();
            foreach (var word in decodedLines)
            {
                if (IsItRightWord(word))
                {
                    if (innerWords.ContainsKey(word))
                        innerWords[word].Frequency++;
                    else
                        innerWords.Add(word, new Word(word));
                }
                else
                    if (!BoringWords.Contains(word))
                        BoringWords.Add(word);
            }
            words = innerWords.Select(pair => pair.Value).ToArray();
        }
    }
}
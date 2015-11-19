﻿using System.Collections.Generic;
using System.Linq;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Classes
{
    class SimpleTextParser : ITextParser
    {
        public SimpleTextParser(ITextDecoder decoder, params string[] boringWords)
        {
            decodedLines = decoder.GetDecodedText();
            BoringWords = new HashSet<string>();
            if (boringWords != null && boringWords.Length != 0)
                foreach (var boringW in boringWords)
                {
                    if (!BoringWords.Contains(boringW.ToLower()))
                        BoringWords.Add(boringW.ToLower());
                }
        }

        public HashSet<string> BoringWords { get; set; }
        private Dictionary<string, Word> innerWords;
        public Word[] Words {
            get
            {
                MakeText();
                return innerWords.Select(pair => pair.Value).ToArray();
            }
            private set { }
        }
        private string[] decodedLines;

        private void MakeText()
        {
            innerWords = new Dictionary<string, Word>();
            foreach (var word in decodedLines)
            {
                if (word.Length > 5 && !BoringWords.Contains(word))
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
        }

        public HashSet<string> GetBoringWords()
        {
            return BoringWords;
        }
    }
}

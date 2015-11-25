﻿using NUnit.Framework;
using System.Linq;
using TagCloudGenerator.Classes;
using TagCloudGenerator.Interfaces;

namespace TagCloudGenerator.Tests
{
    class DecoderForTests : ITextDecoder
    {
        public DecoderForTests(string[] input)
        {
            wordsForTests = input;
        }

        private string[] wordsForTests;

        public string[] GetDecodedText()
        {
            return wordsForTests;
        }
    }

    [TestFixture]
    class SimpleTextHandler_Should
    {
        public static void DoTest(string[] words, Word[] expected, string[] boring = null)
        {
            ITextDecoder fakeDecoder = new DecoderForTests(words);
            var handler = new SimpleTextHandler(boring);
            Word[] actual = handler.GetWords(fakeDecoder).ToArray();
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i].Source, actual[i].Source);
                Assert.AreEqual(expected[i].Frequency, actual[i].Frequency);
            }
        }

        [Test]
        public static void CreateWords()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook" };
            Word[] expected = { new Word("smartphone"), new Word("notebook") };
            DoTest(words, expected);
        }

        [Test]
        public static void CreateWordsWithoutBoringWords()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook", "unittesting" };
            Word[] expected = { new Word("smartphone"), new Word("notebook") };
            string[] boring = { "unittesting" };
            DoTest(words, expected, boring);
        }
    }
}

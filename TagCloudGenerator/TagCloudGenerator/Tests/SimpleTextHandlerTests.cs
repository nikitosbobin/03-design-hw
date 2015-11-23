using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
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
    class SimpleTextHandlerTests
    {
        [Test]
        public static void ShouldCreateWords()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook" };
            Word[] expected = { new Word("smartphone"), new Word("notebook") };
            ITextDecoder fakeDecoder = new DecoderForTests(words);
            var handler = new SimpleTextHandler();

            Word[] actual = handler.GetWords(fakeDecoder).ToArray();
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i].Source, actual[i].Source);
                Assert.AreEqual(expected[i].Frequency, actual[i].Frequency);
            }
        }

        [Test]
        public static void ShouldCreateWordsWithoutBoring()
        {
            string[] words = { "word", "a", "an", "no", "by", "smartphone", "notebook", "unittesting" };
            Word[] expected = { new Word("smartphone"), new Word("notebook") };
            string[] boring = { "unittesting" };
            ITextDecoder fakeDecoder = new DecoderForTests(words);
            var handler = new SimpleTextHandler(boring);

            Word[] actual = handler.GetWords(fakeDecoder).ToArray();
            for (int i = 0; i < expected.Length; ++i)
            {
                Assert.AreEqual(expected[i].Source, actual[i].Source);
                Assert.AreEqual(expected[i].Frequency, actual[i].Frequency);
            }
        }
    }
}

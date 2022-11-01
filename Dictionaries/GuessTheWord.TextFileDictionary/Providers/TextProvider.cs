using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GuessTheWord.Abstractions.Providers;

namespace GuessTheWord.TextFileDictionary.Providers
{
    /// <summary>
    /// Текстовый провайдер
    /// </summary>
    internal sealed class TextProvider : IDictionaryProvider
    {
        private readonly string[] languages = {"ru_RU", "ru"};
        private readonly Dictionary<int, List<string>> words = new();
        
        /// <inheritdoc />
        public IEnumerable<string> Languages => languages;

        /// <inheritdoc />
        public IEnumerable<string> GetWords(ISearchRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (words.Count == 0)
            {
                var stream = typeof(TextProvider).Assembly.GetManifestResourceStream("GuessTheWord.TextFileDictionary.Resources.russian.txt");
                var allWords = SplitToLines(stream);
                foreach (var word in allWords)
                {
                    if (!words.TryGetValue(word.Length, out var lst))
                    {
                        words[word.Length] = lst = new List<string>();
                    }

                    lst.Add(word.ToLower());
                }
            }

            var lettersCount = request.LettersCount;
            if (lettersCount == null)
            {
                return words.Values.SelectMany(a => a);
            }

            if (!words.TryGetValue((int)lettersCount, out var wordsToSearch))
            {
                return Enumerable.Empty<string>();
            }

            return wordsToSearch;
        }

        private static IEnumerable<string> SplitToLines(Stream stream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}
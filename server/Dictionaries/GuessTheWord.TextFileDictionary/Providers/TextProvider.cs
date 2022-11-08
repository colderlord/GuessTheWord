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
        private readonly string[] languages = {"ru-RU"};
        private readonly Dictionary<string, Dictionary<int, List<string>>> wordsByCulture = new();
        
        /// <inheritdoc />
        public IEnumerable<string> Languages => languages;

        /// <inheritdoc />
        public IEnumerable<string> GetWords(ISearchRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var words = CheckAndInit(request.Culture);

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

        /// <inheritdoc />
        public bool HasWord(string word, string culture)
        {
            var words = CheckAndInit(culture);
            return words.Values.SelectMany(a => a).Contains(word);
        }

        private Dictionary<int, List<string>> CheckAndInit(string culture)
        {
            if (!wordsByCulture.TryGetValue(culture, out var words))
            {
                words = wordsByCulture[culture] = new Dictionary<int, List<string>>();
                if (culture == "ru-RU")
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
            }

            return words;
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
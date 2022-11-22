using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Searcher;

namespace GuessTheWord.Engine.Searcher
{
    /// <inheritdoc />
    public sealed class WordSearcher : IWordSearcher
    {
        private List<string> words;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="words">Слова</param>
        public WordSearcher(IEnumerable<string> words)
        {
            this.words = words.ToList();
        }

        /// <inheritdoc />
        public string[] GetWords(ISearchWordModel searchWordModel)
        {
            return GetWordsInternal(searchWordModel);
        }

        /// <inheritdoc />
        public bool IsWord(string word, ISearchSettingsBase settings)
        {
            return true;
        }

        private string[] GetWordsInternal(ISearchWordModel searchWordModel)
        {
            var searchSettings = searchWordModel.SearchSettings;
            var letters = searchWordModel.Letters;
            string[] results;
            if (letters == null || letters.Length == 0)
            {
                results = words
                    .Where(CheckUnique)
                    .ToArray()
                    .Take(searchSettings.MaxResults)
                    .ToArray();
            }
            else
            {
                var checkedLetters = new List<ILetterModel>();
                foreach (var letter in letters)
                {
                    checkedLetters.Add(letter);
                    if (letter.Option == LetterOption.Any)
                    {
                        var noneLetter = checkedLetters.FirstOrDefault(a => a.Value == letter.Value && a.Option == LetterOption.None);
                        if (noneLetter != null)
                        {
                            checkedLetters.Remove(noneLetter);
                        }
                        continue;
                    }

                    if (letter.Option == LetterOption.None)
                    {
                        var anyLetter = checkedLetters.FirstOrDefault(a => a.Value == letter.Value && a.Option == LetterOption.Any);
                        if (anyLetter != null)
                        {
                            checkedLetters.Remove(letter);
                        }
                        continue;
                    }
                }

                letters = checkedLetters.ToArray();

                words = words
                    .Where(w => CheckWord(w, letters))
                    .ToList();
                results = words
                    .Take(searchSettings.MaxResults)
                    .ToArray();
            }

            return results;
        }

        private bool CheckUnique(string word)
        {
            return word.Length == word.Distinct().ToArray().Length;
        }

        private static bool CheckWord(string word, ILetterModel[] letters)
        {
            var fixedLetters = letters.Where(a => a.Option == LetterOption.Fixed);
            if (fixedLetters.Any())
            {
                if (!fixedLetters.All(a => a.Value == word[(int) a.Position]))
                {
                    return false;
                }
            }

            var usedLetters = letters.Where(a => a.Option == LetterOption.None);
            if (usedLetters.Any(letter => word.Contains(letter.Value)))
            {
                return false;
            }

            var anyLetters = letters.Where(a => a.Option == LetterOption.Any);
            if (anyLetters.Any())
            {
                if (!anyLetters.All(a => word.Contains(a.Value) && a.Value != word[(int) a.Position]))
                {
                    return false;
                }
            }

            var anotherLetters = letters.Where(a => a.Option == LetterOption.Default);
            if (anotherLetters.Any(letter => word.Contains(letter.Value)))
            {
                return true;
            }

            return false;
        }
    }
}
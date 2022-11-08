using System;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Abstractions.Searcher;
using GuessTheWord.Engine.Helpers;
using GuessTheWord.Engine.Models;
using GuessTheWord.Engine.Searcher;

namespace GuessTheWord.TryGuessGame.Games
{
    /// <summary>
    /// Отгадай слово
    /// </summary>
    internal sealed class TryGuessGame : IGame
    {
        private IWordSearcher wordSearcher;
        private readonly IRule rule;
        private readonly IDictionaryProvider provider;
        private readonly IAlphabet alphabet;
        private readonly HashSet<char> usedLetters = new();
        private readonly List<ILetterModel> processedLetterModels = new();
        private int attempt;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="rule">Правила</param>
        /// <param name="provider">Провайдер словаря</param>
        /// <param name="alphabet">Модель алфавита</param>
        public TryGuessGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            this.rule = rule;
            this.provider = provider;
            this.alphabet = alphabet;
        }

        /// <inheritdoc />
        public IGameResult Play(string word)
        {
            var calcAttempts = true;
            if (string.IsNullOrWhiteSpace(word))
            {
                calcAttempts = false;
                if (usedLetters.Count > 0 || processedLetterModels.Count > 0)
                {
                    calcAttempts = true;
                }
            }
            else if (word.Length != rule.LettersCount)
            {
                return GameResult.Fail("Длина слова не соответствует длине по правилам");
            }

            if (calcAttempts)
            {
                attempt += 1;
                if (attempt > rule.Attempts)
                {
                    return GameResult.Fail("Превышено количество попыток");
                }
            }

            if (wordSearcher == null)
            {
                var providerResponse = provider.GetWords(new SearchRequest(rule.Culture, rule.LettersCount));
                wordSearcher = new WordSearcher(providerResponse);
            }

            var searchSettings = new SearchSettings(rule.Culture, alphabet);
            if (!wordSearcher.IsWord(word, searchSettings))
            {
                return GameResult.Fail("Переданное значение не является словом");
            }

            ISearchWordModel searchWordModel;
            if (string.IsNullOrWhiteSpace(word))
            {
                // Если слова нет - значит выдать все возможные слова
                searchWordModel = new SearchWordModel(Array.Empty<ILetterModel>(), searchSettings);
            }
            else
            {
                var letterModels = WordParser.ParseWord(word, rule.LettersCount);
                foreach (var letterModel in letterModels)
                {
                    usedLetters.Add(letterModel.Value);
                    processedLetterModels.Add(letterModel);
                }

                var letters = alphabet.AlphabetLetters;
                var lettersToSearch = processedLetterModels.Concat(letters.Except(usedLetters).Select(a => new LetterModel(a))).ToArray();

                searchWordModel = new SearchWordModel(lettersToSearch, searchSettings);
            }

            var result = wordSearcher.GetWords(searchWordModel).ToArray();
            if (result.Length == 0)
            {
                return GameResult.Fail("Не найдены подходящие варианты");
            }

            return GameResult.Ok(result);
        }
    }
}
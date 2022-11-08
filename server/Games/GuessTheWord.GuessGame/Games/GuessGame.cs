using System;
using System.Linq;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Abstractions.Searcher;
using GuessTheWord.Engine.Models;
using GuessTheWord.Engine.Searcher;

namespace GuessTheWord.GuessGame.Games
{
    /// <summary>
    /// Загадай слово
    /// </summary>
    internal sealed class GuessGame : IGame
    {
        private IWordSearcher wordSearcher;
        private readonly IRule rule;
        private readonly IDictionaryProvider provider;
        private readonly IAlphabet alphabet;
        private int attempt;
        private string guessWord;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="rule">Правила</param>
        /// <param name="provider">Провайдер словаря</param>
        /// <param name="alphabet">Модель алфавита</param>
        public GuessGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            this.rule = rule;
            this.provider = provider;
            this.alphabet = alphabet;
        }

        /// <inheritdoc />
        public IGameResult Play(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                return GameResult.Fail("Слово не может быть пустым");
            }

            if (rule.LettersCount != word.Length)
            {
                return GameResult.Fail("Длина слова не соответсвует правилам");
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

            if (attempt == 0)
            {
                // Загадываем слово
                var searchWordModel = new SearchWordModel(Array.Empty<ILetterModel>(), searchSettings);
                var result = wordSearcher.GetWords(searchWordModel).ToArray();
                var index = new Random().Next(0, result.Length - 1);
                guessWord = result[index].ToLower();
            }

            attempt += 1;
            if (attempt > rule.Attempts)
            {
                return GameResult.Fail("Превышено количество попыток");
            }

            var letters = new ILetterModel[word.Length];
            for (int i = 0; i < word.Length; i++)
            {
                var letter = word[i];
                if (guessWord[i] == letter)
                {
                    letters[i] = new LetterModel(LetterOption.Fixed, i, letter);
                    continue;
                }

                if (guessWord.IndexOf(letter) != -1)
                {
                    letters[i] = new LetterModel(LetterOption.Any, i, letter);
                    continue;
                }

                letters[i] = new LetterModel(LetterOption.None, i, letter);
            }

            return GameResult.Ok(letters);
        }
    }
}
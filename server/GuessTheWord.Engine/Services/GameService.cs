using System;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Engine.Games;
using GuessTheWord.Engine.Models;

namespace GuessTheWord.Engine.Services
{
    /// <inheritdoc />
    internal sealed class GameService : IGameService
    {
        private readonly IEnumerable<IDictionaryProvider> providers;
        private readonly IEnumerable<IAlphabet> alphabets;
        private readonly IRule defaultRule = new Rule("ru_RU", 5, 6);
        private IGame game;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="providers">Провайдеры</param>
        /// <param name="alphabets">Алфавиты</param>
        public GameService(IEnumerable<IDictionaryProvider> providers, IEnumerable<IAlphabet> alphabets)
        {
            this.providers = providers;
            this.alphabets = alphabets;
        }

        /// <inheritdoc />
        public void SetRules(string locale, short lettersCount, short attempts)
        {
            if (game != null)
            {
                throw new Exception("Игра уже началась. Сбросьте игру, либо продолжите существующую");
            }

            var rule = new Rule(locale, lettersCount, attempts);
            game = CreateGame(rule);
        }

        /// <inheritdoc />
        public void Restart()
        {
            game = null;
        }

        /// <inheritdoc />
        public string[] Put(string word)
        {
            game ??= CreateGame(defaultRule);

            return game.TryGuess(word);
        }

        private IGame CreateGame(IRule rule)
        {
            var culture = rule.Culture;
            var provider = providers.FirstOrDefault(a => a.Languages.Contains(culture));
            var alphaBet = GetAlphabet(culture);
            return new TryGuessGame(rule, provider, alphaBet);
        }

        private IAlphabet GetAlphabet(string culture)
        {
            var alphabet = alphabets.FirstOrDefault(a => a.CheckCulture(culture));
            if (alphabet == null)
            {
                return null;
            }

            return alphabet;
        }
    }
}
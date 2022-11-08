using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Engine.Models;

namespace GuessTheWord.Engine.Services
{
    /// <inheritdoc />
    internal sealed class GameService : IGameService
    {
        private readonly IEnumerable<IDictionaryProvider> providers;
        private readonly IEnumerable<IAlphabet> alphabets;
        private readonly IEnumerable<IGameProvider> gameProviders;
        private readonly IRule defaultRule = new Rule("ru-RU", 5, 6);
        private readonly ConcurrentDictionary<Guid, IGame> games = new();

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="providers">Провайдеры</param>
        /// <param name="alphabets">Алфавиты</param>
        /// <param name="gameProviders"></param>
        public GameService(IEnumerable<IDictionaryProvider> providers, IEnumerable<IAlphabet> alphabets, IEnumerable<IGameProvider> gameProviders)
        {
            this.providers = providers;
            this.alphabets = alphabets;
            this.gameProviders = gameProviders;
        }

        /// <inheritdoc />
        public void SetRules(Guid gameType, string locale, short lettersCount, short attempts)
        {
            if (games.TryGetValue(gameType, out var game))
            {
                Restart(gameType);
            }

            var rule = new Rule(locale, lettersCount, attempts);
            game = CreateGame(gameType, rule);
            games.TryAdd(gameType, game);
        }

        /// <inheritdoc />
        public void Restart(Guid gameType)
        {
            games.TryRemove(gameType, out _);
        }

        /// <inheritdoc />
        public IGameResult Put(Guid gameType, string word)
        {
            var game = games.GetOrAdd(gameType, t => CreateGame(t, defaultRule));

            return game.Play(!string.IsNullOrWhiteSpace(word) ? word.ToLower() : "");
        }

        private IGame CreateGame(Guid gameType, IRule rule)
        {
            var culture = rule.Culture;
            var provider = providers.FirstOrDefault(a => a.Languages.Contains(culture));
            var alphaBet = GetAlphabet(culture);
            var gameProvider = gameProviders.First(a => a.Uid == gameType);
            return gameProvider.CreateGame(rule, provider, alphaBet);
        }

        private IAlphabet GetAlphabet(string culture)
        {
            var alphabet = alphabets.FirstOrDefault(a => a.Culture == culture);
            if (alphabet == null)
            {
                return null;
            }

            return alphabet;
        }
    }
}
using System.Collections.Generic;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Abstractions.Searcher;

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
        private readonly HashSet<char> usedLetters = new();
        private readonly List<ILetterModel> processedLetterModels = new();
        private int attempt;

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
            throw new System.NotImplementedException();
        }
    }
}
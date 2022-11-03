using System;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;

namespace GuessTheWord.TryGuessGame.Providers
{
    /// <summary>
    /// Провайдер игры, пытающейся угадать заданное слово
    /// </summary>
    internal sealed class TryGuessGameProvider : IGameProvider
    {
        private static readonly Guid ProviderUid = new Guid("B32BB668-5448-4B1B-89C9-31138BC6EB63");

        /// <inheritdoc />
        public Guid Uid => ProviderUid;

        /// <inheritdoc />
        public string Name => "Я (компьютер) пытаюсь отгадать слово";

        /// <inheritdoc />
        public IGame CreateGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            return new Games.TryGuessGame(rule, provider, alphabet);
        }
    }
}
using System;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;

namespace GuessTheWord.GuessGame.Providers
{
    /// <summary>
    /// Провайдер игры, пытающейся угадать заданное слово
    /// </summary>
    internal sealed class GuessGameProvider : IGameProvider
    {
        private static readonly Guid ProviderUid = new Guid("793370CF-7EA5-4D45-AF27-65DEF7293E40");

        /// <inheritdoc />
        public Guid Uid => ProviderUid;

        /// <inheritdoc />
        public string Name => "Ты пытаешься отгадать слово";

        /// <inheritdoc />
        public IGame CreateGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            return new Games.GuessGame(rule, provider, alphabet);
        }
    }
}
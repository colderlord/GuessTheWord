using System;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Abstractions.Providers;

namespace GuessTheWord.Words.Providers
{
    /// <summary>
    /// Провайдер игры, пытающейся угадать заданное слово
    /// </summary>
    internal sealed class WordsGameProvider : IGameProvider
    {
        private static readonly Guid ProviderUid = new Guid("B5451229-E47D-4FCA-AD1B-C787C34D6279");

        /// <inheritdoc />
        public Guid Uid => ProviderUid;

        /// <inheritdoc />
        public string Name => "Игра в слова";

        /// <inheritdoc />
        public IGame CreateGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet)
        {
            throw new NotImplementedException();
        }
    }
}
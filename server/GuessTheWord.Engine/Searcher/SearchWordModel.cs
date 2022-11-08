using System;
using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Searcher
{
    /// <inheritdoc />
    public sealed class SearchWordModel : ISearchWordModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="letters">Модели букв</param>
        /// <param name="searchSettings">Настройки</param>
        public SearchWordModel(ILetterModel[] letters, ISearchSettings searchSettings)
        {
            Letters = letters ?? throw new ArgumentNullException(nameof(letters));
            SearchSettings = searchSettings ?? throw new ArgumentNullException(nameof(searchSettings));
        }

        /// <inheritdoc />
        public ILetterModel[] Letters { get; }

        /// <inheritdoc />
        public ISearchSettings SearchSettings { get; }
    }
}
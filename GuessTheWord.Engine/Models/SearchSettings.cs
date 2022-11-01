using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    internal sealed class SearchSettings : ISearchSettings
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="lettersCount">Количество букв в слове</param>
        /// <param name="culture">Локаль</param>
        /// <param name="maxResults">Максимальное количество результатов</param>
        public SearchSettings(short lettersCount, string culture, int maxResults = 100)
        {
            LettersCount = lettersCount;
            Culture = culture;
            MaxResults = maxResults;
        }

        /// <inheritdoc />
        public short LettersCount { get; }

        /// <inheritdoc />
        public string Culture { get; }

        /// <inheritdoc />
        public int MaxResults { get; }
    }
}
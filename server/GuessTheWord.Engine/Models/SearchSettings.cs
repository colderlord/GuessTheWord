using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    internal sealed class SearchSettings : ISearchSettings
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="culture">Локаль</param>
        /// <param name="maxResults">Максимальное количество результатов</param>
        public SearchSettings(string culture, int maxResults = 100)
        {
            Culture = culture;
            MaxResults = maxResults;
        }

        /// <inheritdoc />
        public string Culture { get; }

        /// <inheritdoc />
        public int MaxResults { get; }
    }
}
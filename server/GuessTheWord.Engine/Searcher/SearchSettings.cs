using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Searcher
{
    /// <inheritdoc />
    public sealed class SearchSettings : ISearchSettings
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="culture">Локаль</param>
        /// <param name="alphabet">Алфавит</param>
        /// <param name="maxResults">Максимальное количество результатов</param>
        public SearchSettings(string culture, IAlphabet alphabet, int maxResults = 100)
        {
            Culture = culture;
            MaxResults = maxResults;
        }

        /// <inheritdoc />
        public string Culture { get; }

        /// <inheritdoc />
        public IAlphabet Alphabet { get; }

        /// <inheritdoc />
        public int MaxResults { get; }
    }
}
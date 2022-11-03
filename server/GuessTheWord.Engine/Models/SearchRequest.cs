using GuessTheWord.Abstractions.Providers;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    public sealed class SearchRequest : ISearchRequest
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="culture">Культура</param>
        /// <param name="lettersCount">Количество букв</param>
        public SearchRequest(string culture, short? lettersCount)
        {
            LettersCount = lettersCount;
            Culture = culture;
        }

        /// <inheritdoc />
        public short? LettersCount { get; }

        /// <inheritdoc />
        public string Culture { get; }
    }
}
using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    internal sealed class Rule : IRule
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="locale">Локаль</param>
        /// <param name="lettersCount">Количество букв</param>
        /// <param name="attempts">Корличество попыток</param>
        public Rule(string locale, short lettersCount, short attempts)
        {
            LettersCount = lettersCount;
            Culture = locale;
            Attempts = attempts;
        }

        /// <inheritdoc />
        public string Culture { get; }

        /// <inheritdoc />
        public short LettersCount { get; }

        /// <inheritdoc />
        public short Attempts { get; }
    }
}
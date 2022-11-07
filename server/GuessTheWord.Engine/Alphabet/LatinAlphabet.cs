using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Alphabet
{
    /// <summary>
    /// Латинский алфавит
    /// </summary>
    internal sealed class LatinAlphabet : IAlphabet
    {
        private static readonly char[] AlphabetLet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        /// <inheritdoc />
        public string Name => "English";

        /// <inheritdoc />
        public string Culture => "en-US";

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
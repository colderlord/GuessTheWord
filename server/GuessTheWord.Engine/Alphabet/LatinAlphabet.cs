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
        public bool CheckCulture(string culture)
        {
            return culture == "en-GB" || culture == "en-US";
        }

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
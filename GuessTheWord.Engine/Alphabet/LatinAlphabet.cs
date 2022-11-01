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
            return culture == "en_GB" || culture == "en_US" || culture == "en";
        }

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
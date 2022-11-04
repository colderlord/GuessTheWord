using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Alphabet
{
    /// <summary>
    /// Русский алфавит
    /// </summary>
    internal sealed class RussianAlphabet : IAlphabet
    {
        private static readonly char[] AlphabetLet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя".ToCharArray();

        /// <inheritdoc />
        public bool CheckCulture(string culture)
        {
            return culture == "ru-RU";
        }

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
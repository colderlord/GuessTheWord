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
            return culture == "ru_RU" || culture == "ru-RU" || culture == "ru";
        }

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
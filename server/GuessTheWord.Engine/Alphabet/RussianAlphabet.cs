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
        public string Name => "Русский";

        /// <inheritdoc />
        public string Culture => "ru-RU";

        /// <inheritdoc />
        public char[] AlphabetLetters => AlphabetLet;
    }
}
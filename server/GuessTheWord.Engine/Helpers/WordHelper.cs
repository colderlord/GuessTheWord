using System.Linq;
using System.Text.RegularExpressions;
using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Helpers
{
    /// <summary>
    /// Помощник для слов
    /// </summary>
    public static class WordHelper
    {
        private const string regExFormat = "^[{0}-{1}]+$";

        /// <summary>
        /// Проверить, что введенное значение является словом
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="alphabet">Алфавит</param>
        /// <returns><c>true</c>, если введенное значение является словом</returns>
        public static bool IsWord(string word, IAlphabet alphabet)
        {
            return Regex.Match(word,
                string.Format(regExFormat, alphabet.AlphabetLetters.First(), alphabet.AlphabetLetters.Last())).Success;
        }
    }
}
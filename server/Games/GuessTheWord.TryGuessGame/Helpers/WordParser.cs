using System.Collections.Generic;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Engine.Models;
using GuessTheWord.TryGuessGame.Games;

namespace GuessTheWord.TryGuessGame.Helpers
{
    /// <summary>
    /// Обработчик слова
    /// </summary>
    internal static class WordParser
    {
        /// <summary>
        /// Обработать слово
        /// </summary>
        /// <param name="word">Слово</param>
        /// <returns>Модель слова</returns>
        internal static IEnumerable<ILetterModel> ParseWord(string word)
        {
            short letterPosition = 0;
            word = word.ToLower();
            for (short i = 0; i < word.Length; i++)
            {
                var charWord = word[i];
                switch (charWord)
                {
                    case '!':
                    {
                        ++i;
                        var letterModel = new LetterModel(LetterOption.Fixed, letterPosition, word[i]);
                        ++letterPosition;
                        yield return letterModel;
                        break;
                    }
                    case '$':
                    {
                        ++i;
                        var letterModel = new LetterModel(LetterOption.Any, letterPosition, word[i]);
                        ++letterPosition;
                        yield return letterModel;
                        break;
                    }
                    default:
                    {
                        var letterModel = new LetterModel(LetterOption.None, letterPosition, word[i]);
                        ++letterPosition;
                        yield return letterModel;
                        break;
                    }
                }
            }
        }
    }
}
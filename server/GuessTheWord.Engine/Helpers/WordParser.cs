using System;
using System.Collections.Generic;
using GuessTheWord.Abstractions.Models;
using GuessTheWord.Engine.Models;

namespace GuessTheWord.Engine.Helpers
{
    /// <summary>
    /// Обработчик слова
    /// </summary>
    public static class WordParser
    {
        /// <summary>
        /// Обработать слово
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="size">Длина слова</param>
        /// <returns>Модель слова</returns>
        public static ILetterModel[] ParseWord(string word, int size)
        {
            var result = new ILetterModel[size];
            int letterPosition = 0;
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
                        result[i] = letterModel;
                        break;
                    }
                    case '$':
                    {
                        ++i;
                        var letterModel = new LetterModel(LetterOption.Any, letterPosition, word[i]);
                        ++letterPosition;
                        result[i] = letterModel;
                        break;
                    }
                    default:
                    {
                        var letterModel = new LetterModel(LetterOption.None, letterPosition, word[i]);
                        ++letterPosition;
                        result[i] = letterModel;
                        break;
                    }
                }
            }

            if (letterPosition != size)
            {
                throw new Exception("Размер слова не соответствует размеру правил");
            }

            return result;
        }
    }
}
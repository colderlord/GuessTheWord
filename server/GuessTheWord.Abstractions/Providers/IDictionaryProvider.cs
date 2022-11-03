using System.Collections.Generic;

namespace GuessTheWord.Abstractions.Providers
{
    /// <summary>
    /// Провайдер словаря
    /// </summary>
    public interface IDictionaryProvider : IExtension
    {
        /// <summary>
        /// Языки
        /// </summary>
        IEnumerable<string> Languages { get; }

        /// <summary>
        /// Получить список слов
        /// </summary>
        /// <param name="request">Запрос на поиск слов</param>
        /// <returns>Список слов</returns>
        IEnumerable<string> GetWords(ISearchRequest request);

        /// <summary>
        /// Есть ли такое слово в словаре
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="culture">Культура</param>
        /// <returns><c>true</c>, если такое слово есть в словаре</returns>
        bool HasWord(string word, string culture);
    }
}
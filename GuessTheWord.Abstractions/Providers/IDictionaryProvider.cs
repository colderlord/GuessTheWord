using System.Collections.Generic;
using GuessTheWord.Abstractions.Models;

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
    }
}
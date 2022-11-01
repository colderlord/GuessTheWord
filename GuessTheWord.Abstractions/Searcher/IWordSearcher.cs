using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Abstractions.Searcher
{
    /// <summary>
    /// Поиск слова
    /// </summary>
    public interface IWordSearcher
    {
        /// <summary>
        /// Получить слова
        /// </summary>
        /// <param name="searchWordModel">Модель поиска слов</param>
        /// <returns>Варианты подошедших слов</returns>
        string[] GetWords(ISearchWordModel searchWordModel);
    }
}
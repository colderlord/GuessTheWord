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

        /// <summary>
        /// Является ли словом
        /// </summary>
        /// <param name="word">Слово</param>
        /// <param name="settings">Настройки</param>
        /// <returns><c>true</c>, если переданное значение является словом</returns>
        bool IsWord(string word, ISearchSettingsBase settings);
    }
}
namespace GuessTheWord.Abstractions.Providers
{
    /// <summary>
    /// Запрос на поиск слов в провайдер
    /// </summary>
    public interface ISearchRequest
    {
        /// <summary>
        /// Количество букв
        /// </summary>
        short? LettersCount { get; }

        /// <summary>
        /// В какой культуре нужны слова
        /// </summary>
        string Culture { get; }
    }
}
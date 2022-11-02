namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Настройки поиска
    /// </summary>
    public interface ISearchSettings
    {
        /// <summary>
        /// Максимальное количество результатов
        /// </summary>
        int MaxResults { get; }

        /// <summary>
        /// Культура
        /// </summary>
        string Culture { get; }
    }
}
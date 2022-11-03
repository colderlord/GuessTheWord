namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Настройки поиска
    /// </summary>
    public interface ISearchSettings : ISearchSettingsBase
    {
        /// <summary>
        /// Максимальное количество результатов
        /// </summary>
        int MaxResults { get; }
    }
}
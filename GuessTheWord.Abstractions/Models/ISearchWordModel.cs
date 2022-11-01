namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Модель слова
    /// </summary>
    public interface ISearchWordModel
    {
        /// <summary>
        /// Модели букв
        /// </summary>
        ILetterModel[] Letters { get; }

        /// <summary>
        /// Настройки поиска
        /// </summary>
        ISearchSettings SearchSettings { get; }
    }
}
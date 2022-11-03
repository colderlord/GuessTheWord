namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Базовые настройки поиска
    /// </summary>
    public interface ISearchSettingsBase
    {
        /// <summary>
        /// Культура
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// Алфавит
        /// </summary>
        IAlphabet Alphabet { get; }
    }
}
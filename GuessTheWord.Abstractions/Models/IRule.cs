namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Правила
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Количество букв в слове
        /// </summary>
        short LettersCount { get; }

        /// <summary>
        /// Культура
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// Количество попыток
        /// </summary>
        short Attempts { get; }
    }
}
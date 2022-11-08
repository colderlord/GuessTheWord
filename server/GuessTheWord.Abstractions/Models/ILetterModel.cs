namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Модель буквы
    /// </summary>
    public interface ILetterModel
    {
        /// <summary>
        /// Опции буквы
        /// </summary>
        LetterOption Option { get; }

        /// <summary>
        /// Позиция
        /// </summary>
        int? Position { get; }

        /// <summary>
        /// Значение буквы
        /// </summary>
        char Value { get; }
    }
}
namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Результат игры
    /// </summary>
    public interface IGameResult
    {
        /// <summary>
        /// Успешность
        /// </summary>
        bool Success { get; }

        /// <summary>
        /// Причина неуспешности
        /// </summary>
        string Reason { get; }

        /// <summary>
        /// Результат
        /// </summary>
        object Result { get; }
    }
}
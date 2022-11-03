using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    public class GameResult : IGameResult
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="result">Результат</param>
        /// <param name="success">Успешность результата</param>
        /// <param name="reason">Причина</param>
        public GameResult(object result, bool success, string reason)
        {
            Result = result;
            Success = success;
            Reason = reason;
        }

        /// <inheritdoc />
        public bool Success { get; }

        /// <inheritdoc />
        public string Reason { get; }

        /// <inheritdoc />
        public object Result { get; }

        /// <summary>
        /// Результат с ошибкой
        /// </summary>
        /// <param name="reason">Причина</param>
        /// <returns>Результат игры</returns>
        public static IGameResult Fail(string reason) => new GameResult(null, false, reason);

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="result">Результат</param>
        /// <returns>Результат игры</returns>
        public static IGameResult Ok(object result) => new GameResult(result, true, null);
    }
}
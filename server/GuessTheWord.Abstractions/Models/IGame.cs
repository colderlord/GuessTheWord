namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Интерфейс игры
    /// </summary>
    public interface IGame
    {
        /// <summary>
        /// Попытаться отгадать
        /// </summary>
        /// <param name="word">Слово в специальном формате</param>
        /// <returns>Список строк вариантов</returns>
        IGameResult Play(string word);
    }
}
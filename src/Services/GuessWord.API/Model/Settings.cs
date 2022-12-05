namespace GuessWord.API.Model
{
    /// <summary>
    /// Настройки игры
    /// </summary>
    public class Settings : Entity
    {
        /// <summary>
        /// Количество попыток
        /// </summary>
        public int Attempts { get; set; }

        /// <summary>
        /// Длина слова
        /// </summary>
        public int WordLength { get; set; }

        /// <summary>
        /// Язык
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Идентификатор игры
        /// </summary>
        public long? GuessGameId { get; set; }

        /// <summary>
        /// Игра
        /// </summary>
        public virtual GuessGame? GuessGame { get; set; }
    }
}
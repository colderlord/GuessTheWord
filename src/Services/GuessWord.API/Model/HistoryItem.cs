using System;

namespace GuessWord.API.Model
{
    /// <summary>
    /// Элемент истории
    /// </summary>
    public class HistoryItem : Entity
    {
        /// <summary>
        /// Модель слова
        /// </summary>
        public virtual WordModel WordModel { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }

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
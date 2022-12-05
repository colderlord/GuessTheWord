using System;
using System.Collections.Generic;

namespace GuessWord.API.Model
{
    /// <summary>
    /// Игра
    /// </summary>
    public class GuessGame : Entity
    {
        /// <summary>
        /// История
        /// </summary>
        public virtual List<HistoryItem> History { get; set; } = new();

        /// <summary>
        /// Настройки
        /// </summary>
        public virtual Settings Settings { get; set; }

        /// <summary>
        /// Дата создания игры
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Дата начала игры
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата завершения игры
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
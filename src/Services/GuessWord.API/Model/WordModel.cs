using System.Collections.Generic;

namespace GuessWord.API.Model
{
    /// <summary>
    /// Модель слова
    /// </summary>
    public class WordModel : Entity
    {
        /// <summary>
        /// Слово
        /// </summary>
        public string Word { get; set; }

        /// <summary>
        /// Буквы
        /// </summary>
        public virtual List<LetterModel> Letters { get; set; } = new ();

        /// <summary>
        /// Идентификатор элемента истории
        /// </summary>
        public long? HistoryItemId { get; set; }

        /// <summary>
        /// Элемент истории
        /// </summary>
        public virtual HistoryItem? HistoryItem { get; set; }
    }
}
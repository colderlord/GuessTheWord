namespace GuessWord.API.Model
{
    /// <summary>
    /// Модель буквы
    /// </summary>
    public class LetterModel : Entity
    {
        /// <summary>
        /// Буква
        /// </summary>
        public char Char { get; set; }

        /// <summary>
        /// Тип буквы
        /// </summary>
        public LetterType Type { get; set; }

        /// <summary>
        /// Позиция буквы
        /// </summary>
        public short Position { get; set; }

        /// <summary>
        /// Идентификатор модели слова
        /// </summary>
        public long? WordModelId { get; set; }

        /// <summary>
        /// Модель слова
        /// </summary>
        public virtual WordModel? WordModel { get; set; }
    }
}
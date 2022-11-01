namespace GuessTheWord.Api.Web.Models
{
    /// <summary>
    /// Модель правил
    /// </summary>
    public sealed class Rule
    {
        /// <summary>
        /// Язык
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Количество букв
        /// </summary>
        public short Letters { get; set; }

        /// <summary>
        /// Количество попыток
        /// </summary>
        public short Attempts { get; set; }
    }
}
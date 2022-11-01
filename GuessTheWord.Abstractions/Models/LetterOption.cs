namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Опции буквы
    /// </summary>
    public enum LetterOption
    {
        /// <summary>
        /// Буква не найдена в конечном слове
        /// </summary>
        None,

        /// <summary>
        /// Буква находится в конечном слове, но не известно в каком месте
        /// </summary>
        Any,

        /// <summary>
        /// Буква находится в конечном слове именно в указанном месте
        /// </summary>
        Fixed,

        /// <summary>
        /// Буква ещё не была использована
        /// </summary>
        Default
    }
}
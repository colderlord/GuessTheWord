namespace GuessWord.API.Model
{
    /// <summary>
    /// Тип буквы
    /// </summary>
    public enum LetterType
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
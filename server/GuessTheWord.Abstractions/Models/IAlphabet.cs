namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Алфивит
    /// </summary>
    public interface IAlphabet : IExtension
    {
        /// <summary>
        /// Наименование
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Локаль
        /// </summary>
        string Culture { get; }

        /// <summary>
        /// Буквы алфавита
        /// </summary>
        char[] AlphabetLetters { get; }
    }
}
namespace GuessTheWord.Abstractions.Models
{
    /// <summary>
    /// Алфивит
    /// </summary>
    public interface IAlphabet : IExtension
    {
        /// <summary>
        /// Проверить локаль
        /// </summary>
        /// <param name="culture">Локаль</param>
        /// <returns><c>true</c>, если локаль подходит</returns>
        bool CheckCulture(string culture);

        /// <summary>
        /// Буквы алфавита
        /// </summary>
        char[] AlphabetLetters { get; }
    }
}
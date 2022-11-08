using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Engine.Models
{
    /// <inheritdoc />
    public sealed class LetterModel : ILetterModel
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="letter">Буква</param>
        public LetterModel(char letter)
        {
            Option = LetterOption.Default;
            Value = letter;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="option">Опции буквы</param>
        /// <param name="position">Позиция</param>
        /// <param name="letter">Буква</param>
        public LetterModel(LetterOption option, int position, char letter)
        {
            Option = option;
            Position = position;
            Value = letter;
        }

        /// <inheritdoc />
        public LetterOption Option { get; }

        /// <inheritdoc />
        public int? Position { get; }

        /// <inheritdoc />
        public char Value { get; }
    }
}
using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Abstractions
{
    /// <summary>
    /// Сервис игры
    /// </summary>
    public interface IGameService : IExtension
    {
        /// <summary>
        /// Установить правила
        /// </summary>
        /// <param name="locale">Локаль</param>
        /// <param name="lettersCount">Количество букв</param>
        /// <param name="attempts">Корличество попыток</param>
        void SetRules(string locale, short lettersCount, short attempts);

        /// <summary>
        /// Перезапуск
        /// </summary>
        void Restart();

        /// <summary>
        /// Попытаться угадать
        /// </summary>
        /// <param name="word">Слово</param>
        /// <returns>Список предположений</returns>
        string[] Put(string word);
    }
}
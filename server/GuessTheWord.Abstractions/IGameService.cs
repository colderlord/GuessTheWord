using System;
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
        /// <param name="gameType">Тип игры</param>
        /// <param name="locale">Локаль</param>
        /// <param name="lettersCount">Количество букв</param>
        /// <param name="attempts">Корличество попыток</param>
        void SetRules(Guid gameType, string locale, short lettersCount, short attempts);

        /// <summary>
        /// Перезапуск
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        void Restart(Guid gameType);

        /// <summary>
        /// Попытаться угадать
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        /// <param name="word">Слово</param>
        /// <returns>Список предположений</returns>
        IGameResult Put(Guid gameType, string word);
    }
}
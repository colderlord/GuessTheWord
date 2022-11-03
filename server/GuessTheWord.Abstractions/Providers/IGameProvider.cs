using System;
using GuessTheWord.Abstractions.Models;

namespace GuessTheWord.Abstractions.Providers
{
    /// <summary>
    /// Провайдер игр
    /// </summary>
    public interface IGameProvider : IExtension
    {
        /// <summary>
        /// Уникальный идентификатор игры
        /// </summary>
        Guid Uid { get; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Создать игру
        /// </summary>
        /// <returns></returns>
        IGame CreateGame(IRule rule, IDictionaryProvider provider, IAlphabet alphabet);
    }
}
using GuessWord.API.Model;

namespace GuessWord.API.Repository
{
    /// <summary>
    /// Базовый интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта</typeparam>
    internal interface IEntityManager<TEntity> where TEntity: IEntity
    {
        /// <summary>
        /// Загрузить объект из БД по идентификатору или null
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Объект</returns>
        TEntity? LoadOrNull(long id);

        /// <summary>
        /// Загрузить объект из БД по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Объект</returns>
        TEntity Load(long id);

        /// <summary>
        /// Сохранить объект в БД
        /// </summary>
        /// <param name="entity">Объект</param>
        void Save(TEntity historyItem);
    }
}
using System;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <summary>
    /// Базовый менеджер объектов
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal abstract class EntityManager<TEntity> : IEntityManager<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        protected EntityManager(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public virtual TEntity? LoadOrNull(long id)
        {
            return Table.Find(id);
        }

        /// <inheritdoc />
        public TEntity Load(long id)
        {
            var entity = Table.Find(id);
            if (entity == null)
            {
                throw new NullReferenceException();
            }

            return entity;
        }

        /// <inheritdoc />
        public virtual void Save(TEntity entity)
        {
            var entityEntry = dbContext.Entry(entity);
            switch (entityEntry.State)
            {
                case EntityState.Detached:
                {
                    dbContext.Add(entity);
                    break;
                }
                case EntityState.Modified:
                {
                    dbContext.Update(entity);
                    break;
                }
                default:
                {
                    throw new NotImplementedException();
                }
            }

            Save();
        }

        /// <summary>
        /// Таблица
        /// </summary>
        protected abstract DbSet<TEntity> Table { get; }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        protected void Save()
        {
            dbContext.SaveChanges();
        }

        // public void UpdateProduct(Product product)
        // {
        //     dbContext.Entry(product).State = EntityState.Modified;
        //     Save();
        // }

        // public IEnumerable<Product> GetProducts()
        // {
        //     return dbContext.Products.ToList();
        // }

        // public void DeleteProduct(int id)
        // {
        //     var tryGuessGame = dbContext.TryGuessGame.Find(id);
        //     dbContext.TryGuessGame.Remove(tryGuessGame);
        //     Save();
        // }
    }
}
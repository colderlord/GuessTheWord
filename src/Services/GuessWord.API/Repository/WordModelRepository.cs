using GuessWord.API.DBContexts;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <inheritdoc cref="GuessWord.API.Repository.IWordModelRepository" />
    internal sealed class WordModelRepository : EntityManager<WordModel>, IWordModelRepository
    {
        private readonly GuessGameContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public WordModelRepository(GuessGameContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        protected override DbSet<WordModel> Table => dbContext.WordModel;
    }
}
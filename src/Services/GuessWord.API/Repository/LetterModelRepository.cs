using GuessWord.API.DBContexts;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <inheritdoc cref="GuessWord.API.Repository.ILetterModelRepository" />
    internal sealed class LetterModelRepository : EntityManager<LetterModel>, ILetterModelRepository
    {
        private readonly GuessGameContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public LetterModelRepository(GuessGameContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        protected override DbSet<LetterModel> Table => dbContext.LetterModel;
    }
}
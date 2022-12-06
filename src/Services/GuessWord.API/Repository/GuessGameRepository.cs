using GuessWord.API.DBContext;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <inheritdoc cref="GuessWord.API.Repository.IGuessGameRepository" />
    internal sealed class GuessGameRepository : EntityManager<GuessGame>, IGuessGameRepository
    {
        private readonly GuessGameContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public GuessGameRepository(GuessGameContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        protected override DbSet<GuessGame> Table => dbContext.GuessGame;
    }
}
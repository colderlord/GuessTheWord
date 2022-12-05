using GuessWord.API.DBContexts;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <inheritdoc cref="GuessWord.API.Repository.IHistoryItemRepository" />
    internal sealed class HistoryItemRepository : EntityManager<HistoryItem>, IHistoryItemRepository
    {
        private readonly GuessGameContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public HistoryItemRepository(GuessGameContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        protected override DbSet<HistoryItem> Table => dbContext.HistoryItem;
    }
}
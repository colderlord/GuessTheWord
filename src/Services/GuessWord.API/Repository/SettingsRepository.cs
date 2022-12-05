using GuessWord.API.DBContexts;
using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.Repository
{
    /// <inheritdoc cref="GuessWord.API.Repository.ISettingsRepository" />
    internal sealed class SettingsRepository : EntityManager<Settings>, ISettingsRepository
    {
        private readonly GuessGameContext dbContext;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dbContext">Контекст БД</param>
        public SettingsRepository(GuessGameContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        protected override DbSet<Settings> Table => dbContext.Settings;
    }
}
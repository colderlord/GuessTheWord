using GuessWord.Dictionary.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.Dictionary.DBContext
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    internal sealed class DictionaryContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">Настройки</param>
        public DictionaryContext(DbContextOptions<DictionaryContext> options) : base(options)
        {
        }

        /// <summary>
        /// Хранилище моделей слов
        /// </summary>
        public DbSet<WordModel> WordModel { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
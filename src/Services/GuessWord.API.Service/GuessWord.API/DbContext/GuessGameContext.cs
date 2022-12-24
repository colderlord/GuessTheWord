using GuessWord.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.API.DBContext
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    internal sealed class GuessGameContext : DbContext
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="options">Настройки</param>
        public GuessGameContext(DbContextOptions<GuessGameContext> options) : base(options)
        {
        }

        /// <summary>
        /// Хранилище игр
        /// </summary>
        public DbSet<GuessGame> GuessGame { get; set; }

        /// <summary>
        /// Хранилище элементов истории
        /// </summary>
        public DbSet<HistoryItem> HistoryItem { get; set; }

        /// <summary>
        /// Хранилище моделей букв
        /// </summary>
        public DbSet<LetterModel> LetterModel { get; set; }

        /// <summary>
        /// Хранилище настроек
        /// </summary>
        public DbSet<Settings> Settings { get; set; }

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
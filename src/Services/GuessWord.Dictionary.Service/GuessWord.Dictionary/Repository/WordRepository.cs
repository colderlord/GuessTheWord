using GuessWord.Dictionary.DBContext;
using GuessWord.Dictionary.Model;
using Microsoft.EntityFrameworkCore;

namespace GuessWord.Dictionary.Repository;

/// <inheritdoc cref="IWordRepository" />
internal sealed class WordRepository : EntityManager<WordModel>, IWordRepository
{
    private readonly DictionaryContext dbContext;

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public WordRepository(DictionaryContext dbContext) : base(dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <inheritdoc />
    protected override DbSet<WordModel> Table => dbContext.WordModel;
}
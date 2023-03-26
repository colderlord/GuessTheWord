using GuessWord.Dictionary.Model;

namespace GuessWord.Dictionary.Repository
{
    /// <summary>
    /// Репозиторий слова
    /// </summary>
    internal interface IWordRepository : IEntityManager<WordModel>
    {
    }
}
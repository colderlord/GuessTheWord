using GuessWord.API.Model;

namespace GuessWord.API.Repository
{
    /// <summary>
    /// Репозиторий истории
    /// </summary>
    internal interface IHistoryItemRepository : IEntityManager<HistoryItem>
    {
    }
}
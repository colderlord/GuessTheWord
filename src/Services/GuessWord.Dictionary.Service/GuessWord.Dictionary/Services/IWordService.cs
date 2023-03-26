using GuessWord.Dictionary.Services.Model;

namespace GuessWord.Dictionary.Services;

public interface IWordService
{
    IEnumerable<string> GetWords(ISearchOptions options);
}
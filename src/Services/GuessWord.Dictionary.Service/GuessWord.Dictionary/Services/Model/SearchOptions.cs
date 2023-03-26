namespace GuessWord.Dictionary.Services.Model;

internal class SearchOptions : ISearchOptions
{
    public Guid Context { get; }
    public short WordLength { get; }
    public IEnumerable<ILetterOption> LetterOptions { get; }
}
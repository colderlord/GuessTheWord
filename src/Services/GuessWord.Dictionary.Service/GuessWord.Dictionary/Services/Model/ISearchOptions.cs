namespace GuessWord.Dictionary.Services.Model;

public interface ISearchOptions
{
    Guid Context { get; }

    short WordLength { get; }

    IEnumerable<ILetterOption> LetterOptions { get; }
}
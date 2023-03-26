using GuessWord.Dictionary.DBContext;
using GuessWord.Dictionary.Model;

namespace GuessWord.Dictionary.Infrastructure;

internal sealed class DictionaryContextSeed
{
    public Task SeedAsync(DictionaryContext context, IWebHostEnvironment env, ILogger<DictionaryContextSeed> logger)
    {
        return Task.Run(async () =>
        {
            var contentRootPath = env.ContentRootPath;

            if (!context.WordModel.Any())
            {
                await context.WordModel.AddRangeAsync(GetCatalogBrandsFromFile(contentRootPath, logger));

                await context.SaveChangesAsync();
            }
        });
    }

    private IEnumerable<WordModel> GetCatalogBrandsFromFile(string contentRootPath, ILogger<DictionaryContextSeed> logger)
    {
        string csvFileCatalogBrands = Path.Combine(contentRootPath, "Setup", "Words.csv");

        if (!File.Exists(csvFileCatalogBrands))
        {
            logger.LogError("File not exist");
            return new WordModel[]{};
        }

        string[] csvheaders;
        try
        {
            string[] requiredHeaders = { "words" };
            csvheaders = GetHeaders(csvFileCatalogBrands, requiredHeaders);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
            return new WordModel[]{};
        }

        return File.ReadAllLines(csvFileCatalogBrands)
            .Skip(1) // skip header row
            .Select(CreateWordModel)
            .Where(x => x != null);
    }

    private WordModel CreateWordModel(string word)
    {
        word = word.Trim('"').Trim();

        if (string.IsNullOrEmpty(word))
        {
            throw new Exception("word is empty");
        }

        return new WordModel
        {
            Word = word,
        };
    }

    private string[] GetHeaders(string csvfile, string[] requiredHeaders, string[] optionalHeaders = null)
    {
        string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

        if (csvheaders.Count() < requiredHeaders.Count())
        {
            throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is bigger then csv header count '{csvheaders.Count()}' ");
        }

        if (optionalHeaders != null)
        {
            if (csvheaders.Count() > (requiredHeaders.Count() + optionalHeaders.Count()))
            {
                throw new Exception($"csv header count '{csvheaders.Count()}'  is larger then required '{requiredHeaders.Count()}' and optional '{optionalHeaders.Count()}' headers count");
            }
        }

        foreach (var requiredHeader in requiredHeaders)
        {
            if (!csvheaders.Contains(requiredHeader))
            {
                throw new Exception($"does not contain required header '{requiredHeader}'");
            }
        }

        return csvheaders;
    }
}
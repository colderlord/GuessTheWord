using GuessWord.Dictionary.Services;
using GuessWord.Dictionary.Services.Model;
using Microsoft.AspNetCore.Mvc;

namespace GuessWord.Dictionary.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DictionaryController : Controller
{
    private readonly IWordService _wordService;

    public DictionaryController(IWordService wordService)
    {
        _wordService = wordService;
    }

    [HttpGet]
    public IActionResult GetWords()
    {
        var words = _wordService.GetWords(new SearchOptions());
        return new OkObjectResult(words);
    }
}
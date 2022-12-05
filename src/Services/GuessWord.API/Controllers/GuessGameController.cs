using EventBus.Bus;
using GuessWord.Abstractions.Events;
using GuessWord.API.Model;
using GuessWord.API.Services;
using GuessWord.API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GuessWord.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuessGameController : Controller
    {
        private readonly IGuessGameService guessGameService;
        private readonly IEventBus eventBus;

        public GuessGameController(IGuessGameService guessGameService, IEventBus eventBus)
        {
            this.guessGameService = guessGameService;
            this.eventBus = eventBus;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            //var game = guessGameRepository.LoadOrNull(id);
            return new OkObjectResult(null);
        }

        [HttpPost("CreateGame")]
        public IActionResult CreateGame([FromBody] GameSettings gameSettings)
        {
            var settings = new Settings
            {
                Attempts = gameSettings.Attempts,
                WordLength = gameSettings.LettersCount,
                Language = gameSettings.Language
            };
            var game = guessGameService.Create(settings);
            return CreatedAtAction(nameof(Get), new { id = game.Id }, game);
        }

        [HttpGet("Game")]
        public IActionResult Game(long id, string word)
        {
            guessGameService.Game(id, word);
            return new OkResult();
        }

        [HttpGet("StartGame")]
        public IActionResult StartGame(long id)
        {
            var startedDate = guessGameService.Start(id);
            return new OkObjectResult(startedDate);
        }

        [HttpGet("EndGame")]
        public IActionResult EndGame(long id)
        {
            var endedDate = guessGameService.End(id);
            return new OkObjectResult(endedDate);
        }

        [HttpGet("SendMessage")]
        public IActionResult SendMessage(string message)
        {
            eventBus.Publish(new GetWordEvent{ Message = message});
            return new OkObjectResult("Сообщение отправлено");
        }
    }
}
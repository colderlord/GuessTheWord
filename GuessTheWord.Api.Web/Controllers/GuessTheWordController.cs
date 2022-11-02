using GuessTheWord.Abstractions;
using GuessTheWord.Api.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheWord.Web.Controller
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GuessTheWordController : ControllerBase
    {
        private readonly IGameService gameService;

        public GuessTheWordController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        /// <summary>
        /// Ping сервера
        /// </summary>
        [HttpGet(Name = "Ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        [HttpPost(Name = "SetRules")]
        public IActionResult SetRules(Rule rule)
        {
            gameService.SetRules(rule.Language, rule.Letters, rule.Attempts);
            return Ok();
        }

        [HttpGet(Name = "Send")]
        public IActionResult Send(string word)
        {
            var result = gameService.Put(word);
            return Ok(result);
        }

        [HttpGet(Name = "Restart")]
        public IActionResult Restart()
        {
            gameService.Restart();
            return Ok();
        }
    }
}
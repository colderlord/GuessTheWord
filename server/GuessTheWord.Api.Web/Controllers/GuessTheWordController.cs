using System;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions;
using GuessTheWord.Abstractions.Providers;
using GuessTheWord.Api.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuessTheWord.Api.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GuessTheWordController : ControllerBase
    {
        private readonly IGameService gameService;
        private readonly IEnumerable<IGameProvider> gameProviders;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameService">Сервис игры</param>
        /// <param name="gameProviders"></param>
        public GuessTheWordController(IGameService gameService, IEnumerable<IGameProvider> gameProviders)
        {
            this.gameService = gameService;
            this.gameProviders = gameProviders;
        }

        /// <summary>
        /// Ping сервера
        /// </summary>
        [HttpGet(Name = "Ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        /// <summary>
        /// Получить доступные игры
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetAvailableGames")]
        public IActionResult GetAvailableGames()
        {
            var availableGames = gameProviders.Select(a => new {a.Name, a.Uid})
                .ToArray();
            return Ok(availableGames);
        }

        /// <summary>
        /// Установить правила
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        /// <param name="rule">Правила</param>
        /// <returns></returns>
        [HttpPost(Name = "SetRules")]
        public IActionResult SetRules(Guid gameType, Rule rule)
        {
            gameService.SetRules(gameType, rule.Language, rule.Letters, rule.Attempts);
            return Ok();
        }

        /// <summary>
        /// Отправить слово
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        /// <param name="word">Слово</param>
        /// <returns>Результат</returns>
        [HttpGet(Name = "Send")]
        public IActionResult Send(Guid gameType, string word)
        {
            var result = gameService.Put(gameType, word);
            return Ok(result);
        }

        /// <summary>
        /// Перезапуск
        /// </summary>
        /// <param name="gameType">Тип игры</param>
        /// <returns></returns>
        [HttpGet(Name = "Restart")]
        public IActionResult Restart(Guid gameType)
        {
            gameService.Restart(gameType);
            return Ok();
        }
    }
}
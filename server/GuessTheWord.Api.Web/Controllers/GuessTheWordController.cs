using System;
using System.Collections.Generic;
using System.Linq;
using GuessTheWord.Abstractions;
using GuessTheWord.Abstractions.Models;
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
        private readonly IEnumerable<IDictionaryProvider> dictionaryProviders;
        private readonly IEnumerable<IAlphabet> alphabets;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="gameService">Сервис игры</param>
        /// <param name="gameProviders">Провайдеры игр</param>
        /// <param name="dictionaryProviders">Провайдеры словарей</param>
        /// <param name="alphabets">Алфавиты</param>
        public GuessTheWordController(
            IGameService gameService,
            IEnumerable<IGameProvider> gameProviders,
            IEnumerable<IDictionaryProvider> dictionaryProviders,
            IEnumerable<IAlphabet> alphabets)
        {
            this.gameService = gameService;
            this.gameProviders = gameProviders;
            this.dictionaryProviders = dictionaryProviders;
            this.alphabets = alphabets;
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
        [HttpGet(Name = "GetAvailableLanguages")]
        public IActionResult GetAvailableLanguages()
        {
            var availableAlphabets = alphabets
                .Where(a => dictionaryProviders.FirstOrDefault(d => d.Languages.Contains(a.Culture)) != null)
                .Select(a => new {a.Name, a.Culture})
                .ToArray();
            return Ok(availableAlphabets);
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
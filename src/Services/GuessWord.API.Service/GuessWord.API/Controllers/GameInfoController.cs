using Microsoft.AspNetCore.Mvc;

namespace GuessWord.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameInfoController : Controller
    {
        private static Guid Uid = new Guid("5f67eec9-24a4-4ea7-89fe-357e711459e0");

        [HttpGet(Name = "GetInfo")]
        public IActionResult GetInfo()
        {
            return Ok(
                new
                {
                    Uid = Uid,
                    Name = "Отгадай слово"
                });
        }
    }
}


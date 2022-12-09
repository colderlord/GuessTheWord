using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebSPA.Server.Model;

namespace WebSPA.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        public HomeController(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Configuration()
        {
            return Json(_settings.Value);
        }
    }
}
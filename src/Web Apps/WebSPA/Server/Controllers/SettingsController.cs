using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebSPA.Server.Model;

namespace WebSPA.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : Controller
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;

        public SettingsController(IOptionsSnapshot<AppSettings> settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_settings.Value);
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        [HttpGet("[action]")]
        public AppConfig GetConfig()
        {
            return new AppConfig();
        }
    }
}

using FinanceTracker.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        [HttpGet("[action]")]
        [AllowAnonymous]
        public AppConfig GetConfig()
        {
            return new AppConfig();
        }
    }
}

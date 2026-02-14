using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancesController : ControllerBase
    {
        [HttpGet("[action]")]
        public List<int> GetFinances(int id)
        {
            return [1];
        }
    }
}

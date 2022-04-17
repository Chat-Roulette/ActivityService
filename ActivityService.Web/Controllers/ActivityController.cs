using ActivityService.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ActivityService.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("client/{clientId}/ping")]
        public async Task PingClient(Guid clientId)
        {
            await _activityService.PingClientAsync(clientId);
        }
    }
}

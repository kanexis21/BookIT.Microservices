using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StatusUpdaterService.Core.Application.Services;

namespace StatusUpdaterService.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusUpdateService _statusService;

        public StatusController(IStatusUpdateService statusService)
        {
            _statusService = statusService;
        }

        [HttpPost("manual-update")]
        public async Task<IActionResult> ManualUpdate()
        {
            await _statusService.UpdateStatusesAsync();
            return Ok("Статусы обновлены.");
        }
    }

}

using DUR_Application.Services.Services_Lane.LaneDto.CreateLane;
using DUR_Application.Services.Services_Lane.LaneDto.UpdateLane;
using DUR_Application.Services.Services_Lane.LaneServicesController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUR_Application.Controllers
{
    [ApiController]
    [Route("/api/lanes")]
    [Authorize]
    public class LanesController:ControllerBase
    {
        private readonly ILaneServices _laneServices;

        public LanesController(ILaneServices laneServices)
        {
            _laneServices = laneServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLanes()
        {
            var response = await _laneServices.GetAllLanes();
            return Ok(response);

        }

        [HttpGet("lane")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLaneById([FromQuery] string LaneId)
        {
            var response = await _laneServices.GetLaneById(LaneId);
            return Ok(response);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddLane([FromBody] CreateLaneDto create)
        {
            var result = await _laneServices.CreateLane(create);
            return Created(string.Empty, result);
        }

        [HttpPatch("update")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> UpdateLane([FromQuery] string LaneId, [FromBody] UpdateLaneDto LaneDto)
        {
            await _laneServices.UpdateLane(LaneDto, LaneId);
            var responseCreated = new { Good = 1 };
            return Created(string.Empty, responseCreated);
        }

    }
}

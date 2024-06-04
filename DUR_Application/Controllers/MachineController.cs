using DUR_Application.Services.Services_Machine.MachineDto.AddMachine;
using DUR_Application.Services.Services_Machine.MachineServicesController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUR_Application.Controllers
{
    [ApiController]
    [Route("/api/lanes/machines")]
    [Authorize]
    public class MachineController:ControllerBase
    {
        private readonly IMachineServices _machineServices;

        public MachineController(IMachineServices machineServices)
        {
            _machineServices = machineServices;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> AddMachine([FromQuery] string LaneNumber, [FromBody] AddMachineDto add)
        {
            var result = await _machineServices.AddMachine(add, LaneNumber);
            return Created(string.Empty, result);
        }

        [HttpGet("informations/all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMachine()
        {
            
            var response = await _machineServices.GetAllMachines();
            return Ok(response);
           
        }
        [HttpGet("informations/by-Lane")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMachinesByLane([FromQuery] string LaneNumber)
        {
            var response = await _machineServices.GetMachinesByLane(LaneNumber);

            return Ok(response);
        }

        [HttpGet("infromations/by-name")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMachineByName([FromQuery] string LaneNumber, [FromQuery] string name)
        {
            var response = await _machineServices.GetOneMachine(LaneNumber, name);

            return Ok(response);

               
        }
        [HttpPatch("update")]
        [Authorize(Roles ="Admin, Manager")]
        public async Task<IActionResult> UpdateLaneNumber([FromQuery] string LaneNumber, [FromQuery] string NewLaneNumber) 
        {
            await _machineServices.UpdateLaneMachines(LaneNumber, NewLaneNumber);

            var responseCreated = new {Good  = 1};

            return Created($"/infromations/byLane?LaneNumber={NewLaneNumber}", responseCreated);
        }

    }
}

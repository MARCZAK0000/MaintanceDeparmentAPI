using DUR_Application.Exceptions;
using DUR_Application.Services.Services_Malfunctions.CloseMalfunction;
using DUR_Application.Services.Services_Malfunctions.CreateMalfunction;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetAllMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.GetMalfunctions.GetClosedMalfunctionsQuery;
using DUR_Application.Services.Services_Malfunctions.MalfunctionsServicesController;
using DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUR_Application.Controllers
{
    [ApiController]
    [Route("api/malfunctions")]
    [Authorize]
    public class MalfunctionsController : ControllerBase
    {
        private readonly IMalfunctionsServices _malfunctionsServices;

        public MalfunctionsController(IMalfunctionsServices malfunctionsServices)
        {
            _malfunctionsServices = malfunctionsServices;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic, ProductionLeader")]
        public async Task<IActionResult> CreateMalfunctons([FromBody] CreateMalfunctionDto createMalfunction, [FromQuery] string LaneNumber, [FromQuery] int MachineId)
        {
            await _malfunctionsServices.CreateMalfunctions(createMalfunction, LaneNumber, MachineId);

            var responseCreated = new { Good = 1 };

            return Created(string.Empty, responseCreated);
        }
        [HttpPost("update")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic")]
        public async Task<IActionResult> UpdateMalfunction([FromBody] UpdateMalfunctionsDto update)
        {
            await _malfunctionsServices.UpdateMalfunctions(update);

            var responseUpdated = new { Good = 1 };

            return Ok(responseUpdated);
        }

        [HttpPost("Close")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic")]
        public async Task<IActionResult> CloseMalfunction([FromBody] CloseMalfunctioDto close)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Model state is valid");
            }
            await _malfunctionsServices.CloseMalfucntion(close);

            var responseClosed = new { Close = 1 };

            return Ok(responseClosed);
        }

        [HttpGet("informations/all/by-date")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic, ProductionLeader")]
        public async Task<IActionResult> GetAllMalfunctions ([FromQuery] GetAllMalfunctionsQuery query) 
        {
            var result = await _malfunctionsServices.GetAllMalfunctions(query);

            return Ok(result);
        }

        [HttpGet("informations/by-id")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic, ProductionLeader")]
        public async Task<IActionResult> GetMalfunction([FromQuery] int MalfuncionsId)
        {
            var result = await _malfunctionsServices.GetOneMalfunctions(MalfuncionsId);

            return Ok(result);  
        }
        
        [HttpGet("informations/closed/by-id")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic, ProductionLeader")]
        public async Task<IActionResult> GetMalfunctionsByDate([FromQuery] GetClosedMalfunctionsQueryDto queryDto)
        {
            var result = await _malfunctionsServices.GetClosedMalfunctionsByDate(queryDto);
            return Ok(result);  
        }

        
    }
}

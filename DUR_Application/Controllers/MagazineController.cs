using DUR_Application.Services.Services_Magazine.AddSpareParts;
using DUR_Application.Services.Services_Magazine.MagazineServicesController;
using DUR_Application.Services.Services_Magazine.ShowSpareParts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DUR_Application.Controllers
{
    [ApiController]
    [Route("/api/magazine")]
    [Authorize]
    public class MagazineController:ControllerBase
    {
        private readonly IMagazineServices _magazineServices;     
        public MagazineController(IMagazineServices magazineServices)
        {
            _magazineServices = magazineServices;
        }

        [HttpPost("add/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAllParts([FromQuery] int MagazineId)
        {
            await _magazineServices.AddSparePartsToDb(MagazineId);

            var CreatedResponse = new { Created = 1 };

            return Created(string.Empty, CreatedResponse);

        }
        [HttpPost("update/part-number")]
        [Authorize(Roles = "Admin, Manager, Lider")]
        public async Task<IActionResult> UpdatePartNumber()
        {
            await _magazineServices.UpdatePartNumber();

            return Ok(string.Empty);
        }

        [HttpPost("add/part")]
        [Authorize(Roles = "Admin, Manager, Lider")]
        public async Task<IActionResult> AddPart([FromBody] AddSparePartsDto add, [FromQuery] int MagazineId) 
        {
            await _magazineServices.AddSparePart(add, MagazineId);

            var createdResponse = new { Created = 1 };

            return Created(string.Empty, createdResponse);
        }
        [HttpGet("show")]
        [Authorize(Roles = "Admin, Manager, Lider, Mechanic")]
        public async Task<IActionResult> ShowSparePart([FromQuery] SearchPartQuery query)
        {
            var result = await _magazineServices.GetSpareParts(query);

            return Ok(result);  
        }
    }
}

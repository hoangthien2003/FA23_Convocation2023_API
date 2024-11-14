using FA23_Convocation2023_API.DTO;
using FA23_Convocation2023_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly HallService _hallService;

        public HallController(HallService hallService)
        {
            _hallService = hallService;
        }

        //Create a new hall
        [HttpPost("CreateHall")]
        public async Task<IActionResult> CreateHallAsync([FromBody]CreateHallRequest hallRequest)
        {
            var hallExist = await _hallService.HallExist(hallRequest.HallName);
            if (hallExist)
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Hall already exists!"
                });
            }
            
            var result = await _hallService.CreateHall(hallRequest.HallName);
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Create hall successfully!",
                data = result
            });
        }
    }
}

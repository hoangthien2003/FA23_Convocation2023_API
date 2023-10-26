using FA23_Convocation2023_API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();

        [HttpGet("Test")]
        public IActionResult Test()
        {
            var result = _context.Bachelors.FirstOrDefault();
            if (result == null)
            {
                return Ok("Has error in get data process!");
            }
            return Ok(result);
        }
    }
}

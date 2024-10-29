using FA23_Convocation2023_API.Entities;
using FA23_Convocation2023_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();

        //Create a new session
        [HttpPost("CreateSession")]
        public async Task<IActionResult> CreateSessionAsync([FromBody]CreateSessionRequest sessionRequest)
        { 
            var checkInExist = _context.CheckIns.FirstOrDefault(c => c.HallName == sessionRequest.HallName && c.SessionNum == sessionRequest.SessionNum);
            if (checkInExist != null) {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Session already exists!"
                });
            }
            CheckIn checkIn = new CheckIn
            {
                HallName = sessionRequest.HallName,
                SessionNum = sessionRequest.SessionNum,
                Status = false
            };
            await _context.CheckIns.AddAsync(checkIn);
            var sessonNumExist = _context.Sessions.FirstOrDefault(s => s.Session1 == sessionRequest.SessionNum);
            if (sessonNumExist == null) {
                Session session = new Session
                {
                    Session1 = sessionRequest.SessionNum,
                };
                await _context.Sessions.AddAsync(session);
            }
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Create session check in successfully!",
                data = checkIn
            });
        }

    }
}

using FA23_Convocation2023_API.Entities;
using FA23_Convocation2023_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN, CK")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();

        [HttpPut("UpdateCheckin")]
        public async Task<IActionResult> UpdateCheckinAsync(CheckinRequest checkinRequest)
        {
            var bachelor = await _context.Bachelors.FirstOrDefaultAsync(b => b.StudentCode == checkinRequest.StudentCode);
            if (checkinRequest.CheckIn == "1")
            {
                bachelor.TimeCheckIn1 = DateTime.Now;
                bachelor.CheckIn1 = true;
            }
            if (checkinRequest.CheckIn == "2")
            {
                bachelor.TimeCheckIn2 = DateTime.Now;
                bachelor.CheckIn2 = true;
            }
            _context.Bachelors.Update(bachelor);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Update checkin successfully!",
                data = bachelor
            });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCheckinAsync()
        {
            var result = await _context.Bachelors.ToListAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get checkin successfully!",
                data = result
            });
        }
    }
}

using FA23_Convocation2023_API.Models;
using FA23_Convocation2023_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN")]
    [ApiController]
    public class BachelorController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBachelorAsync()
        {
            var result = await _context.Bachelors.Select(b => new
            {
                b.StudentCode,
                b.FullName,
                b.Mail,
                b.Faculty,
                b.Major,
                b.Image,
                b.Status,
                b.StatusBaChelor,
                b.HallName,
                b.SessionNum
            }).ToListAsync();
            return Ok(result);
        }

        [HttpPost("AddBachelor")]
        public async Task<IActionResult> AddBechelorAsync([FromBody] List<BachelorDTO> bachelorRequest)
        {
            bool isExist = false;
            foreach(var bItem in  bachelorRequest)
            {
                var existingBachelor = await _context.Bachelors.
                FirstOrDefaultAsync(b => b.StudentCode == bItem.StudentCode);
                if (existingBachelor != null)
                {
                    isExist = true;
                    break;
                }
                var bachelor = new Bachelor
                {
                    Id = bItem.Id,
                    Image = bItem.Image,
                    FullName = bItem.FullName,
                    StudentCode = bItem.StudentCode,
                    Mail = bItem.Mail,
                };
                await _context.Bachelors.AddAsync(bachelor);
            }
            if (isExist) return BadRequest("Existed bachelor!");
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Add bachelors successfully!",
                data = bachelorRequest
            });
        }

        [HttpPut("UpdateBachelor")]
        public async Task<IActionResult> UpdateBachelorAsync(List<BachelorDTO> bachelorRequest)
        {
            foreach (var entity in _context.Bachelors)
            {
                _context.Bachelors.Remove(entity);
            }
            await _context.SaveChangesAsync();
            foreach (var entity in bachelorRequest)
            {
                var bachelor = new Bachelor
                {
                    Id = entity.Id,
                    Image = entity.Image,
                    FullName = entity.FullName,
                    StudentCode = entity.StudentCode,
                    Mail = entity.Mail,
                };
                await _context.Bachelors.AddAsync(bachelor);
            }
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Update bachelors successfully!",
                data = bachelorRequest
            });
        }

        [HttpDelete("DeleteBachelor/{StudentCode}")]
        public async Task<IActionResult> DeleteBachelorAsync([FromRoute] string StudentCode)
        {
            var existBachelor = _context.Bachelors.FirstOrDefault(b => b.StudentCode == StudentCode);
            if (existBachelor == null)
            {
                return BadRequest("Not any bachelor!");
            }
            _context.Bachelors.Remove(existBachelor);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Delete bachelors successfully!",
                data = existBachelor
            });
        }
    }
}

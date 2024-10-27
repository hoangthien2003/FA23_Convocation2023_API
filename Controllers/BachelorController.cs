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
                b.Id,
                b.StudentCode,
                b.FullName,
                b.Mail,
                b.Major,
                b.Image,
                b.Status,
                b.StatusBaChelor,
                b.CheckIn,
                b.HallName,
                b.SessionNum,
                b.Chair,
                b.ChairParent
            }).ToListAsync();
            if (result.Count == 0) return Ok(new
            {
                status = StatusCodes.Status204NoContent,
                message = "Not any bachelors!"
            });
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get all bachelors successfully!",
                data = result
            });
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddBechelorAsync([FromBody] List<BachelorDTO> bachelorRequest)
        {
            List<string> errorList = new List<string>();

            var studentCodes = bachelorRequest.Select(b => b.StudentCode).ToList();
            var existingStudents = await _context.Bachelors
                .Where(b => studentCodes.Contains(b.StudentCode))
                .Select(b => b.StudentCode)
                .ToListAsync();

            foreach (var bItem in bachelorRequest)
            {
                if (existingStudents.Contains(bItem.StudentCode))
                {
                    errorList.Add($"Bachelor {bItem.StudentCode} is existed!");
                    continue;
                }

                var bachelor = new Bachelor
                {
                    Image = bItem.Image,
                    FullName = bItem.FullName,
                    StudentCode = bItem.StudentCode,
                    Major = bItem.Major,
                    Mail = bItem.Mail,
                    HallName = bItem.HallName,
                    SessionNum = bItem.SessionNum,
                    Status = false,
                    CheckIn = false,
                    Chair = bItem.Chair,
                    ChairParent = bItem.ChairParent,
                };

                await _context.Bachelors.AddAsync(bachelor);
            }

            await _context.SaveChangesAsync();

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Add bachelors successfully!",
                errorMessages = errorList,
                data = bachelorRequest
            });
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateBachelorAsync(BachelorDTO bachelorRequest)
        {
            var existingBachelor = await _context.Bachelors.FirstOrDefaultAsync(b => b.StudentCode == bachelorRequest.StudentCode);

            existingBachelor.Image = bachelorRequest.Image;
            existingBachelor.FullName = bachelorRequest.FullName;
            existingBachelor.StudentCode = bachelorRequest.StudentCode;
            existingBachelor.Mail = bachelorRequest.Mail;
            existingBachelor.Major = bachelorRequest.Major;
            existingBachelor.HallName = bachelorRequest.HallName;
            existingBachelor.SessionNum = bachelorRequest.SessionNum;
            existingBachelor.Chair = bachelorRequest.Chair;
            existingBachelor.ChairParent = bachelorRequest.ChairParent;

            _context.Bachelors.Update(existingBachelor);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Update bachelors successfully!",
                data = existingBachelor
            });
        }

        [HttpDelete("Delete/{StudentCode}")]
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
                message = "Delete successfully!",
            });
        }

        [HttpDelete("DeleteAll")]
        public async Task<IActionResult> DeleteAllBachelorAsync()
        {
            foreach(var bachelor in _context.Bachelors)
            {
                _context.Bachelors.Remove(bachelor);
            }
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Delete all bachelor successfully!"
            });
        }

        [HttpPut("ResetStatus")]
        public async Task<IActionResult> ResetStatusAsync()
        {
            foreach(var bachelor in _context.Bachelors)
            {
                bachelor.StatusBaChelor = null;
                bachelor.Status = false;
                bachelor.CheckIn = false;
                _context.Bachelors.Update(bachelor);
            }
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Reset all status checkin successfully!"
            });
        }

        //get bachelor by hall name and session number
        [HttpGet("GetByHallSession/{hallName}/{sessionNum}")]
        public async Task<IActionResult> GetBachelorByHallSessionAsync([FromRoute] string hallName, [FromRoute] int sessionNum)
        {
            var result = await _context.Bachelors
                .Where(b => b.HallName == hallName && b.SessionNum == sessionNum)
                .Select(b => new
                {
                    b.Id,
                    b.StudentCode,
                    b.FullName,
                    b.Mail,
                    b.Major,
                    b.Image,
                    b.Status,
                    b.StatusBaChelor,
                    b.CheckIn,
                    b.HallName,
                    b.SessionNum,
                    b.Chair,
                    b.ChairParent
                }).ToListAsync();
            if (result.Count == 0) return Ok(new
            {
                status = StatusCodes.Status204NoContent,
                message = "Not any bachelors!"
            });
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get all bachelors successfully!",
                data = result
            });
        }
    }
}

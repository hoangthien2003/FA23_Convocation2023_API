﻿using FA23_Convocation2023_API.Entities;
using FA23_Convocation2023_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();

        [HttpPut("UpdateCheckin")]
        [Authorize(Roles = "MN, CK")]
        public async Task<IActionResult> UpdateCheckinAsync(CheckinRequest checkinRequest)
        {

            var bachelor = await _context.Bachelors.FirstOrDefaultAsync(b => b.StudentCode == checkinRequest.StudentCode);
            if (bachelor != null && bachelor.StatusBaChelor == "Current")
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Bachelor is being displayed in led, cannot be updated at this time",
                    data = ""
                });
            }
            var statusCheckin = await _context.CheckIns.FirstOrDefaultAsync(c => c.HallName == bachelor.HallName && c.SessionNum == bachelor.SessionNum);
            if (statusCheckin.Status == true)
            {
                bachelor.TimeCheckIn = DateTime.Now;
                bachelor.CheckIn = checkinRequest.Status;
                if (bachelor.CheckIn == true)
                {
                    bachelor.Status = true;
                }
                else
                {
                    bachelor.Status = false;
                }
                _context.Bachelors.Update(bachelor);
                await _context.SaveChangesAsync();
            } else
            {
                return BadRequest(new
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Status is false, cannot checkin"
                });
            }

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Update checkin successfully!",
                data = bachelor
            });
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "MN, CK")]
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

        [HttpPut("UncheckAll")]
        [Authorize(Roles = "MN, CK")]
        public async Task<IActionResult> UncheckAllCheckinAsync()
        {
            foreach (var bachelor in _context.Bachelors)
            {
                bachelor.TimeCheckIn = null;
                bachelor.CheckIn = false;
                bachelor.Status = false;
                _context.Bachelors.Update(bachelor);
            }
            await _context.SaveChangesAsync();
            var bachelorResponse = await _context.Bachelors.Select(bItem => new
            {
                bItem.StudentCode,
                bItem.TimeCheckIn,
                bItem.CheckIn
            }).ToListAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Uncheck all checkin successfully!",

            });
        }

        [HttpGet("GetAllStatusCheckin")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> GetAllStatusCheckinAsync()
        {
            var statusCheckin = await _context.CheckIns.ToListAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get all status checkin successfully",
                data = statusCheckin
            });
        }

        [HttpPut("UpdateStatusCheckin")]
        [Authorize(Roles = "MN")]
        public async Task<IActionResult> UpdateStatusCheckinAsync(StatusCheckinRequest request)
        {
            var statusCheckin = await _context.CheckIns.FirstOrDefaultAsync(
                c => c.HallName == request.HallName && c.SessionNum == request.SessionNum);
            statusCheckin.Status = request.Status;
            _context.CheckIns.Update(statusCheckin);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Update status checkin successfully!",
                data = statusCheckin
            });
        }
    }
}

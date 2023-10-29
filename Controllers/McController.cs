using FA23_Convocation2023_API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;
using static System.Collections.Specialized.BitVector32;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN")]
    [ApiController]
    public class McController : ControllerBase
    {
        private readonly Convocation2023Context _context = new Convocation2023Context();
        [HttpGet("GetBachelor1st")]
        public async Task<IActionResult> Get1stBachelorToShow([FromQuery] string hall, [FromQuery] int session)
        {
            var user1 = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Status == true && b1.HallName.Equals(hall) && b1.SessionNum == session);
            if (user1 == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Not Found",
                    data = ""
                });
            }
            int userIdToSearch = (user1.Id) + 1;
            Bachelor user2 = null;
            while (user2 == null)
            {
                user2 = await _context.Bachelors.FirstOrDefaultAsync(u => (u.Id == userIdToSearch) && (u.Status == true) && u.HallName.Equals(hall) && u.SessionNum == session);
                userIdToSearch++;
            }

            if (user1 == null || user2 == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Not Found",
                    data = ""
                });
            }
            user1.StatusBaChelor = "Current";
            user2.StatusBaChelor = "Next";
            var result = new
            {
                User1 = user1,
                User2 = user2
            };

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get all bachelors successfully!",
                data = result
            });

        }

        [HttpGet("GetBachelorNext")]
        public async Task<IActionResult> GetBaChelorNext([FromQuery] int idCurrent, [FromQuery] int idNext, [FromQuery] string hall, [FromQuery] int session)
        {
            var bachelorLast = await _context.Bachelors.Where(b => b.Status == true && b.HallName.Equals(hall) && b.SessionNum == session).OrderBy(b => b.Id).LastOrDefaultAsync();
            var bachelor1 = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Id == idCurrent && b1.HallName.Equals(hall) && b1.SessionNum == session);
            var bachelor2 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.Id == idNext && b2.HallName.Equals(hall) && b2.SessionNum == session);

            int numBachelor3 = idNext + 1;
            Bachelor bachelor3 = null;
            if (bachelorLast != null)
            {
                if (bachelorLast.Id == idNext)
                {
                    bachelor1.StatusBaChelor = "Back";
                    bachelor2.StatusBaChelor = "Current";
                    await _context.SaveChangesAsync();
                    var result0 = new
                    {
                        Bachelor1 = bachelor1,
                        Bachelor2 = bachelor2,
                        Bachelor3 = "",

                    };
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Last bachelor",
                        data = result0
                    });
                }
            }
            while (bachelor3 == null)
            {
                bachelor3 = await _context.Bachelors.FirstOrDefaultAsync(u => (u.Id == numBachelor3) && (u.Status == true) && u.HallName.Equals(hall) && u.SessionNum == session);

                numBachelor3++;
            }
            if (bachelor1 == null || bachelor2 == null || bachelor3 == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Not Found",
                    data = ""
                });
            }
            bachelor1.StatusBaChelor = "Back";
            bachelor2.StatusBaChelor = "Current";
            bachelor3.StatusBaChelor = "Next";
            await _context.SaveChangesAsync();

            var result = new
            {
                Bachelor1 = bachelor1,
                Bachelor2 = bachelor2,
                Bachelor3 = bachelor3,

            };
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get 3 bachelors here!",
                data = result
            });
        }
        [HttpGet("GetBachelorBack")]
        public async Task<IActionResult> GetBaChelorBack([FromQuery] int idBack, [FromQuery] int idCurrent, [FromQuery] string hall, [FromQuery] int session)
        {
            var bachelorFirst = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Status == true && b1.HallName.Equals(hall) && b1.SessionNum == session);

            var bachelor1 = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Id == idBack && b1.HallName.Equals(hall) && b1.SessionNum == session);
            var bachelor2 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.Id == idCurrent && b2.HallName.Equals(hall) && b2.SessionNum == session);
            Bachelor bachelor0 = null;
            if (bachelorFirst.Id == idBack)
            {
                bachelor1.StatusBaChelor = "Current";
                bachelor2.StatusBaChelor = "Next";
                await _context.SaveChangesAsync();
                var result0 = new
                {
                    Bachelor0 = "",
                    Bachelor1 = bachelor1,
                    Bachelor2 = bachelor2,

                };
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "First bachelor",
                    data = result0
                });
            }
            int numBachelor0 = idBack - 1;
            while (bachelor0 == null)
            {
                bachelor0 = await _context.Bachelors.FirstOrDefaultAsync(u => (u.Id == numBachelor0) && (u.Status == true) && u.HallName.Equals(hall) && u.SessionNum == session);

                numBachelor0--;
            }
            if (bachelor1 == null || bachelor2 == null || bachelor0 == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Not Found",
                    data = ""
                });
            }
            bachelor0.StatusBaChelor = "Back";
            bachelor1.StatusBaChelor = "Current";
            bachelor2.StatusBaChelor = "Next";
            await _context.SaveChangesAsync();

            var result = new
            {
                Bachelor0 = bachelor0,
                Bachelor1 = bachelor1,
                Bachelor2 = bachelor2,

            };
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get 3 bachelors here!",
                data = result
            });
        }
        [HttpGet("GetBachelorCurrent")]
        public async Task<IActionResult> GetBaChelorCurrrent([FromQuery] string hall, [FromQuery] int session)
        {
            var bachelorFirst = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Status == true && b1.HallName.Equals(hall) && b1.SessionNum == session);
            var bachelorLast = await _context.Bachelors.Where(b => b.Status == true && b.HallName.Equals(hall) && b.SessionNum == session).OrderBy(b => b.Id).LastOrDefaultAsync();
            var bachelorCurrent = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Status == true && b1.HallName.Equals(hall) && b1.SessionNum == session && b1.StatusBaChelor == "Current");

            if (bachelorCurrent != null)
            {
                int numBachelorBack = bachelorCurrent.Id - 1;
                int numBachelorNext = bachelorCurrent.Id + 1;
                if (bachelorCurrent.Id != bachelorLast.Id && bachelorCurrent.Id != bachelorFirst.Id)
                {
                    Bachelor bachelorBack = null;
                    Bachelor bachelorNext = null;
                    while (bachelorBack == null)
                    {
                        bachelorBack = await _context.Bachelors.FirstOrDefaultAsync(bb => (bb.Id == numBachelorBack) && (bb.Status == true) && bb.HallName.Equals(hall) && bb.SessionNum == session);
                        numBachelorBack--;
                    }
                    while (bachelorNext == null)
                    {
                        bachelorNext = await _context.Bachelors.FirstOrDefaultAsync(bn => (bn.Id == numBachelorNext) && (bn.Status == true) && bn.HallName.Equals(hall) && bn.SessionNum == session);
                        numBachelorNext++;
                    }
                    bachelorBack.StatusBaChelor = "Back";
                    bachelorCurrent.StatusBaChelor = "Current";
                    bachelorNext.StatusBaChelor = "Next";
                    await _context.SaveChangesAsync();
                    var result = new
                    {
                        Bachelor0 = bachelorBack,
                        Bachelor1 = bachelorCurrent,
                        Bachelor2 = bachelorNext,

                    };
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Get 3 bachelors here!",
                        data = result
                    });
                }
                if(bachelorCurrent.Id == bachelorLast.Id && bachelorCurrent.Id != bachelorFirst.Id)
                {
                    Bachelor bachelorBack = null;
                    while (bachelorBack == null)
                    {
                        bachelorBack = await _context.Bachelors.FirstOrDefaultAsync(bb => (bb.Id == numBachelorBack) && (bb.Status == true) && bb.HallName.Equals(hall) && bb.SessionNum == session);
                        numBachelorBack--;
                    }
                    bachelorBack.StatusBaChelor = "Back";
                    bachelorCurrent.StatusBaChelor = "Current";
                    await _context.SaveChangesAsync();
                    var result0 = new
                    {
                        Bachelor1 = bachelorBack,
                        Bachelor2 = bachelorCurrent,
                        Bachelor3 = "",

                    };
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Last bachelor",
                        data = result0
                    });
                }
                if (bachelorCurrent.Id != bachelorLast.Id && bachelorCurrent.Id == bachelorFirst.Id)
                {
                    Bachelor bachelorNext = null;
                    while (bachelorNext == null)
                    {
                        bachelorNext = await _context.Bachelors.FirstOrDefaultAsync(bn => (bn.Id == numBachelorNext) && (bn.Status == true) && bn.HallName.Equals(hall) && bn.SessionNum == session);
                        numBachelorNext++;
                    }
                    bachelorCurrent.StatusBaChelor = "Current";
                    bachelorNext.StatusBaChelor = "Next";
                    await _context.SaveChangesAsync();
                    var result0 = new
                    {
                        Bachelor1 = "",
                        Bachelor2 = bachelorCurrent,
                        Bachelor3 = bachelorNext,

                    };
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "Last bachelor",
                        data = result0
                    });
                }

            }
            return NotFound(new
            {
                status = StatusCodes.Status404NotFound,
                message = "Not Found",
                data = ""
            });
        }
    }
}

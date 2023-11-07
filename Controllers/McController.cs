using FA23_Convocation2023_API.Entities;
using FA23_Convocation2023_API.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.Intrinsics.X86;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace FA23_Convocation2023_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "MN, US, CK")]
    [ApiController]
    public class McController : ControllerBase
    {
        private IHubContext<MessageHub> messageHub;
        public McController(IHubContext<MessageHub> _messageHub)
        {
            messageHub = _messageHub;
        }
        private readonly Convocation2023Context _context = new Convocation2023Context();
        [HttpGet("GetLocationBachelor")]
        public async Task<IActionResult> GetLocationBachelor([FromQuery] string studentCode)
        {
            var bachelor = await _context.Bachelors.FirstOrDefaultAsync(b => b.StudentCode == studentCode);
            if (bachelor == null)
            {
                return NotFound(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "Not Found",
                    data = ""
                });
            }
            if (bachelor != null && bachelor.CheckIn1 == false && bachelor.CheckIn2 ==true)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Bachelor does not Checkin1. Please checkin",
                    data = ""
                });
            }
            if (bachelor != null && bachelor.CheckIn2 == false && bachelor.CheckIn1 == true)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Bachelor does not Checkin2. Please checkin",
                    data = ""
                });
            }

            if (bachelor != null && bachelor.Status == false)
            {
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "Bachelor does not Checkin. Please checkin",
                    data = ""
                });
            }

            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Location of bachelor",
                data = bachelor
            });
        }
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
            await _context.SaveChangesAsync();
            var result = new
            {
                User1 = user1,
                User2 = user2
            };
            await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + user1.ToString(), user1.ToString());
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get all bachelors successfully!",
                data = result
            });

        }

        [HttpGet("GetBachelorNext")]
        public async Task<IActionResult> GetBaChelorNext([FromQuery] string hall, [FromQuery] int session)
        {
            var bachelorLast = await _context.Bachelors.Where(b => b.Status == true && b.HallName.Equals(hall) && b.SessionNum == session).OrderBy(b => b.Id).LastOrDefaultAsync();
            var bachelor1 = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.StatusBaChelor == "Current" && b1.HallName.Equals(hall) && b1.SessionNum == session && b1.Status == true);
            //var bachelor2 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.Id == idNext && b2.HallName.Equals(hall) && b2.SessionNum == session);
            if (bachelor1.Id == bachelorLast.Id)
            {
                return Ok(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "IN THE LAST BACHELOR, CAN NOT NEXT",
                    data = ""
                });
            }
            Bachelor bachelor2 = null;
            int numBachelor2 = bachelor1.Id + 1;
            while(bachelor2 == null)
            {
                bachelor2 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.Id == numBachelor2 && b2.HallName.Equals(hall) && b2.SessionNum == session && b2.Status == true);
                numBachelor2++;
            }
            int numBachelor3 = bachelor2.Id + 1;
            Bachelor bachelor3 = null;
            if (bachelorLast != null)
            {
                if (bachelorLast.Id == bachelor2.Id)
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
                    await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelor2.ToString(), bachelor2.ToString());
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "CURRENT IS IN THE LAST OF INDEX",
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
            await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelor2.ToString(), bachelor2.ToString());
            return Ok(new
            {
                status = StatusCodes.Status200OK,
                message = "Get 3 bachelors here!",
                data = result
            });
        }
        [HttpGet("GetBachelorBack")]
        public async Task<IActionResult> GetBaChelorBack([FromQuery] string hall, [FromQuery] int session)
        {
            var bachelorFirst = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Status == true && b1.HallName.Equals(hall) && b1.SessionNum == session);

            //var bachelor1 = await _context.Bachelors.FirstOrDefaultAsync(b1 => b1.Id == idBack && b1.HallName.Equals(hall) && b1.SessionNum == session);
            
            var bachelor2 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.StatusBaChelor == "Current" && b2.HallName.Equals(hall) && b2.SessionNum == session && b2.Status == true);
            if(bachelor2.Id == bachelorFirst.Id)
            {
                return Ok(new
                {
                    status = StatusCodes.Status404NotFound,
                    message = "IN THE FIRST BACHELOR, CAN NOT BACK",
                    data = ""
                });
            }    
            Bachelor bachelor1 = null;
                int numBachelor1 = bachelor2.Id - 1;
                while (bachelor1 == null && bachelorFirst.Id != bachelor2.Id)
                {
                    bachelor1 = await _context.Bachelors.FirstOrDefaultAsync(b2 => b2.Id == numBachelor1 && b2.HallName.Equals(hall) && b2.SessionNum == session && b2.Status == true);
                    numBachelor1--;
                }
            
            
            Bachelor bachelor0 = null;
            if (bachelorFirst.Id == bachelor1.Id)
            {
                bachelor1.StatusBaChelor = "Current";
                bachelor2.StatusBaChelor = "Next";
                await _context.SaveChangesAsync();
                var result0 = new
                {
                    Bachelor1 = "",
                    Bachelor2 = bachelor1,
                    Bachelor3 = bachelor2,

                };
                await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelor1.ToString(), bachelor1.ToString());
                return Ok(new
                {
                    status = StatusCodes.Status200OK,
                    message = "CURRENT IS IN THE FIRST OF INDEX",
                    data = result0
                });
            }
            int numBachelor0 = bachelor1.Id - 1;
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
                Bachelor1 = bachelor0,
                Bachelor2 = bachelor1,
                Bachelor3 = bachelor2,

            };
            await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelor1.ToString(), bachelor1.ToString());
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
                        Bachelor1 = bachelorBack,
                        Bachelor2 = bachelorCurrent,
                        Bachelor3 = bachelorNext,

                    };
                    await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelorCurrent.ToString(), bachelorCurrent.ToString());
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
                    await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelorCurrent.ToString(), bachelorCurrent.ToString());
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "CURRENT IS IN THE LAST OF INDEX",
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
                    await messageHub.Clients.All.SendAsync("SendMessage", "CurrentBachelor " + bachelorCurrent.ToString(), bachelorCurrent.ToString());
                    return Ok(new
                    {
                        status = StatusCodes.Status200OK,
                        message = "CURRENT IS IN THE FIRST OF INDEX",
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

using FA23_Convocation2023_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FA23_Convocation2023_API.Services
{
    public class HallService
    {
        private readonly Convo24Context _context = new Convo24Context();

        //check if hall is existed
        public Task<bool> HallExist(string HallName)
        {
            return _context.Halls.AnyAsync(h => h.HallName == HallName);
        }

        //create new hall
        public async Task<Hall> CreateHall(string HallName) 
        {
            var hall = new Hall
            {
                HallName = HallName
            };
            await _context.Halls.AddAsync(hall);
            await _context.SaveChangesAsync();
            return hall;
        }
    }

}

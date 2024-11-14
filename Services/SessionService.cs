using FA23_Convocation2023_API.DTO;
using FA23_Convocation2023_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FA23_Convocation2023_API.Services
{
    public class SessionService
    {
        private readonly Convo24Context _context = new Convo24Context();

        //check if session is existed
        public Task<bool> SessionExist(int sessionNum)
        {
            return _context.Sessions.AnyAsync(s => s.Session1 == sessionNum);
        }

        //create session
        public async Task<Session> CreateSession(int sessionNum)
        {
            var session = new Session
            {
                Session1 = sessionNum
            };
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
            return session;
        }

        //get all session
        public async Task<List<ListSession>> GetAllSessionAsync()
        {
            var sessions = await _context.Sessions.ToListAsync();
            var listSession = new List<ListSession>();
            foreach (var session in sessions)
            {
                listSession.Add(new ListSession
                {
                    SessionId = session.SessionId,
                    Session1 = session.Session1
                });
            }
            return listSession;
        }
    }
}

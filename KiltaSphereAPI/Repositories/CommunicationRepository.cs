using KiltaSphereAPI.Data;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KiltaSphereAPI.Repositories
{
    public class CommunicationRepository : ICommunicationRepository
    {
        private readonly ApplicationDbContext _context;

        public CommunicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommunicationLog>> GetLogsByMemberIdAsync(int memberId)
        {
            return await _context.CommunicationLogs
                .Where(l => l.MemberId == memberId)
                .OrderByDescending(l => l.LogDate)
                .ToListAsync();
        }

        public async Task AddLogAsync(CommunicationLog log)
        {
            await _context.CommunicationLogs.AddAsync(log);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
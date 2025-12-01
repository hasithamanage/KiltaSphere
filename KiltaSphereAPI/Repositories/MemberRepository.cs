using Microsoft.EntityFrameworkCore;
using KiltaSphereAPI.Data;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly ApplicationDbContext _context;

        // Dependency Injection: The DbContext is injected into the repository
        public MemberRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            // EF Core query to fetch all members asynchronously
            return await _context.Members.ToListAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            // EF Core query to find a member by its primary key
            return await _context.Members.FindAsync(id);
        }

        public async Task<bool> AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            // Stage the member for saving; actual database save happens in the service layer.
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            // Returns true if one or more rows were affected
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
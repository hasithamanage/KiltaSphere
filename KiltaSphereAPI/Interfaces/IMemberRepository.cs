using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface IMemberRepository
    {
        // Contract for retrieving all members
        Task<IEnumerable<Member>> GetAllMembersAsync();

        // Contract for retrieving a single member by ID
        Task<Member?> GetMemberByIdAsync(int id);

        // Contract for creating a new member
        Task<bool> AddMemberAsync(Member member);

        // Contract for saving changes to the database 
        Task<bool> SaveChangesAsync();
    }
}

using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface IMemberRepository
    {
        // Contract for retrieving all members
        Task<IEnumerable<Member>> GetAllMembersAsync();

        // Contract for retrieving a single member by ID
        Task<Member?> GetMemberByIdAsync(int id);

        // Contract for creating a new member (returns a flag, save changes is separate)
        Task<bool> AddMemberAsync(Member member);

        // Contract for delete a member 
        Task<bool> DeleteMemberAsync(int id);

        // Contract for saving changes to the database 
        Task<bool> SaveChangesAsync();
    }
}
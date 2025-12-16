using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface IMemberRepository
    {
        // Contract for retrieving all members
        Task<IEnumerable<Member>> GetAllMembersAsync();

        // Contract for retrieving a single member by ID
        Task<Member?> GetMemberByIdAsync(int id);

        // Contract for creating a new member (Stages the member for saving)
        Task<bool> AddMemberAsync(Member member);

        // Contract for delete a member 
        Task<bool> DeleteMemberAsync(int id);

        // Utility: Saves all pending changes to the database
        Task<bool> SaveChangesAsync();
    }
}
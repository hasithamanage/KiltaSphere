using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    // The Service Interface defines all business operations
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMemberListAsync();

        // Get by ID
        Task<Member?> GetMemberByIdAsync(int id);

        // Create (returns the created Member or null if failed)
        Task<Member?> CreateNewMemberAsync(Member member);

        // Delete
        Task<bool> DeleteMemberAsync(int id);

        // Will handle Update (PUT) later
    }
}
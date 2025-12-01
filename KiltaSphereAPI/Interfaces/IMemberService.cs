using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    // The Service Interface defines all business operations
    public interface IMemberService
    {
        // This method may include complex filtering, validation, or orchestration logic later
        Task<IEnumerable<Member>> GetMemberListAsync();

        // This could validate the email format, check for existing members, etc.
        Task<bool> CreateNewMemberAsync(Member member);
    }
}
using KiltaSphereAPI.Models;
using KiltaSphereAPI.DTOs;  

namespace KiltaSphereAPI.Interfaces
{
    // The Service Interface defines all business operations
    public interface IMemberService
    {
        Task<IEnumerable<Member>> GetMemberListAsync();

        // Get by ID
        Task<Member?> GetMemberByIdAsync(int id);

        // Create
        Task<MemberReadDTO> CreateMemberAsync(MemberCreationDTO memberDto);

        // Delete
        Task<bool> DeleteMemberByIdAsync(int memberId);

        // Update
        Task<MemberReadDTO?> UpdateMemberAsync(int memberId, MemberUpdateDTO memberDto);
    }
}
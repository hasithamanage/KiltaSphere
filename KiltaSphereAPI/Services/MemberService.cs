using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;
using KiltaSphereAPI.DTOs;

namespace KiltaSphereAPI.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;

        // Inject the Repository into the Service
        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<IEnumerable<Member>> GetMemberListAsync()
        {
            return await _memberRepository.GetAllMembersAsync();
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            return await _memberRepository.GetMemberByIdAsync(id);
        }

        public async Task<MemberReadDTO> CreateMemberAsync(MemberCreationDTO memberDto)
        {
            // 1. DTO to Entity Mapping
            var memberEntity = new Member
            {
                FirstName = memberDto.FirstName,
                LastName = memberDto.LastName,
                Email = memberDto.Email,
                MembershipStatus = memberDto.MembershipStatus,
                JoiningDate = DateTime.UtcNow
            };

            // 2. Add and Save
            await _memberRepository.AddMemberAsync(memberEntity);
            await _memberRepository.SaveChangesAsync();

            // 3. Entity to Read DTO Mapping (Returning the saved object with its new Id)
            return new MemberReadDTO
            {
                MemberId = memberEntity.MemberId,
                FirstName = memberEntity.FirstName,
                LastName = memberEntity.LastName,
                Email = memberEntity.Email,
                MembershipStatus = memberEntity.MembershipStatus,
                JoiningDate = memberEntity.JoiningDate
            };
        }

        public async Task<MemberReadDTO?> UpdateMemberAsync(int memberId, MemberUpdateDTO memberDto)
        {
            // 1. Fetch the existing entity from the database
            var memberEntity = await _memberRepository.GetMemberByIdAsync(memberId);

            // If the member doesn't exist, return null
            if (memberEntity == null)
            {
                return null;
            }

            // 2. Apply changes from the DTO to the Entity
            // Only update the fields present in the MemberUpdateDTO
            memberEntity.FirstName = memberDto.FirstName;
            memberEntity.LastName = memberDto.LastName;
            memberEntity.Email = memberDto.Email;
            memberEntity.MembershipStatus = memberDto.MembershipStatus;

            // 3. Save the changes to the database
            var success = await _memberRepository.SaveChangesAsync();

            if (!success)
            {
                // Return null if the save operation fails
                return null;
            }

            // 4. Map the updated Entity back to a Read DTO for the response
            return new MemberReadDTO
            {
                MemberId = memberEntity.MemberId,
                FirstName = memberEntity.FirstName,
                LastName = memberEntity.LastName,
                Email = memberEntity.Email,
                MembershipStatus = memberEntity.MembershipStatus,
                JoiningDate = memberEntity.JoiningDate
            };
        }

        public async Task<bool> DeleteMemberByIdAsync(int memberId)
        {
            // Use repository method
            var deleteResult = await _memberRepository.DeleteMemberAsync(memberId);

            if (!deleteResult)
            {
                return false; // Not found in repository
            }

            return await _memberRepository.SaveChangesAsync();
        }
    }
}
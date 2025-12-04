using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;

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

        public async Task<Member?> CreateNewMemberAsync(Member member)
        {
            // Business Logic Check: Could check if email already exists before adding

            await _memberRepository.AddMemberAsync(member);
            var success = await _memberRepository.SaveChangesAsync();

            return success ? member : null; // Return the member or null on failure
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            var deleteResult = await _memberRepository.DeleteMemberAsync(id);

            if (!deleteResult)
            {
                return false; // Not found in repository
            }

            return await _memberRepository.SaveChangesAsync();
        }
    }
}
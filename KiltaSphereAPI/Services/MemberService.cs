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
            // Currently simple pass-through to repository
            return await _memberRepository.GetAllMembersAsync();
        }

        public async Task<bool> CreateNewMemberAsync(Member member)
        {
            // Future Business Logic goes here (e.g., set default status, generate welcome email, validate fields)

            await _memberRepository.AddMemberAsync(member);
            return await _memberRepository.SaveChangesAsync();
        }
    }
}
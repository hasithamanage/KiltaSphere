using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Interfaces
{
    public interface ICommunicationRepository
    {
        Task<IEnumerable<CommunicationLog>> GetLogsByMemberIdAsync(int memberId);
        Task AddLogAsync(CommunicationLog log);
        Task<bool> SaveChangesAsync();
    }
}
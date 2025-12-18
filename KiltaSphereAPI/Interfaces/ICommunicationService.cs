using KiltaSphereAPI.DTOs;

namespace KiltaSphereAPI.Interfaces
{
    public interface ICommunicationService
    {
        Task<IEnumerable<CommunicationReadDTO>> GetLogsForMemberAsync(int memberId);
        Task<CommunicationReadDTO> AddLogAsync(CommunicationCreateDTO createDto);
    }
}
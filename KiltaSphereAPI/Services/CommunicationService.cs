using KiltaSphereAPI.DTOs;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;

namespace KiltaSphereAPI.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ICommunicationRepository _repository;

        public CommunicationService(ICommunicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CommunicationReadDTO>> GetLogsForMemberAsync(int memberId)
        {
            var logs = await _repository.GetLogsByMemberIdAsync(memberId);

            // Mapping Entity -> DTO
            return logs.Select(l => new CommunicationReadDTO
            {
                LogId = l.LogId,
                MemberId = l.MemberId,
                LogDate = l.LogDate,
                CommunicationType = l.CommunicationType,
                SubjectSummary = l.SubjectSummary
            });
        }

        public async Task<CommunicationReadDTO> AddLogAsync(CommunicationCreateDTO createDto)
        {
            var log = new CommunicationLog
            {
                MemberId = createDto.MemberId,
                CommunicationType = createDto.CommunicationType,
                SubjectSummary = createDto.SubjectSummary,
                LogDate = DateTime.UtcNow // Force server-side timestamp
            };

            await _repository.AddLogAsync(log);
            await _repository.SaveChangesAsync();

            return new CommunicationReadDTO
            {
                LogId = log.LogId,
                MemberId = log.MemberId,
                LogDate = log.LogDate,
                CommunicationType = log.CommunicationType,
                SubjectSummary = log.SubjectSummary
            };
        }
    }
}
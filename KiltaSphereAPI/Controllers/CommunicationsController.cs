using KiltaSphereAPI.DTOs;
using KiltaSphereAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KiltaSphereAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommunicationsController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;

        public CommunicationsController(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }

        // GET: api/Communications/member/1
        [HttpGet("member/{memberId}")]
        public async Task<ActionResult<IEnumerable<CommunicationReadDTO>>> GetMemberLogs(int memberId)
        {
            var logs = await _communicationService.GetLogsForMemberAsync(memberId);
            return Ok(logs);
        }

        // POST: api/Communications
        [HttpPost]
        public async Task<ActionResult<CommunicationReadDTO>> CreateLog(CommunicationCreateDTO createDto)
        {
            var result = await _communicationService.AddLogAsync(createDto);
            return Ok(result);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;

// Defines the base route for the controller: /api/members
[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberRepository _memberRepository;

    // Dependency Injection: The controller asks for the IMemberRepository
    public MembersController(IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    // GET: /api/members
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        // 1. Logic: Call the repository to fetch the data
        var members = await _memberRepository.GetAllMembersAsync();

        // 2. Response: Return a 200 OK with the data
        return Ok(members);
    }

    // Add the GET by ID, POST, PUT, and DELETE methods later
}
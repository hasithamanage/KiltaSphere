using Microsoft.AspNetCore.Mvc;
using KiltaSphereAPI.Interfaces;
using KiltaSphereAPI.Models;
using KiltaSphereAPI.DTOs;

// Defines the base route for the controller: /api/members
[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    // Dependency Injection: Inject the Service
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    // GET: /api/members
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        // 1. Logic: Call the service to fetch the data
        var members = await _memberService.GetMemberListAsync();

        // 2. Response: Return a 200 OK with the data
        return Ok(members);
    }

    // GET: /api/members/1
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Member>> GetMember(int id)
    {
        var member = await _memberService.GetMemberByIdAsync(id);

        if (member == null)
        {
            return NotFound(); // Return 404 if member is not found
        }

        return Ok(member); // Return 200 OK with the member
    }

    // POST: /api/members
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Member>> PostMember([FromBody] MemberCreationDTO memberDto)
    {
        // Check Model State (automatic validation from DTO attributes)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 with validation errors
        }

        // 1. Mapping: Convert DTO to Model
        var member = new Member
        {
            FirstName = memberDto.FirstName,
            LastName = memberDto.LastName,
            Email = memberDto.Email,
            JoiningDate = DateTime.Now, // Set server side for integrity
            MembershipStatus = "Pending" // Set server side default
        };

        // 2. Logic: Call the service
        var createdMember = await _memberService.CreateNewMemberAsync(member);

        if (createdMember == null)
        {
            // Handle unique constraint violation (e.g. email already exists)
            return BadRequest("Luoja (Creation failed). Tarkista tiedot.");
        }

        // 3. Response: Return 201 Created with the location of the new resource
        return CreatedAtAction(nameof(GetMember), new { id = member.MemberId }, member);
    }

    // DELETE: /api/members/1
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMember(int id)
    {
        var success = await _memberService.DeleteMemberAsync(id);

        if (!success)
        {
            return NotFound(); // 404 if the member to delete wasn't found
        }

        // Return 204 No Content 
        return NoContent();
    }

}
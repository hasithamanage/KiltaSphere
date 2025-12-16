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
    public async Task<ActionResult<MemberReadDTO>> CreateMember([FromBody] MemberCreationDTO memberDto)
    {
        // Check Model State (Validation)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 with validation errors
        }

        // 1. Logic: Pass the DTO directly to the service. The service handles everything else.
        var createdMember = await _memberService.CreateMemberAsync(memberDto);

        if (createdMember == null)
        {
            return BadRequest("Luoja (Creation failed). Tarkista tiedot.");
        }

        // 2. Response: Return 201 Created with the location of the new resource
        return CreatedAtAction(nameof(GetMember), new { id = createdMember.MemberId }, createdMember);
    }

    // PUT: /api/members/1
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MemberReadDTO>> UpdateMember(int id, [FromBody] MemberUpdateDTO memberDto)
    {
        // 1. Validation Check: Ensure the ID in the route matches the data provided

        // Model state validation (from DTO attributes)
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Return 400 with validation errors
        }

        // 2. Logic: Call the DTO centric service method
        var updatedMember = await _memberService.UpdateMemberAsync(id, memberDto);

        if (updatedMember == null)
        {
            // If ID is not found in the database
            return NotFound();
        }

        // 3. Response: Return 200 OK with the updated member data (MemberReadDTO)
        return Ok(updatedMember);
    }

    // DELETE: /api/members/1
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMember(int id)
    {
        // Call the service method to handle the deletion logic
        var result = await _memberService.DeleteMemberByIdAsync(id);

        if (!result)
        {
            // If the service returns false, the member was not found or save failed
            return NotFound();
        }

        // Return 204 No Content (standard response for a successful delete)
        return NoContent();
    }

}
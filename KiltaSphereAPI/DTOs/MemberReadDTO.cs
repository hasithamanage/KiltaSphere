namespace KiltaSphereAPI.DTOs
{
    // [API Output Contract]
    public class MemberReadDTO
    {
        // The unique identifier (Id) from the database
        public int MemberId { get; set; }

        // Fields matching the Create DTO
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Server set fields for the client to see
        public string MembershipStatus { get; set; } = string.Empty;
        public DateTime JoiningDate { get; set; }
    }
}
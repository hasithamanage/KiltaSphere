using System.ComponentModel.DataAnnotations;

namespace KiltaSphereAPI.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Uses a secure naming convention for sensitive data
        public string? NationalIdentificationNumber { get; set; }

        [Required]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        public DateTime JoiningDate { get; set; } = DateTime.Now;

        // Represents key business logic data
        [MaxLength(50)]
        public string MembershipStatus { get; set; } = "Pending";

        // Navigation property for relationships (optional for now)
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        // Navigation property for communication history
        public ICollection<CommunicationLog> CommunicationLogs { get; set; } = new List<CommunicationLog>();
    }
}
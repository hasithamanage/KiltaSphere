using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiltaSphereAPI.Models
{
    public class CommunicationLog
    {
        [Key]
        public int LogId { get; set; }

        // Foreign Key
        [ForeignKey("Member")]
        public int MemberId { get; set; }

        public DateTime LogDate { get; set; } = DateTime.UtcNow;

        // e.g., "Email Invoice", "SMS Reminder", "System Alert"
        [Required]
        [MaxLength(50)]
        public string CommunicationType { get; set; } = string.Empty;

        // Summary of the content sent
        [Required]
        public string SubjectSummary { get; set; } = string.Empty;

        // Navigation property
        public Member Member { get; set; } = null!;
    }
}
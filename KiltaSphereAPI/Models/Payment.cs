using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KiltaSphereAPI.Models
{
    public class Payment
    {
        // Primary Key for the Payment entity
        [Key]
        public int PaymentId { get; set; }

        // Foreign Key linking back to the Member table
        [ForeignKey("Member")]
        public int MemberId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")] // Ensure SQL precision
        public decimal Amount { get; set; }

        // Rename PaymentDate to PaidDate to be more specific
        public DateTime? PaidDate { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentStatus { get; set; } = "Pending"; // e.g., "Paid", "Pending", "Overdue"

        // Adding the missing ReferenceNumber for Finnish "Viitenumero" logic
        [Required]
        [MaxLength(100)]
        public string ReferenceNumber { get; set; } = string.Empty;

        // Navigation property (allows loading the related Member object)
        public Member Member { get; set; } = null!;
    }
}
using System.ComponentModel.DataAnnotations;

namespace KiltaSphereAPI.DTOs
{
    public class MemberUpdateDTO
    {
        // Only allow changes to the editable fields
        [Required(ErrorMessage = "Etunimi (First Name) on pakollinen.")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sukunimi (Last Name) on pakollinen.")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sähköposti (Email) on pakollinen.")]
        [EmailAddress]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        // Allowing updates to the status is a key business function
        [Required]
        [MaxLength(50)]
        public string MembershipStatus { get; set; } = "Active";
    }
}
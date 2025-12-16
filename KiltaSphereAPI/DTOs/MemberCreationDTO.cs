using System.ComponentModel.DataAnnotations;

namespace KiltaSphereAPI.DTOs
{
    // [API Input Contract]
    public class MemberCreationDTO
    {
        [Required(ErrorMessage = "Etunimi (First Name) on pakollinen.")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sukunimi (Last Name) on pakollinen.")]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Sähköposti (Email) on pakollinen.")]
        [EmailAddress] // Ensures the format is a valid email
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        // Status is mandatory and is sent by the Vue form
        [Required]
        public string MembershipStatus { get; set; } = string.Empty;

        // Omitted Id, NationalIdentificationNumber fields for data protection
        // and RegistrationDate (set by server)
       
    }
}
using System.ComponentModel.DataAnnotations;

namespace KiltaSphereAPI.DTOs
{
    // [Data Validation & API Input Control]
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

        // Omit sensitive fields like NationalIdentificationNumber 
        // and internal fields like JoiningDate/Status from the creation input
    }
}
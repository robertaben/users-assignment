using System.ComponentModel.DataAnnotations;

namespace BlazorUserApp.Client.DTOs
{
    public class UserWriteModel
    {
        [Required]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž]+$", ErrorMessage = "First name can only contain letters")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters.")]
        [MaxLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-zĄČĘĖĮŠŲŪŽąčęėįšųūž]+$", ErrorMessage = "Last name can only contain letters")]
        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"^(\+\d{7,15}|00\d{7,15}|\d{7,15})$", ErrorMessage = "Phone number must start with '+', '00', or contain only digits with a length of 7 to 15 characters.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
    }
}
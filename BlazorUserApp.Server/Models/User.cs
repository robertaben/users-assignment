using System.ComponentModel.DataAnnotations;

namespace BlazorUserApp.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public required string Email { get; set; }
    }
}
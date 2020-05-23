using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class RegistrationModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; }
    }
}
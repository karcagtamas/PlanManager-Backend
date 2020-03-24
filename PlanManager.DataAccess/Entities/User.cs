using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities
{
    public class User
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        [MaxLength(100), MinLength(8)]
        public string UserName { get; set; }
        
        [Required]
        [MaxLength(100), MinLength(8)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(120), EmailAddress]
        public string Email { get; set; }
        
        [MaxLength(100)]
        public string FullName { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}
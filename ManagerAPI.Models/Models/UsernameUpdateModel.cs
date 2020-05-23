using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class UsernameUpdateModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Username's max length is 100")]
        [MinLength(6)]
        public string UserName { get; set; }
    }
}
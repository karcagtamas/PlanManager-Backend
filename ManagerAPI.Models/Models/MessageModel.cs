using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class MessageModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string PartnerId { get; set; }
    }
}
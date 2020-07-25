using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    public class MessageModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string PartnerId { get; set; }
    }
}
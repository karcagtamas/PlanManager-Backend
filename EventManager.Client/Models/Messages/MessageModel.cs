using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.Messages
{
    public class MessageModel
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public string PartnerId { get; set; }
    }
}
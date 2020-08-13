using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// Message create or update model
    /// </summary>
    public class MessageModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Message { get; set; }

        [Required] public string PartnerId { get; set; }
    }
}
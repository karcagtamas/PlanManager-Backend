using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs.EM
{
    public class EventCreateDto
    {
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    public class GenderModel
    {
        [Required]
        public string Name { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models.WM
{
    public class WokringFieldModel
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public int Length { get; set; }
    }
}

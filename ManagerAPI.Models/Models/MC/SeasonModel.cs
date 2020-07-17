using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models.MC
{
    public class SeasonModel
    {
        [Required]
        public int Number { get; set; }
    }
}
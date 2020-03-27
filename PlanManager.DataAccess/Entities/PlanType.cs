using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities
{
    public class PlanType
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
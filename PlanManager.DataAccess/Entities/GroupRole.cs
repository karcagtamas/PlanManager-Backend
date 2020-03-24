using System.ComponentModel.DataAnnotations;

namespace PlanManager.DataAccess.Entities
{
    public class GroupRole
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int AccessLevel { get; set; }
    }
}
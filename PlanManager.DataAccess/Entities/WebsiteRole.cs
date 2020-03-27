using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PlanManager.DataAccess.Entities
{
    public class WebsiteRole : IdentityRole
    {
        [Required]
        public string Title { get; set; }
        
        [Required]
        public int AccessLevel { get; set; }
    }
}
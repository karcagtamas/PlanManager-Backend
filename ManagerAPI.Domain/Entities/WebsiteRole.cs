using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ManagerAPI.Domain.Entities
{
    public class WebsiteRole : IdentityRole
    {
        [Required]
        public int AccessLevel { get; set; }
    }
}
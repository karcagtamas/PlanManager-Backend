using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Domain.Entities
{
    public class WebsiteRole : IdentityRole
    {
        [Required]
        public int AccessLevel { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PlanManager.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
        
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime LastLogin { get; set; }
    }
}
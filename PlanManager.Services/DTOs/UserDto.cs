using System;
using System.ComponentModel.DataAnnotations;

namespace PlanManager.Services.DTOs 
{
    /// <summary>
    /// User DTO
    /// </summary>
    public class UserDto 
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }
    }
    
    /// <summary>
    /// User Update DTO
    /// Model for user Update
    /// </summary>
    public class UserUpdateDto
    {
        [Required]
        public string Id { get; set; }
        
        [Required]
        public string FullName { get; set; }
    }
}
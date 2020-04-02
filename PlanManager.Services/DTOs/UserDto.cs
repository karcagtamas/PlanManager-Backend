using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PlanManager.DataAccess.Entities;
using PlanManager.Services.Utils;

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
        public string SecondaryEmail { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
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
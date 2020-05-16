using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Client.Models.User
{
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

    public class UserUpdateDto
    {
        [Required]
        public string Id { get; set; }
        
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [EmailAddress]
        public string SecondaryEmail { get; set; }
        
        [Phone]
        public string PhoneNumber { get; set; }

        public UserUpdateDto(UserDto user)
        {
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.SecondaryEmail = user.SecondaryEmail;
            this.PhoneNumber = user.PhoneNumber;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.DTOs
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
        public string TShirtSize { get; set; }
        public string Allergy { get; set; }
        public string Group { get; set; }
        public DateTime? BirthDay { get; set; }
        public string ProfileImageTitle { get; set; }
        public byte[] ProfileImageData { get; set; }
        public string Country { get; set; }
        public int? GenderId { get; set; }
        public string City { get; set; }
        public List<string> Roles { get; set; }
    }

    public class UserShortDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
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

        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [EmailAddress]
        public string SecondaryEmail { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(6)]
        public string TShirtSize { get; set; }

        public string Allergy { get; set; }

        [MaxLength(40)]
        public string Group { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(120)]
        public string Country { get; set; }

        public int? GenderId { get; set; }

        [MaxLength(120)]
        public string City { get; set; }
    }
}
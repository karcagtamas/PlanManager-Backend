using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.DTOs
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
        [Required(ErrorMessage = "Id field is required")]
        public string Id { get; set; }

        [MaxLength(100, ErrorMessage = "Maximum length is 100")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-mail is required")]
        [EmailAddress(ErrorMessage = "Must be valid E-mail")]
        public string Email { get; set; }

        [EmailAddress(ErrorMessage = "Must be valid E-mail")]
        public string SecondaryEmail { get; set; }

        [Phone(ErrorMessage = "Must be valid Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(6, ErrorMessage = "Maximum length is 6")]
        public string TShirtSize { get; set; }

        public string Allergy { get; set; }

        [MaxLength(40, ErrorMessage = "Maximum length is 40")]
        public string Group { get; set; }

        public DateTime? BirthDay { get; set; }

        [MaxLength(120, ErrorMessage = "Maximum length is 120")]
        public string Country { get; set; }

        public int? GenderId { get; set; }

        [MaxLength(120, ErrorMessage = "Maximum length is 120")]
        public string City { get; set; }

        public UserUpdateDto() { }

        public UserUpdateDto(UserDto user)
        {
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.SecondaryEmail = user.SecondaryEmail;
            this.PhoneNumber = user.PhoneNumber;
            this.TShirtSize = user.TShirtSize;
            this.Allergy = user.Allergy;
            this.Group = user.Group;
            this.BirthDay = user.BirthDay;
            this.Country = user.Country;
            this.GenderId = user.GenderId;
            this.City = user.City;
        }
    }
}
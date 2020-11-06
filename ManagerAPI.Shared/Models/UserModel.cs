using ManagerAPI.Shared.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Shared.Models
{
    /// <summary>
    /// User Update DTO
    /// Model for user Update
    /// </summary>
    public class UserModel
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

        /// <summary>
        /// Empty init
        /// </summary>
        public UserModel() { }

        /// <summary>
        /// Model from user data object
        /// </summary>
        /// <param name="user"></param>
        public UserModel(UserDto user)
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
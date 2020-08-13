using System;
using System.Collections.Generic;

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
}
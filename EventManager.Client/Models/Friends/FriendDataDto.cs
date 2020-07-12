using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.Friends
{
    public class FriendDataDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string TShirtSize { get; set; }
        public string Allergy { get; set; }
        public string Group { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public List<string> Roles { get; set; }
    }
}

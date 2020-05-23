using System;

namespace EventManager.Client.Models.User
{
    public class UserShortDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
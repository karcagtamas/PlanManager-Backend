using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// User short DTO
    /// </summary>
    public class UserShortDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
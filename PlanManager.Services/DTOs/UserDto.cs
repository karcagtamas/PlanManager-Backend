using System.ComponentModel.DataAnnotations;

namespace PlanManager.Services.DTOs 
{
    public class UserDto 
    {
        public string Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
    }

    public class UserUpdateDto
    {
        public string Id { get; set; }
        public string FullName { get; set; }
    }
}
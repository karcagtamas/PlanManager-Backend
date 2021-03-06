using System;

namespace ManagerAPI.Shared.DTOs.EM
{
    /// <summary>
    /// My event list DTO
    /// </summary>
    public class MyEventListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Members { get; set; }
        public string Creator { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
    }
}
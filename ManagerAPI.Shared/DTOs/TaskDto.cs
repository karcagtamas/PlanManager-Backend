using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// Task DTO
    /// </summary>
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsSolved { get; set; }
    }
}
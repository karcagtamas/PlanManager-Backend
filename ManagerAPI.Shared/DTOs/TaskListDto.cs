using System;

namespace ManagerAPI.Shared.DTOs
{
    /// <summary>
    /// Task list DTO
    /// </summary>
    public class TaskListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsSolved { get; set; }
    }
}
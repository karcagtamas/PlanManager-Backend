using System;

namespace ManagerAPI.Models.DTOs
{
    public class TaskDataDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsSolved { get; set; }
    }
}

using System;

namespace ManagerAPI.Models.DTOs
{
    public class ToDoDataDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsSolved { get; set; }
    }
}

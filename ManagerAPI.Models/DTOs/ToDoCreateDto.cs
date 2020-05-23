using System;

namespace ManagerAPI.Models.DTOs
{
    public class ToDoCreateDto
    {
        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public ToDoCreateDto() { }
    }
}

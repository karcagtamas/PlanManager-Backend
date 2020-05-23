using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs
{
    public class ToDoDateDto
    {
        public DateTime DueDate { get; set; }

        public List<ToDoDto> ToDoList { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs
{
    public class TaskDateDto
    {
        public DateTime Deadline { get; set; }

        public List<TaskDto> TaskList { get; set; }

    }
}

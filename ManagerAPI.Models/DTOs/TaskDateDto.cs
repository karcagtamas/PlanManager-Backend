using System;
using System.Collections.Generic;

namespace ManagerAPI.Models.DTOs
{
    public class TaskDateDto
    {
        public DateTime Deadline { get; set; }
        public bool OutOfRange { get; set; }
        public bool AllSolved { get; set; }
        public List<TaskDto> TaskList { get; set; }
    }
}

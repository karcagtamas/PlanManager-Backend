using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.Tasks
{
    public class TaskModel
    {
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}

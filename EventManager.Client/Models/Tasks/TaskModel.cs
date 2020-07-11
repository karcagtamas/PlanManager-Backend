using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.Tasks
{
    public class TaskModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(100, ErrorMessage = "Max length is 100")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public DateTime Deadline { get; set; }
        
        [Required(ErrorMessage = "Field is required")]
        public string Description { get; set; }
    }
}

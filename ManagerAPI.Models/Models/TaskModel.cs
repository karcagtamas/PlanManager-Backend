using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerAPI.Models.Models
{
    public class TaskModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public string Description { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using ManagerAPI.Shared.DTOs;

namespace ManagerAPI.Shared.Models
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

        [Required]
        public bool IsSolved { get; set; } = false;

        public TaskModel() {}

        public TaskModel(TaskDto task) {
            this.Title = task.Title;
            this.Deadline = task.Deadline;
            this.Description = task.Description;
            this.IsSolved = task.IsSolved;
        }
    }
}

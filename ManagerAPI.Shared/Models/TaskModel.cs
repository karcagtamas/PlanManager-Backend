using System;
using System.ComponentModel.DataAnnotations;

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
    }
}

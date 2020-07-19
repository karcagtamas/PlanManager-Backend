using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
{
    public class WorkingFieldModel
    {
        [Required(ErrorMessage = "Field is required")]
        [MaxLength(200, ErrorMessage = "Maximum length is 200")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int Length { get; set; }
    }
}

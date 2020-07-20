using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models.EM
{
    public class EventModel
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(256, ErrorMessage = "Title's max length is 256")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}

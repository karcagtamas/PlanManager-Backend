using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models.WM
{
    public class WorkingDayInitModel
    {
        [Required]
        public DateTime Date { get; set; }
    }
}

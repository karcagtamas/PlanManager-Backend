using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Models.WM
{
    public class WorkingDayInitModel
    {
        [Required]
        public DateTime Date { get; set; }
    }
}

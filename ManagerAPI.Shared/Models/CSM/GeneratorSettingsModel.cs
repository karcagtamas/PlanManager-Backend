using ManagerAPI.Shared.Annotations;
using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class GeneratorSettingsModel
    {
        [Required]
        [MaxLength(120)]
        public string Title { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime Finish { get; set; }

        [Required]
        [MinNumber(1)]
        [MaxNumber(8)]
        public int MaxWorkHour { get; set; } = 3;

        [Required]
        [MinNumber(1)]
        [MaxNumber(4)]
        public int MinRestHour { get; set; } = 1;
        public List<PersonModel> Persons { get; set; }
        public List<Work> Works { get; set; }

        public GeneratorSettingsModel()
        {
            this.Title = "New Generator";
            var date = DateTime.Now;
            date = date.AddMinutes(-date.Minute).AddMinutes(-date.Second).ToLocalTime();
            this.Start = date;
            this.Finish = date.AddDays(1);
            this.MaxWorkHour = 3;
            this.MinRestHour = 1;
        }
    }
}

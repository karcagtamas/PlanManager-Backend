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
    }
}

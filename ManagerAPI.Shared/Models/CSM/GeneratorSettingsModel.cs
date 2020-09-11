using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class GeneratorSettingsModel
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int MaxWorkHour { get; set; } = 3;
        public int MinRestHour { get; set; } = 1;
        public List<PersonModel> Persons { get; set; }
        public List<Work> Works { get; set; }
    }
}

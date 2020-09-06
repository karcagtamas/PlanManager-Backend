using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class GeneratorSettings
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int MaxWorkHour { get; set; } = 3;
        public int MinRestHour { get; set; } = 1;
        public List<Person> Persons { get; set; }
        public List<Work> Works { get; set; }
    }
}
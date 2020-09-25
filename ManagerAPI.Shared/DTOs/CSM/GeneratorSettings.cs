using System;
using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class GeneratorSettings
    {
        public int? Id { get; set; }
        public string Title { get; set; } = "Untitled";
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public int MaxWorkHour { get; set; } = 3;
        public int MinRestHour { get; set; } = 1;
        public List<Person> Persons { get; set; }
        public List<Work> Works { get; set; }

        public DateTime? Creation { get; set; }
        public string Owner { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string LastUpdater { get; set; }
        public bool? IsShared { get; set; }
        public bool? IsPublic { get; set; }
        public bool? HasGeneratedCsomor { get; set; }
        public DateTime? LastGeneration { get; set; }
        public List<CsomorAccessDTO> SharedWith { get; set; }
    }
}
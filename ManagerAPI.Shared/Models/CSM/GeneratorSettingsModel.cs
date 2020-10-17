using ManagerAPI.Shared.Annotations;
using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        public bool HasGeneratedCsomor { get; set; }
        public DateTime? LastGeneration { get; set; }
        public List<PersonModel> Persons { get; set; }
        public List<WorkModel> Works { get; set; }

        public GeneratorSettingsModel()
        {
            Title = "New Generator";
            DateTime date = DateTime.UtcNow;
            date = date.AddMinutes(-date.Minute).AddSeconds(-date.Second);
            Start = date;
            Finish = date.AddDays(1);
            MaxWorkHour = 3;
            MinRestHour = 1;
            HasGeneratedCsomor = false;
            LastGeneration = null;

            Persons = new List<PersonModel>();
            Works = new List<WorkModel>();
        }

        public GeneratorSettingsModel(GeneratorSettings settings)
        {
            Title = settings.Title;
            Start = settings.Start;
            Finish = settings.Finish;
            MaxWorkHour = settings.MaxWorkHour;
            MinRestHour = settings.MinRestHour;
            HasGeneratedCsomor = settings.HasGeneratedCsomor;
            LastGeneration = settings.LastGeneration;

            Persons = settings.Persons.Select(x => new PersonModel(x)).OrderBy(x => x.Name).ToList();
            Works = settings.Works.Select(x => new WorkModel(x)).OrderBy(x => x.Name).ToList();
        }
    }
}

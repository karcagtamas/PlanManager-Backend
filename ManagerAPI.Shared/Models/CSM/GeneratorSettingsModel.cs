using ManagerAPI.Shared.Annotations;
using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public bool HasGeneratedCsomor { get; set; }
        public DateTime? LastGeneration { get; set; }
        public List<PersonModel> Persons { get; set; }
        public List<WorkModel> Works { get; set; }

        public GeneratorSettingsModel()
        {
            this.Title = "New Generator";
            var date = DateTime.UtcNow;
            date = date.AddMinutes(-date.Minute).AddSeconds(-date.Second);
            this.Start = date;
            this.Finish = date.AddDays(1);
            this.MaxWorkHour = 3;
            this.MinRestHour = 1;
            this.HasGeneratedCsomor = false;
            this.LastGeneration = null;

            this.Persons = new List<PersonModel>();
            this.Works = new List<WorkModel>();
        }

        public GeneratorSettingsModel(GeneratorSettings settings)
        {
            this.Title = settings.Title;
            this.Start = settings.Start;
            this.Finish = settings.Finish;
            this.MaxWorkHour = settings.MaxWorkHour;
            this.MinRestHour = settings.MinRestHour;
            this.HasGeneratedCsomor = settings.HasGeneratedCsomor;
            this.LastGeneration = settings.LastGeneration;

            this.Persons = settings.Persons.Select(x => new PersonModel(x)).OrderBy(x => x.Name).ToList();
            this.Works = settings.Works.Select(x => new WorkModel(x)).OrderBy(x => x.Name).ToList();
        }
    }
}

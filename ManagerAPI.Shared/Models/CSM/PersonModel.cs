using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Field is required")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [MaxLength(120, ErrorMessage = "Max length is 120")]
        public string Name { get; set; }
        public List<PersonTableModel> Tables { get; set; }
        public List<string> IgnoredWorks { get; set; }
        public bool IsIgnored { get; set; }

        public PersonModel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Tables = new List<PersonTableModel>();
            this.IgnoredWorks = new List<string>();
            this.IsIgnored = false;
        }

        public PersonModel(Person person)
        {
            this.Id = person.Id;
            this.Name = person.Name;
            this.Tables = person.Tables.Select(x => new PersonTableModel(x)).ToList();
            this.IgnoredWorks = person.IgnoredWorks;
            this.IsIgnored = person.IsIgnored;
        }

        public void SetTables(DateTime start, DateTime finish)
        {
            var s = start;
            while (s < finish)
            {
                Tables.Add(new PersonTableModel(s));
                s = s.AddHours(1);
            }
        }

        public void UpdateTable(DateTime newStart, DateTime newFinish)
        {
            var oldList = this.Tables;
            this.Tables = new List<PersonTableModel>();

            this.SetTables(newStart, newFinish);

            foreach (var i in this.Tables)
            {
                var e = oldList.FirstOrDefault(x => DateHelper.CompareDates(x.Date, i.Date));
                if (e != null)
                {
                    i.IsAvailable = e.IsAvailable;
                }
            }
        }
    }
}

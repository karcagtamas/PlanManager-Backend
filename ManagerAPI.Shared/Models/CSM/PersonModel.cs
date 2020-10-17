using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
            Id = Guid.NewGuid().ToString();
            Tables = new List<PersonTableModel>();
            IgnoredWorks = new List<string>();
            IsIgnored = false;
        }

        public PersonModel(string name)
        {
            Id = Guid.NewGuid().ToString();
            Tables = new List<PersonTableModel>();
            IgnoredWorks = new List<string>();
            IsIgnored = false;
            Name = name;
        }

        public PersonModel(Person person)
        {
            Id = person.Id;
            Name = person.Name;
            Tables = person.Tables.Select(x => new PersonTableModel(x)).OrderBy(x => x.Date).ToList();
            IgnoredWorks = person.IgnoredWorks;
            IsIgnored = person.IsIgnored;
        }

        public void SetTables(DateTime start, DateTime finish)
        {
            DateTime s = start;
            while (s < finish)
            {
                Tables.Add(new PersonTableModel(s));
                s = s.AddHours(1);
            }
        }

        public void UpdateTable(DateTime newStart, DateTime newFinish)
        {
            List<PersonTableModel> oldList = Tables;
            Tables = new List<PersonTableModel>();

            SetTables(newStart, newFinish);

            foreach (PersonTableModel i in Tables)
            {
                PersonTableModel e = oldList.FirstOrDefault(x => DateHelper.CompareDates(x.Date, i.Date));
                if (e != null)
                {
                    i.IsAvailable = e.IsAvailable;
                }
            }
        }
    }
}

using ManagerAPI.Shared.Models.CSM;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<PersonTable> Tables { get; set; }
        public List<string> IgnoredWorks { get; set; }
        public bool IsIgnored { get; set; }
        public int Hours { get; set; } = 0;

        public Person()
        {

        }

        public Person(PersonModel model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Tables = model.Tables.Select(x => new PersonTable(x)).ToList();
            this.IgnoredWorks = model.IgnoredWorks;
            this.IsIgnored = model.IsIgnored;
        }
    }
}
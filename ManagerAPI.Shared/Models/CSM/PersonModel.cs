using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class PersonModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<PersonTableModel> Tables { get; set; }
        public List<string> IgnoredWorks { get; set; }
        public bool IsIgnored { get; set; }
    }
}

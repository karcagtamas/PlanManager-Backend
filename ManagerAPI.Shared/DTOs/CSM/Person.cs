using System.Collections.Generic;

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
    }
}
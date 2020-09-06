using System.Collections.Generic;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class Work
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<WorkTable> Tables { get; set; }
    }
}
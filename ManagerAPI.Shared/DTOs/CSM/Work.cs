using ManagerAPI.Shared.Models.CSM;
using System.Collections.Generic;
using System.Linq;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class Work
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<WorkTable> Tables { get; set; }

        public Work() { }

        public Work(WorkModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Tables = model.Tables.Select(x => new WorkTable(x)).ToList();
        }
    }
}
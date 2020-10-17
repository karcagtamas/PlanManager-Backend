using ManagerAPI.Shared.DTOs.CSM;
using System;

namespace ManagerAPI.Shared.Models.CSM
{
    public class PersonTableModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public string WorkId { get; set; }

        public PersonTableModel() { }

        public PersonTableModel(DateTime date)
        {
            Id = Guid.NewGuid().ToString();
            Date = date;
            IsAvailable = true;
            WorkId = null;
        }

        public PersonTableModel(PersonTable table)
        {
            Id = table.Id;
            Date = table.Date;
            IsAvailable = table.IsAvailable;
            WorkId = table.WorkId;
        }
    }
}

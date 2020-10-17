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
            this.Id = Guid.NewGuid().ToString();
            this.Date = date;
            this.IsAvailable = true;
            this.WorkId = null;
        }

        public PersonTableModel(PersonTable table)
        {
            this.Id = table.Id;
            this.Date = table.Date;
            this.IsAvailable = table.IsAvailable;
            this.WorkId = table.WorkId;
        }
    }
}

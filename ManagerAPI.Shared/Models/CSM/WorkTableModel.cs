using ManagerAPI.Shared.DTOs.CSM;
using System;

namespace ManagerAPI.Shared.Models.CSM
{
    public class WorkTableModel
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string PersonId { get; set; }

        public WorkTableModel() { }

        public WorkTableModel(DateTime date)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Date = date;
            this.IsActive = true;
            this.PersonId = null;
        }

        public WorkTableModel(WorkTable table)
        {
            this.Id = table.Id;
            this.Date = table.Date;
            this.IsActive = table.IsActive;
            this.PersonId = table.PersonId;
        }
    }
}

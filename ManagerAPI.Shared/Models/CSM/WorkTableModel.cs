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
            Id = Guid.NewGuid().ToString();
            Date = date;
            IsActive = true;
            PersonId = null;
        }

        public WorkTableModel(WorkTable table)
        {
            Id = table.Id;
            Date = table.Date;
            IsActive = table.IsActive;
            PersonId = table.PersonId;
        }
    }
}

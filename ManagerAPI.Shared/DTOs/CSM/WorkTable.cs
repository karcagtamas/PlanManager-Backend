using ManagerAPI.Shared.Models.CSM;
using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class WorkTable
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string PersonId { get; set; }

        public WorkTable() { }

        public WorkTable(WorkTableModel model)
        {
            this.Id = model.Id;
            this.Date = model.Date;
            this.IsActive = model.IsActive;
            this.PersonId = null;
        }
    }
}
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
            Id = model.Id;
            Date = model.Date;
            IsActive = model.IsActive;
            PersonId = null;
        }
    }
}
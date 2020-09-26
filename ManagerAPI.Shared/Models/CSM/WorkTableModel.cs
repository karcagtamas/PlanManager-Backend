using ManagerAPI.Shared.DTOs.CSM;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class WorkTableModel
    {
        public DateTime Date;
        public bool IsActive { get; set; }
        public string PersonId { get; set; }

        public WorkTableModel() { }

        public WorkTableModel(DateTime date)
        {
            this.Date = date;
            this.IsActive = true;
            this.PersonId = null;
        }

        public WorkTableModel(WorkTable table)
        {
            this.Date = table.Date;
            this.IsActive = table.IsActive;
            this.PersonId = table.PersonId;
        }
    }
}

using ManagerAPI.Shared.Models.CSM;
using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class PersonTable
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public string WorkId { get; set; }

        public PersonTable() { }

        public PersonTable(PersonTableModel model)
        {
            Id = model.Id;
            Date = model.Date;
            IsAvailable = model.IsAvailable;
            WorkId = null;
        }
    }
}
using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class WorkTable
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public string PersonId { get; set; }
    }
}
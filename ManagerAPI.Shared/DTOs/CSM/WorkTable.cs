using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class WorkTable
    {
        public DateTime Date;
        public bool IsAvailable { get; set; }
        public string PersonId { get; set; }
    }
}
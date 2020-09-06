using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class PersonTable
    {
        public DateTime Date;
        public bool IsAvailable { get; set; }
        public string WorkId { get; set; }
    }
}
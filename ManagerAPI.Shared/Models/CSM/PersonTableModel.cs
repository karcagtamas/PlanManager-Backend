using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class PersonTableModel
    {
        public DateTime Date;
        public bool IsAvailable { get; set; }
        public string WorkId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class WorkModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<WorkTableModel> Tables { get; set; }
    }
}

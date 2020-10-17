using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class CsomorListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Owner { get; set; }
        public bool HasCsomor { get; set; }
        public bool IsPublished { get; set; }
        public bool IsShared { get; set; }
        public DateTime Creation { get; set; }
    }
}

using System;

namespace ManagerAPI.Shared.DTOs.CSM
{
    public class CsomorAccessDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool HasWriteAccess { get; set; }
        public DateTime SharedOn { get; set; }
    }
}

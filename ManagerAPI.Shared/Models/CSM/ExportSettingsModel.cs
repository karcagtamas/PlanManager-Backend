using ManagerAPI.Shared.Enums;
using System.Collections.Generic;

namespace ManagerAPI.Shared.Models.CSM
{
    public class ExportSettingsModel
    {
        public CsomorType Type { get; set; }
        public List<string> FilterList { get; set; }
    }
}

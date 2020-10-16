using ManagerAPI.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerAPI.Shared.Models.CSM
{
    public class ExportSettingsModel
    {
        public CsomorType Type { get; set; }
        public List<string> FilterList { get; set; }
    }
}

using ManagerAPI.Shared.DTOs.CSM;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class CsomorComponent
    {
        [Parameter]
        public GeneratorSettings Settings { get; set; }
    }
}

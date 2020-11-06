using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Enums;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class CsomorComponent
    {
        [Parameter]
        public GeneratorSettings Settings { get; set; }

        [Parameter]
        public CsomorType TableType { get; set; }

        [Parameter]
        public List<string> FilterList { get; set; }

        [Parameter]
        public EventCallback<CsomorType> TableTypeChanged { get; set; }

        [Parameter]
        public EventCallback<List<string>> FilterListChanged { get; set; }

        private async void Changed(CsomorType element)
        {
            await this.TableTypeChanged.InvokeAsync(element);
        }

        private async void Changed(List<string> element)
        {
            await this.FilterListChanged.InvokeAsync(element);
        }
    }
}

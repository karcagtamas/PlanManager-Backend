using ManagerAPI.Shared.DTOs.CSM;
using Microsoft.AspNetCore.Components;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class CsomorListElementComponent
    {
        [Parameter]
        public CsomorListDTO Csomor { get; set; }

        [Parameter]
        public bool ShowOwner { get; set; } = true;

        [Parameter]
        public bool ShowIsPublished { get; set; } = true;

        [Parameter]
        public bool ShowIsShared { get; set; } = true;

        [Inject]
        public NavigationManager NavigationManager { get; set; }
    }
}

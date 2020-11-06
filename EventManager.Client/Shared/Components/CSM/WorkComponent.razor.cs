using ManagerAPI.Shared.Enums;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using System;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class WorkComponent
    {
        [Parameter]
        public WorkModel Work { get; set; }

        [Parameter]
        public EventCallback StateChanged { get; set; }

        [Parameter]
        public Func<CsomorRole[], bool> RoleChecker { get; set; }

        [Parameter]
        public bool IsEditable { get; set; } = false;

        private bool IsEdit { get; set; } = false;
        private bool IsOpened { get; set; } = false;

        private void ChangeIsActiveStatus(WorkTableModel model, bool value)
        {
            model.IsActive = value;
            this.StateChanged.InvokeAsync();
        }

        private bool CheckRole(params CsomorRole[] roles)
        {
            return this.RoleChecker.Invoke(roles);
        }
    }
}

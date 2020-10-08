using ManagerAPI.Shared.Enums;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Shared.Components.CSM
{
    public partial class PersonComponent
    {
        [Parameter]
        public PersonModel Person { get; set; }

        [Parameter]
        public EventCallback StateChanged { get; set; }

        [Parameter]
        public Func<CsomorRole[], bool> RoleChecker { get; set; }

        [Parameter]
        public List<WorkModel> Works { get; set; }

        [Parameter]
        public bool IsEditable { get; set; } = false;

        private bool IsEdit { get; set; } = false;
        private bool IsOpened { get; set; } = false;

        private void ChangeIsAvailableStatus(PersonTableModel model, bool value)
        {
            model.IsAvailable = value;
            this.StateChanged.InvokeAsync();
        }

        private void ChangeIsIgnoredStatus(PersonModel model, bool value)
        {
            model.IsIgnored = value;
            this.StateChanged.InvokeAsync();
        }

        private bool CheckRole(params CsomorRole[] roles)
        {
            return RoleChecker.Invoke(roles);
        }

        private bool WorkIsIgnored(PersonModel person, WorkModel work)
        {
            return person.IgnoredWorks.Any(x => x == work.Id);
        }

        private void IgnoredWorkChange(PersonModel person, WorkModel work, bool value)
        {
            if (value)
            {
                if (!person.IgnoredWorks.Any(x => x == work.Id))
                {
                    person.IgnoredWorks.Add(work.Id);
                }
            }
            else
            {
                if (person.IgnoredWorks.Any(x => x == work.Id))
                {
                    person.IgnoredWorks.Remove(work.Id);
                }
            }
            this.StateChanged.InvokeAsync();
        }
    }
}

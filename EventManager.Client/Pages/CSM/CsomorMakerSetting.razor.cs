using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace EventManager.Client.Pages.CSM
{
    public partial class CsomorMakerSetting
    {
        [Parameter]
        public int? Id { get; set; }

        private EditContext Context { get; set; }
        private GeneratorSettingsModel Model { get; set; }
        private List<PersonModel> Persons { get; set; }
        private List<WorkModel> Works { get; set; }
        private EditContext PersonContext { get; set; }
        private PersonModel PersonModel { get; set; }
        private EditContext WorkContext { get; set; }
        private WorkModel WorkModel { get; set; }

        protected override void OnInitialized()
        {
            this.Model = new GeneratorSettingsModel();
            this.Context = new EditContext(this.Model);

            this.WorkModel = new WorkModel();
            this.PersonModel = new PersonModel();

            this.WorkContext = new EditContext(this.WorkModel);
            this.PersonContext = new EditContext(this.PersonModel);
        }
    }
}
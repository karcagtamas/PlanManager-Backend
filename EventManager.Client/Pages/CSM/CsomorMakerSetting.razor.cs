using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;

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

            this.PersonModel = new PersonModel();
            this.WorkModel = new WorkModel();

            this.PersonContext = new EditContext(this.PersonModel);
            this.WorkContext = new EditContext(this.WorkModel);

            this.Persons = new List<PersonModel>();
            this.Works = new List<WorkModel>();
        }

        private void SaveSettings() { }

        private void AddPerson()
        {
            if (this.PersonContext.Validate())
            {
                this.PersonModel.SetTables(this.Model.Start, this.Model.Finish);
                this.Persons.Add(this.PersonModel);
                this.PersonModel = new PersonModel();
                this.PersonContext = new EditContext(this.PersonModel);
                StateHasChanged();
            }
        }

        private void AddWork()
        {
            if (this.WorkContext.Validate())
            {
                this.WorkModel.SetTables(this.Model.Start, this.Model.Finish);
                this.Works.Add(this.WorkModel);
                this.WorkModel = new WorkModel();
                this.WorkContext = new EditContext(this.WorkModel);
                StateHasChanged();
            }
        }

        private void ReSetup(DateTime date, string type)
        {
            if (type == "start")
            {
                this.Model.Start = date.AddMinutes(-date.Minute).ToLocalTime();
            }
            if (type == "finish")
            {
                this.Model.Finish = date.AddMinutes(-date.Minute).ToLocalTime();
            }
            this.Persons.ForEach(x => x.UpdateTable(this.Model.Start, this.Model.Finish));
            this.Works.ForEach(x => x.UpdateTable(this.Model.Start, this.Model.Finish));
            StateHasChanged();
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
        }

        private bool WorkIsIgnored(PersonModel person, WorkModel work)
        {
            return person.IgnoredWorks.Any(x => x == work.Id);
        }
    }
}
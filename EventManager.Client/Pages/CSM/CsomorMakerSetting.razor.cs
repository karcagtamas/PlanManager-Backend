using EventManager.Client.Services.Interfaces;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.CSM
{
    public partial class CsomorMakerSetting
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        private IGeneratorService GeneratorService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private EditContext Context { get; set; }
        private GeneratorSettingsModel Model { get; set; }
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
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetSettings();
        }

        private async Task GetSettings()
        {
            if (this.Id != null)
            {
                var settings = await this.GeneratorService.Get((int)this.Id);
                if (settings == null)
                {
                    this.NavigationManager.NavigateTo("/csomors");
                }
                this.Model = new GeneratorSettingsModel(settings);
            }
            else
            {
                this.Model = new GeneratorSettingsModel();
            }
        }

        private async void SaveSettings()
        {
            if (this.Context.Validate())
            {
                if (this.Id == null)
                {
                    this.Id = await this.GeneratorService.Create(this.Model);
                    this.NavigationManager.NavigateTo($"/csomor/{this.Id}");
                }
                else
                {
                    await this.GeneratorService.Update((int)this.Id, this.Model);
                }
            }
        }

        private void AddPerson()
        {
            if (this.PersonContext.Validate())
            {
                this.PersonModel.SetTables(this.Model.Start, this.Model.Finish);
                this.Model.Persons.Add(this.PersonModel);
                this.PersonModel = new PersonModel();
                this.PersonContext.NotifyValidationStateChanged();
                this.PersonContext = new EditContext(this.PersonModel);
                StateHasChanged();
            }
        }

        private void AddWork()
        {
            if (this.WorkContext.Validate())
            {
                this.WorkModel.SetTables(this.Model.Start, this.Model.Finish);
                this.Model.Works.Add(this.WorkModel);
                this.WorkModel = new WorkModel();
                this.WorkContext = new EditContext(this.WorkModel);
                StateHasChanged();
            }
        }

        private void ReSetup(DateTime date, string type)
        {
            Console.WriteLine(date);
            if (type == "start")
            {
                this.Model.Start = date.AddMinutes(-date.Minute);
            }
            if (type == "finish")
            {
                this.Model.Finish = date.AddMinutes(-date.Minute);
            }
            Console.WriteLine(this.Model.Start);
            Console.WriteLine(this.Model.Finish);
            this.Model.Persons.ForEach(x => x.UpdateTable(this.Model.Start, this.Model.Finish));
            this.Model.Works.ForEach(x => x.UpdateTable(this.Model.Start, this.Model.Finish));
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

        private async void Generate()
        {
            if (!this.Context.IsModified())
            {
                var settings = await this.GeneratorService.GenerateSimple(new GeneratorSettings(this.Id, this.Model));
                this.Model.Persons = settings.Persons.Select(x => new PersonModel(x)).ToList();
                this.Model.Works = settings.Works.Select(x => new WorkModel(x)).ToList();
                StateHasChanged();
            }
        }
    }
}
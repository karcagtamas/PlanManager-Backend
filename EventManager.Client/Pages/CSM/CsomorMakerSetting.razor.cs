﻿using EventManager.Client.Enums;
using EventManager.Client.Models;
using EventManager.Client.Services.Interfaces;
using EventManager.Client.Shared.Common;
using ManagerAPI.Shared.DTOs.CSM;
using ManagerAPI.Shared.Models.CSM;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventManager.Client.Pages.CSM
{
    public partial class CsomorMakerSetting : IDisposable
    {
        [Parameter]
        public int? Id { get; set; }

        [Inject]
        private IGeneratorService GeneratorService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject] private IModalService Modal { get; set; }

        private GeneratorSettings Settings { get; set; }
        private EditContext Context { get; set; }
        private GeneratorSettingsModel Model { get; set; }
        private EditContext PersonContext { get; set; }
        private PersonModel PersonModel { get; set; }
        private EditContext WorkContext { get; set; }
        private WorkModel WorkModel { get; set; }
        private bool IsModifiedState { get; set; }

        protected override void OnInitialized()
        {
            this.Model = new GeneratorSettingsModel();
            this.Context = new EditContext(this.Model);

            this.Context.OnFieldChanged += OnFieldChanged;

            this.PersonModel = new PersonModel();
            this.WorkModel = new WorkModel();

            this.PersonContext = new EditContext(this.PersonModel);
            this.WorkContext = new EditContext(this.WorkModel);
        }

        private void OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            this.IsModifiedState = true;
        }

        protected override async Task OnParametersSetAsync()
        {
            await this.GetSettings();
        }

        private async Task GetSettings()
        {
            if (this.Id != null)
            {
                this.Settings = await this.GeneratorService.Get((int)this.Id);
                if (this.Settings == null)
                {
                    this.NavigationManager.NavigateTo("/csomors");
                }
                this.Model = new GeneratorSettingsModel(this.Settings);
            }
            else
            {
                this.Model = new GeneratorSettingsModel();
                this.Settings = null;
            }
            this.IsModifiedState = false;
            StateHasChanged();
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
                    await this.GetSettings();
                    this.IsModifiedState = false;
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
                this.IsModifiedState = true;
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
                this.IsModifiedState = true;
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
            this.IsModifiedState = true;
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
            this.IsModifiedState = true;
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
                this.Model.HasGeneratedCsomor = settings.HasGeneratedCsomor;
                this.Model.LastGeneration = settings.LastGeneration;
                this.Settings = settings;
                this.IsModifiedState = true;
                StateHasChanged();
            }
        }

        private bool CanSave()
        {
            return this.IsModifiedState;
        }

        private bool CanGenerate()
        {
            return !this.CanSave();
        }

        public void Dispose()
        {
            this.Context.OnFieldChanged -= OnFieldChanged;
        }

        private void ChangeIsAvailableStatus(PersonTableModel model, bool value)
        {
            model.IsAvailable = value;
            this.IsModifiedState = true;
        }

        private void ChangeIsIgnoredStatus(PersonModel model, bool value)
        {
            model.IsIgnored = value;
            this.IsModifiedState = true;
        }

        private void ChangeIsActiveStatus(WorkTableModel model, bool value)
        {
            model.IsActive = value;
            this.IsModifiedState = true;
        }

        private void OpenConfirmDialog(bool status)
        {
            var parameters = new ModalParameters();
            parameters.Add("FormId", 1);
            parameters.Add("type", status ? ConfirmType.Publish : ConfirmType.Hide);
            parameters.Add("name", this.Model.Title);

            var options =
                new ModalOptions(new ModalButtonOptions(true, true, CancelButton.Cancel, ConfirmButton.Confirm));

            Modal.OnClose += ConfirmDialogClosed;
            Modal.Show<Confirm>(status ? "Confirm Publish" : "Confirm Hide", parameters, options);
        }

        private async void ConfirmDialogClosed(ModalResult modalResult)
        {
            if (!modalResult.Cancelled && (bool)modalResult.Data && this.Settings != null && this.Settings.IsPublic != null && await this.GeneratorService.ChangePublicStatus((int)this.Id, new GeneratorPublishModel { Status = !(bool)this.Settings.IsPublic }))
            {
                this.Settings.IsPublic = !(bool)this.Settings.IsPublic;
                StateHasChanged();
            }

            Modal.OnClose -= ConfirmDialogClosed;
        }
    }
}